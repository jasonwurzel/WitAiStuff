using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using todoistsharp;

namespace WitAIClient.ProcessIntents
{
	public class ProcessReminder
	{
		private static string _todoistApiToken = ConfigurationManager.AppSettings["todoistAccessToken"];

		public static ActionOutcome Go(Outcome mostConfidentOutcome)
		{

			// TODO: ProcessNewAppointment.Go(mostConfidentOutcome); wenn 2 Datümer.... (=> Calendar Entry)
			if (mostConfidentOutcome.Entities != null)
			{
				if (mostConfidentOutcome.Entities.reminder != null)
				{
					var reminders = mostConfidentOutcome.Entities.reminder;
					foreach (var reminder in reminders)
						Console.WriteLine("***reminder set:	Task: {0}", string.Join("***", reminder.value));
				}
				DateTime? dueDate = null;
				if (mostConfidentOutcome.Entities.datetime != null)
				{
					dueDate = ((IEnumerable<dynamic>)mostConfidentOutcome.Entities.datetime).Select<dynamic, DateTime?>(time => time.value).FirstOrDefault();
					foreach (var dateAndTime in mostConfidentOutcome.Entities.datetime)
						if (dateAndTime.type == "interval")
							Console.WriteLine("***interval: from: {0} to: {1}", dateAndTime.from.value, dateAndTime.to.value);
						else if (dateAndTime.type == "value")
							Console.WriteLine("***time: {0}", dateAndTime.value);
				}

				if (mostConfidentOutcome.Entities.reminder != null) AddItemInTodoist((string)mostConfidentOutcome.Entities.reminder[0].value, dueDate);

				return ActionOutcome.Success;
			}

			return ActionOutcome.CommandNotFound;
		}

		public static void AddItemInTodoist(string reminderText, DateTime? dueDate)
		{
			var todoist = new Todoist();
			todoist.Login(_todoistApiToken);
			var projects = todoist.GetProjects();
			var projectId = projects.FirstOrDefault().id;
			var addedItem = todoist.AddItem(projectId, reminderText, date_string: "", dueDate: dueDate);
		}
	}
}