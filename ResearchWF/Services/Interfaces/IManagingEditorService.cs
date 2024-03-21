using ResearchWF.Enums;


namespace ResearchWF.Services.Interfaces
{
	public interface IManagingEditorService
	{
		void AssignResearchForReview(int researchId);
		void AcceptResearch(int researchId);
		void ReturnResearchForRevision(int researchId);
	}
}
