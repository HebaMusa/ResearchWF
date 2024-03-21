using ResearchWF.Enums;

namespace ResearchWF.Model
{
    public class Magazine
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public MagazineStatus Status { get; set; }
		public List<int> ResearchIds { get; set; }
	}
}
