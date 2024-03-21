using ResearchWF.Enums;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ResearchWF.Steps
{
	public class RejectedFromEditorManager : StepBodyAsync
	{
		public int userDecision { get; set; }

		public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
		{
			userDecision = (int)UserDecision.MoveForward;
			Console.WriteLine("hello from " + nameof(RejectedFromEditorManager));

			return ExecutionResult.Next();
		}
	}
}
