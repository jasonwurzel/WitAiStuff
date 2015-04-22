using System;
using Newtonsoft.Json;

namespace WitAIClient
{
	public class Contact : IEntity
	{
		/// <summary>
		/// Comment
		/// </summary>
		public string Value { get; set; }
	}

	public class DateAndTime : IEntity
	{
		/// <summary>
		/// Comment
		/// </summary>
		public string Grain { get; set; }

		/// <summary>
		/// Comment
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Comment
		/// </summary>
		public DateTime? Value { get; set; }
		
		/// <summary>
		/// Comment
		/// </summary>
		public DateAndTime From { get; set; }

		/// <summary>
		/// Comment
		/// </summary>
		public DateAndTime To { get; set; }
	}

	[JsonObject("datetime")]
	public class SingleDateTime : DateAndTime
	{
		
	}

	//[JsonObject("to")]
	//public class ToDateTime : DateAndTime
	//{
		
	//}

	//[JsonObject("from")]
	//public class FromDateTime : DateAndTime
	//{
		
	//}
}