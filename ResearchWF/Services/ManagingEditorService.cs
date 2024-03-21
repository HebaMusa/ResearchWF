using ResearchWF.Services.Interfaces;
using ResearchWF.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResearchWF.Services
{
	public class ManagingEditorService : IManagingEditorService
	{
		//replace with the correct repository Interface
		//private readonly object _researchRepository;

		//replace with the correct repository Interface
		public ManagingEditorService()//object researchRepository
		{
			//_researchRepository = researchRepository;
		}

		public void AssignResearchForReview(int researchId)
		{
			// Assign the research to the managing editor for review
			//var research = _researchRepository.Find(researchId);
			//research.Status = ResearchStatus.UnderReview;

			//add main data like time and save history record
			//_researchRepository.Save(research);
		}

		public void AcceptResearch(int researchId)
		{
			//// Accept the research
			//var research = _researchRepository.Find(researchId);
			//research.Status = ResearchStatus.Accepted;

			////add main data like time and save history record

			//_researchRepository.Save(research);
		}

		public void ReturnResearchForRevision(int researchId)
		{
			//// Return the research for revision
			//var research = _researchRepository.Find(researchId);
			//research.Status = ResearchStatus.ReturnedForRevision;

			////add main data like time and save history record
			//_researchRepository.Save(research);
		}
	}
}
