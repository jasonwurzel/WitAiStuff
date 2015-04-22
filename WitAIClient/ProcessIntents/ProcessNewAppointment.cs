using System;
using System.Linq;
using Microsoft.Office.Interop.Outlook;

namespace WitAIClient.ProcessIntents
{
	public class ProcessNewAppointment
	{
		public static void Go(Outcome mostConfidentOutcome)
		{
			var applicationClass = new Application();
			var item = (AppointmentItem)applicationClass.CreateItem(OlItemType.olAppointmentItem);
			item.Subject = mostConfidentOutcome.Text;
			
			item.Display();
		}
	}
}