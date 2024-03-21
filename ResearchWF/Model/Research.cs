using ResearchWF.Enums;

namespace ResearchWF.Model
{
    public class Research
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Author { get; set; } = string.Empty;
		public string ContentBrief { get; set; } = string.Empty;
		public ResearchStatus Status { get; set; }
	}
}
