using Newtonsoft.Json;
using ResearchWF.Enums;
using ResearchWF.Model;
using ResearchWF.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ResearchWF
{
	public class ResearchManagerWF : IWorkflow<WFState>
	{
		public string Id => "ResearchManagerWF";
		public int Version => 1;


		public void Build(IWorkflowBuilder<WFState> builder)
		{
			
			//shared
			var ReturnedToSource = builder.CreateBranch()
			   .StartWith<ReturnedToSourceForUpdates>()
			   .Output(data => data.outputDecision, data => data.userDecision);


			//editor manager
			var editorManagerAcceptBranch = builder.CreateBranch()
			   .StartWith<AcceptedFromEditorManager>()
			   .Output(data => data.outputDecision, data => data.userDecision);

			var editorManagerCompleted = builder.CreateBranch()
			   .StartWith<CompletedFromEditorManager>()
			   .Output(data => data.outputDecision, data => data.userDecision);

			var editorManagerRejected = builder.CreateBranch()
			   .StartWith<CompletedFromEditorManager>()
			   .Output(data => data.outputDecision, data => data.userDecision);

			//arbitrators
			var arbitratorUnderReviewBranch = builder.CreateBranch()
			   .StartWith<UnderReviewFromArbitrator>()
			   .Output(data => data.outputDecision, data => data.userDecision);

			var arbitratorAcceptBranch = builder.CreateBranch()
			   .StartWith<AcceptedFromArbitrator>()
			   .Output(data => data.outputDecision, data => data.userDecision);

			var arbitratorRejectBranch = builder.CreateBranch()
			   .StartWith<AcceptedFromArbitrator>()
			   .Output(data => data.outputDecision, data => data.userDecision);

			//supervisor
			var arbitratorSupervisorPendingReviewBranch = builder.CreateBranch()
			   .StartWith<PendingReviewFromArbitratorSupervisor>()
			   .Output(data => data.outputDecision, data => data.userDecision);

			var arbitratorSupervisorAcceptBranch = builder.CreateBranch()
			   .StartWith<AcceptedFromArbitratorSupervisor>()
			   .Output(data => data.outputDecision, data => data.userDecision);

			var arbitratorSupervisorRejectBranch = builder.CreateBranch()
			   .StartWith<RejectedFromArbitratorSupervisor>()
			   .Output(data => data.outputDecision, data => data.userDecision);

			//main
			var mainBranch = builder
			.StartWith<LogMessage>()
			.Input(x => x.Message, _ => "--- Starting workflow ---")
			
			.WaitFor("GetResearchArbitrators", e => e.getArbitratorsEventId, null, data => !string.IsNullOrEmpty(data.researchId))
					.Output(data => data.arbitrators, step => JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(step.EventData)))

			.If(data => data.userRole == (int)RoleName.User)
			.Do(then => then
				.Then<LogMessage>()
				.Input(x => x.Message, _ => "-- User ---")
				.Then<NewStep>()
			)

			.If(data => data.userRole == (int)RoleName.EditorManager)
			.Do(then => then
				.Then(context => Console.WriteLine("-- EditorManager ---"))
				.If(data => data.ContentCurrentState == (int)ResearchStatus.New).Do(then => then
					.StartWith<PendingReviewFromEditorManager>())
				.If(data => data.ContentCurrentState == (int)ResearchStatus.PendingReviewFromEditorManager).Do(then => then
					.StartWith<UnderReviewFromEditorManager>())
				.If(data => data.ContentCurrentState == (int)ResearchStatus.UnderReviewFromEditorManager).Do(then => then
					.Decide(data => data.outputDecision)
						.Branch((int)UserDecision.MoveForward, editorManagerAcceptBranch)
						.Branch((int)UserDecision.StepBack, ReturnedToSource))
				.If(data => data.ContentCurrentState == (int)ResearchStatus.AcceptedFromArbitratorSupervisor).Do(then => then
					.Decide(data => data.outputDecision)
						.Branch((int)UserDecision.MoveForward, editorManagerCompleted)
						.Branch((int)UserDecision.Reject, editorManagerRejected))
			  )

			.If(data => data.userRole == (int)RoleName.Arbitrator)
			.Do(then => then
				.Then(context => Console.WriteLine("--- Arbitrator ---"))
				.If(data => data.ContentCurrentState == (int)ResearchStatus.PendingReviewFromArbitrator).Do(then => then
					.Then<PendingReviewFromArbitrator>() // decision
					.Output(dt => dt.outputDecision, data => data.userDecision)
					.If(data => data.outputDecision == (int)UserDecision.StepBack).Do(then => then
					.Decide(data => data.outputDecision) // this is new decision added 20/03
						.Branch((int)UserDecision.MoveForward, arbitratorUnderReviewBranch)
						.Branch((int)UserDecision.StepBack, arbitratorSupervisorPendingReviewBranch))
					)
				.If(data => data.ContentCurrentState == (int)ResearchStatus.UnderReviewFromArbitrator).Do(then => then
					.Decide(data => data.outputDecision)
						.Branch((int)UserDecision.MoveForward, arbitratorAcceptBranch)
						.Branch((int)UserDecision.Reject, arbitratorRejectBranch))
			  )

			.If(data => data.userRole == (int)RoleName.ArbitratorSupervisor)
			.Do(then => then
				.Then(context => Console.WriteLine("--- ArbitratorSupervisor ---"))
				.If(data => data.ContentCurrentState == (int)ResearchStatus.PendingReviewFromArbitratorSupervisor).Do(then => then
					.ForEach(data => data.arbitrators)
					 .Do(x => x
					 .StartWith<AcceptedFromArbitrator>())
					 .Then<PendingReviewFromArbitratorSupervisor>())

				.If(data => data.ContentCurrentState == (int)ResearchStatus.AcceptedFromArbitrator).Do(then => then
					.Decide(data => data.outputDecision)
						.Branch((int)UserDecision.MoveForward, arbitratorSupervisorAcceptBranch)
						.Branch((int)UserDecision.Reject, arbitratorSupervisorRejectBranch)
						.Branch((int)UserDecision.Reject, ReturnedToSource))

				.Then<LogMessage>()
				.Input(x => x.Message, _ => "--- Finishing workflow ---")

			);


			//builder
			//.StartWith<AcceptedFromArbitrator>()

			//.Schedule(data => TimeSpan.FromSeconds(20)).Do(schedule => schedule
			//.StartWith<AcceptedFromArbitratorSupervisor>())
			//.Then<AcceptedFromArbitrator>()
			//.Then(context => Console.WriteLine("Workflow Ended"));

		}

		private int printItem(object item)
		{
			Console.WriteLine(item);
			return Convert.ToInt32( item);
		}
	}
}
