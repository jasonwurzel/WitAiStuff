using System.Diagnostics;
using System.Linq;

namespace WitAIClient.Core
{
	public class CommonWindowsCalls
	{
		public static void StartProcess(Outcome commandLine)
		{
			// @"W:\trunk\trunk\formware10cs.sln"
			var subject = commandLine.Entities.Message_Subject;
			if (subject == null || !subject.Any())
				return;

			var messageSubject = subject.FirstOrDefault();
			if (messageSubject == null)
				return;

			var target = messageSubject.Value.ToLower();
			if (target.ToLower() == "explorer")
				Process("explorer");
			if (target.ToLower() == "visual studio")
				Process(@"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe");
			
		}

		public static void Process(string exeName, string arguments = null) { System.Diagnostics.Process.Start(exeName, arguments); }
	}
}