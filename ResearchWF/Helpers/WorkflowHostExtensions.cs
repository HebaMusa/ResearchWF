using ResearchWF.Enums;
using ResearchWF.Model;
using WorkflowCore.Interface;

namespace ResearchWF.Helpers
{
	public static class WorkflowHostExtensions
	{
		public static ValidateUserDTO ValidateUserActions(this IWorkflowHost host, string workflowId)
		{
			var workflow = host.PersistenceStore.GetWorkflowInstance(workflowId).Result;

			return new ValidateUserDTO { UserId="userId", OutcomeValue = ResultStatus.Success };
		}
	}
}
