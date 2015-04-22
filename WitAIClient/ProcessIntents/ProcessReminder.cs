using System;
using System.Globalization;
using System.Linq;
using todoistsharp;

namespace WitAIClient.ProcessIntents
{
	public class ProcessReminder
	{
		private static string _todoistApiToken = "7ce6336751321a8ab9dc6904782ab7f00621e17f";

		public static void Go(Outcome mostConfidentOutcome)
		{

			// TODO: ProcessNewAppointment.Go(mostConfidentOutcome); wenn 2 Datümer.... (=> Calendar Entry)
			if (mostConfidentOutcome.Entities != null)
			{
				if (mostConfidentOutcome.Entities.Reminder != null)
				{
					var reminders = mostConfidentOutcome.Entities.Reminder;
					foreach (var reminder in reminders)
					{
						Console.WriteLine("reminder set:	Task: {0}", string.Join("***", reminder.Value));
					}
				}
				DateTime? dueDate = null;
				if (mostConfidentOutcome.Entities.DateTime != null)
				{
					dueDate = mostConfidentOutcome.Entities.DateTime.Select(time => time.Value).FirstOrDefault();
					foreach (var dateAndTime in mostConfidentOutcome.Entities.DateTime)
					{
						if (dateAndTime.Type == "interval")
						{
							Console.WriteLine("		interval: from: {0} to: {1}", dateAndTime.From.Value, dateAndTime.To.Value);
						}
						else if (dateAndTime.Type == "value")
						{
							Console.WriteLine("		time: {0}", dateAndTime.Value);
						}
					}
					
				}

				if (mostConfidentOutcome.Entities.Reminder != null) AddItemInTodoist(mostConfidentOutcome.Entities.Reminder.First().Value, dueDate);
			}
		}

		public static void AddItemInTodoist(string reminderText, DateTime? dueDate)
		{
			var todoist = new Todoist();
			todoist.Login(_todoistApiToken);
			var projects = todoist.GetProjects();
			var projectId = projects.FirstOrDefault().id;
			var addItem = todoist.AddItem(projectId, reminderText, date_string: "", dueDate: dueDate);
		}
	}
}