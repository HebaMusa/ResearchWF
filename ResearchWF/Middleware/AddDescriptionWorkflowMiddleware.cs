using Microsoft.Extensions.Logging;
using ResearchWF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ResearchWF.Middleware
{
	public class AddDescriptionWorkflowMiddleware : IWorkflowMiddleware
	{
		public WorkflowMiddlewarePhase Phase => WorkflowMiddlewarePhase.PreWorkflow;
		private readonly ILogger<AddDescriptionWorkflowMiddleware> log;

		public AddDescriptionWorkflowMiddleware(ILogger<AddDescriptionWorkflowMiddleware> log)
		{
			this.log = log;
		}
		public Task HandleAsync(WorkflowInstance workflow, WorkflowDelegate next)
		{
			if (workflow.Data is ILogParameters descriptiveParams)
			{
				workflow.Description = descriptiveParams.Description;
			}

			log.LogInformation($@"Workflow {workflow.Description}");

			return next();
		}
	}
}
