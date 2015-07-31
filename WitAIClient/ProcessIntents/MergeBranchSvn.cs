using System.Collections.Generic;
using WitAIClient.Core;

namespace WitAIClient.ProcessIntents
{
	public class MergeBranchSvn
	{
		public static ActionOutcome Process(Outcome mostConfidentOutcome)
		{
			Dictionary<string, string> branchNameAndPath = new Dictionary<string, string>
															{
																{"trunk", @"C:\Sourcen\trunk\trunk"},
																{"frozen", @"C:\Sourcen\frozen\frozen"}
															};

			string branchName = mostConfidentOutcome.Entities.branch_name[0].value;

			if (!branchNameAndPath.ContainsKey(branchName))
				return ActionOutcome.CommandNotFound;

			CommonWindowsCalls.Process(@"C:\Programme\tortoiseSVN\bin\TortoiseProc.exe", string.Format(@"/command:merge /path:{0}", branchNameAndPath[branchName]));

			return ActionOutcome.Success;
		}
	}
}