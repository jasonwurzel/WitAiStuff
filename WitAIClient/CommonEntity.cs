using System.Collections.Generic;
using Newtonsoft.Json;

namespace WitAIClient
{
	public class CommonEntity
	{
		//[JsonProperty("entities")]
		public List<Reminder> Reminder { get; set; }
		public List<DateAndTime> DateTime { get; set; }
		public List<Contact> Contact { get; set; }
		public List<Message_Subject> Message_Subject { get; set; }
	}

	
}