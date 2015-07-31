using System.Collections.Generic;
using WitAIClient.Core;

namespace WitAIClient.ProcessIntents
{
	public class OpenBranchSvn
	{
		public static ActionOutcome Process(Outcome mostConfidentOutcome)
		{
			Dictionary<string, string> branchNameAndPath = new Dictionary<string, string> { { "trunk", GlobalValues.TrunkSolutionPath } };
			string branchName = mostConfidentOutcome.Entities.branch_name[0].value;

			if (!branchNameAndPath.ContainsKey(branchName))
				return ActionOutcome.CommandNotFound;

			CommonWindowsCalls.Process(branchNameAndPath[branchName]);

			return ActionOutcome.Success;
		}
	}
}