using System.Collections.Generic;
using Newtonsoft.Json;

namespace WitAIClient
{
	public class ResultFromMessageRequest
	{
		[JsonProperty("msg_id")]
		public string MessageId { get; set; }
		[JsonProperty("_text")]
		public string Text { get; set; }
		[JsonProperty("outcomes")]
		public List<Outcome> Outcomes { get; set; }


	}
}