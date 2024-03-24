using Microsoft.Extensions.Logging;
using ResearchWF.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ResearchWF.Steps
{
	public class LogMessage : StepBodyAsync
	{
		public string Message { get; set; }
		private readonly ILogger<LogMessage> log;

		public LogMessage(ILogger<LogMessage> log)
		{
			this.log = log;
		}

		public override Task<ExecutionResult> RunAsync(IStepExecutionContext context)
		{
			if (Message != null)
			{
				this.log.LogInformation(Message);
			}

			return Task.FromResult(ExecutionResult.Next());
		}
	}
}
