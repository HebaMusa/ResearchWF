using ResearchWF.Enums;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ResearchWF.Steps
{
	public class AcceptedFromArbitrator : StepBodyAsync
	{
		public int userRole { get; set; }
		public int userDecision { get; set; }


		public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
		{
			userDecision = (int)UserDecision.MoveForward;
			Console.WriteLine("hello from " + nameof(AcceptedFromArbitrator));
			return ExecutionResult.Next();
		}
	}
}
