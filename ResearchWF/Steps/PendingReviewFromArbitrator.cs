using ResearchWF.Enums;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ResearchWF.Steps
{
	public class PendingReviewFromArbitrator : StepBodyAsync
	{
		public int userDecision { get; set; }

		public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
		{
			// call services that check if (current date - assign date) > 3
			// if true set decision to back to supervisor to re-asign
			userDecision = (int)UserDecision.StepBack; 
			Console.WriteLine("hello from " + nameof(PendingReviewFromArbitrator));

			return ExecutionResult.Next();
		}
	}
}
