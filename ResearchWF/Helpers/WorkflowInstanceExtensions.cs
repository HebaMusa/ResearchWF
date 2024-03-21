using ResearchWF.Enums;
using ResearchWF.Model;
using WorkflowCore.Models;

namespace ResearchWF.Helpers
{

	public static class WorkflowInstanceExtensions
	{
		public static ValidateUserDTO ValidateUserActions(this WorkflowInstance workflow, string actionKey, string user, bool value)
		{
			return new ValidateUserDTO { UserId = "userId", OutcomeValue = ResultStatus.Success };
		}
	}
}