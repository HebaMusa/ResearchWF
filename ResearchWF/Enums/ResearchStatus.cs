using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchWF.Enums
{
    public enum ResearchStatus
    {
        New,
        PendingReviewFromEditorManager,
        UnderReviewFromEditorManager,
        ReturnedToSourceForUpdates,
        AcceptedFromEditorManager,

		PendingReviewFromArbitratorSupervisor,
		
        PendingReviewFromArbitrator,
		UnderReviewFromArbitrator, //how to make this 3 times -- list that gives final decision before change state
		AcceptedFromArbitrator, //must calculate 3 with rating
		RejectedFromArbitrator, //must calculate 3 with rating
		
        // conditionl acceptance ?? // accept to voting???
       
        AcceptedFromArbitratorSupervisor,
		RejectedFromArbitratorSupervisor, //next also //PendingReviewFromEditorManager //but already exist no 2

		CompletedFromEditorManager,
		RejectedFromEditorManager
	}
}
