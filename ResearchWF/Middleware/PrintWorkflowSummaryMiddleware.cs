using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ResearchWF.Middleware
{
	public class PrintWorkflowSummaryMiddleware : IWorkflowMiddleware
	{
		private readonly ILogger<PrintWorkflowSummaryMiddleware> log;
		public WorkflowMiddlewarePhase Phase => WorkflowMiddlewarePhase.PostWorkflow;

		public PrintWorkflowSummaryMiddleware(ILogger<PrintWorkflowSummaryMiddleware> log)
		{
			this.log = log;
		}

		public Task HandleAsync(WorkflowInstance workflow, WorkflowDelegate next)
		{
			if (!workflow.CompleteTime.HasValue)
			{
				return next();
			}

			var duration = workflow.CompleteTime.Value - workflow.CreateTime;
			
			log.LogInformation($@"Workflow --> completed in {duration:g}");

			foreach (var step in workflow.ExecutionPointers)
			{
				if (!string.IsNullOrEmpty(step.StepName))
				{
					var stepName = step.StepName;
					var stepDuration = (step.EndTime - step.StartTime) ?? TimeSpan.Zero;
					log.LogInformation($"  - Step {stepName} completed in {stepDuration:g}");
				}
			}

			return next();
		}

	}
}
