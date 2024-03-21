using ResearchWF.Model;
using ResearchWF.Services.Interfaces;
using ResearchWF.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResearchWF.Services
{
	public class ResearchService : IResearchService
	{
		//replace with the correct repository Interface
		//private readonly object _researchRepository;

		public ResearchService()//object researchRepository
		{
			//_researchRepository = researchRepository;
		}

		public void SubmitResearch(int researchId, ResearchStatus newStatus)
		{
			// Save the research to the database
			//_researchRepository.Save(research);
		}

		public void AcceptResearch(int researchId)
		{
			//// Update the research status to "Accepted" in the database
			//var research = _researchRepository.Find(researchId);
			//research.Status = ResearchStatus.Accepted;
			//_researchRepository.Save(research);
		}

		public void ReturnResearchForRevision(int researchId)
		{
			//// Update the research status to "Returned for Revision" in the database
			//var research = _researchRepository.Find(researchId);
			//research.Status = ResearchStatus.ReturnedForRevision;
			//_researchRepository.Save(research);
		}
	}
}
