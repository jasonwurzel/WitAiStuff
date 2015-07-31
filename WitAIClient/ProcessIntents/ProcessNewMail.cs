using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Outlook;

namespace WitAIClient.ProcessIntents
{
	public class ProcessNewMail
	{
		public static ActionOutcome Go(Outcome mostConfidentOutcome)
		{
			var applicationClass = new Application();
			var item = (MailItem)applicationClass.CreateItem(OlItemType.olMailItem);
			if (mostConfidentOutcome.Entities != null && mostConfidentOutcome.Entities.Contact != null)
				item.To = String.Join(";", ((IEnumerable<dynamic>)mostConfidentOutcome.Entities.Contact).Select(contact => contact.Value).ToArray());
			item.Display();

			return ActionOutcome.Success;
		}
	}
}