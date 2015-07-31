using System.Collections.Generic;
using System.Linq;
using WitAIClient.ProcessIntents;

namespace WitAIClient.Core
{
	public class CommonWindowsCalls
	{
		public static ActionOutcome StartProcess(Outcome commandLine)
		{
			Dictionary<string, string> appNameAndPath = new Dictionary<string, string>
														{
															{"explorer", "explorer"},
															{"visual studio 12", @"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe"},
															{"visual studio 14", @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe"},
															{"task manager", "taskmgr"}
														};
			if (commandLine.Entities.app_name == null || commandLine.Entities.app_name.Count == 0)
				return ActionOutcome.CommandNotFound;

			string appName = commandLine.Entities.app_name[0].value;
			Process(appNameAndPath[appName.ToLower()]);

			return ActionOutcome.Success;
		}

		public static void Process(string exeName, string arguments = null) { System.Diagnostics.Process.Start(exeName, arguments); }
	}
}