using System.Collections.Generic;
using System.Linq;

namespace WitAIClient.Core
{
	public class CommonWindowsCalls
	{
		public static void StartProcess(Outcome commandLine)
		{
			Dictionary<string, string> appNameAndPath = new Dictionary<string, string>
														{
															{"explorer", "explorer"},
															{"visual studio", @"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe"}
														};
			if (commandLine.Entities.app_name == null || commandLine.Entities.app_name.Count == 0)
				return;

			string appName = commandLine.Entities.app_name[0].value;
			Process(appNameAndPath[appName.ToLower()]);
		}

		public static void Process(string exeName, string arguments = null) { System.Diagnostics.Process.Start(exeName, arguments); }
	}
}