using ResearchWF.Enums;
using ResearchWF.Model;
using ResearchWF.Services.Interfaces;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ResearchWF.Steps
{
	public class NewStep : StepBodyAsync
	{
		public int userRole { get; set; }
		public int userDecision { get; set; }
		public List<int> arbitrators { get; set; }

		private IManagingEditorService managingEditorService;
		private IResearchService researchService;

		public NewStep(IManagingEditorService managingEditorService,
			IResearchService researchService)
		{
			this.managingEditorService = managingEditorService;
			this.researchService = researchService;
		}

		public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
		{
			//context.PersistenceData = new { inputResearchId, inputStatus };

			//researchService.SubmitResearch()
			userDecision = (int)UserDecision.MoveForward;
			Console.WriteLine("hello from " + nameof(NewStep)  + " this step input data from event result : " + string.Join(",", arbitrators));

			return ExecutionResult.Next();
		}

	}
}
