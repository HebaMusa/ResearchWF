using ResearchWF.Model;
using ResearchWF.Enums;

namespace ResearchWF.Services.Interfaces
{
	public interface IResearchService
	{
		void SubmitResearch(int researchId, ResearchStatus newStatus);
		void AcceptResearch(int researchId);
		void ReturnResearchForRevision(int researchId);
	}
}
