using ResearchWF.Enums;
using ResearchWF.Model;
using ResearchWF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ResearchWF.Steps
{
	public class AcceptedFromEditorManager : StepBodyAsync
	{
		public int userDecision { get; set; }

		private IManagingEditorService managingEditorService;
		private IResearchService researchService;

		public AcceptedFromEditorManager(IManagingEditorService managingEditorService,
			IResearchService researchService)
		{
			this.managingEditorService = managingEditorService;
			this.researchService = researchService;
		}

		public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
		{
			userDecision = (int)UserDecision.MoveForward;
			Console.WriteLine("hello from " + nameof(AcceptedFromEditorManager));

			return ExecutionResult.Next();
		}
	}
}
