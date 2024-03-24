using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ResearchWF.Middleware
{
	public class AddMetadataToLogsMiddleware : IWorkflowStepMiddleware
	{
		private readonly ILogger<AddMetadataToLogsMiddleware> _log;

		public AddMetadataToLogsMiddleware(ILogger<AddMetadataToLogsMiddleware> log)
		{
			_log = log;
		}

		public async Task<ExecutionResult> HandleAsync(
			IStepExecutionContext context,
			IStepBody body,
			WorkflowStepDelegate next)
		{
			var workflowId = context.Workflow.Id;
			var stepId = context.Step.Id;

			using (_log.BeginScope("WorkflowId => {@WorkflowId}", workflowId))
			using (_log.BeginScope("StepId => {@StepId}", stepId))
			{
				return await next();
			}
		}
	}
}
