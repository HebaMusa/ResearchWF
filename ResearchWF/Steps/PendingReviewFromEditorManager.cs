using ResearchWF.Enums;
using ResearchWF.Model;
using ResearchWF.Services;
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
	public class PendingReviewFromEditorManager : StepBody
	{
		public int userDecision { get; set; }

		private IManagingEditorService managingEditorService;
		private IResearchService researchService;
		public PendingReviewFromEditorManager(IManagingEditorService managingEditorService,
			IResearchService researchService)
		{
			this.managingEditorService = managingEditorService;
			this.researchService = researchService;
		}

		public override ExecutionResult Run(IStepExecutionContext context)
		{
			//researchService.ValidateResearch();
			//managingEditorService.ValidateUser(researchId);


			userDecision = (int)UserDecision.MoveForward;
			Console.WriteLine("hello from " + nameof(PendingReviewFromEditorManager));

			return ExecutionResult.Next();
		}
	}
}
