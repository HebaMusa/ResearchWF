using ResearchWF.Enums;

namespace ResearchWF.Model
{
    public class WFState
	{
		public string researchId { get; set; }
		public int userRole { get; set; }
		public int ContentCurrentState { get; set; }
		public int outputDecision { get; set; }
		public string getArbitratorsEventId { get; set; }
		public List<int> arbitrators { get; set; }
	}
}
