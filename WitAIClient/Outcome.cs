using Newtonsoft.Json;

namespace WitAIClient
{
	public class Outcome
	{
		[JsonProperty("_text")]
		public string Text { get; set; }
		[JsonProperty("intent")]
		public string Intent { get; set; }
		[JsonProperty("entities")]
		public CommonEntity Entities { get; set; }
		public double Confidence { get; set; }
	}
}