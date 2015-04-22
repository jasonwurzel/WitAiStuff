using System.Collections.Generic;
using Newtonsoft.Json;

namespace WitAIClient
{
	[JsonObject("message_subject")]
	public class Message_Subject : IEntity
	{
		public string Value { get; set; }
		//public List<string> Value { get; set; }
	}
}