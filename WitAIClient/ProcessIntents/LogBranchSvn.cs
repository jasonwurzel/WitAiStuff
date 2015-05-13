using System.Collections.Generic;
using WitAIClient.Core;

namespace WitAIClient.ProcessIntents
{
	public class LogBranchSvn
	{
		public static void Process(Outcome mostConfidentOutcome)
		{
			Dictionary<string, string> branchNameAndPath = new Dictionary<string, string>{{"trunk", @"C:\Sourcen\trunk\trunk"}};

			string branchName = mostConfidentOutcome.Entities.branch_name[0].value;
			CommonWindowsCalls.Process(@"C:\Programme\tortoiseSVN\bin\TortoiseProc.exe", string.Format(@"/command:log /path:{0}", branchNameAndPath[branchName]));
		}
	}
}