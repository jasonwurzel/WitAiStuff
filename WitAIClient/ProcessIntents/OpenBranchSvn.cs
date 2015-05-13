using System.Collections.Generic;
using WitAIClient.Core;

namespace WitAIClient.ProcessIntents
{
	public class OpenBranchSvn
	{
		public static void Process(Outcome mostConfidentOutcome)
		{
			Dictionary<string, string> branchNameAndPath = new Dictionary<string, string> { { "trunk", GlobalValues.TrunkSolutionPath } };
			string branchName = mostConfidentOutcome.Entities.branch_name[0].value;
			CommonWindowsCalls.Process(branchNameAndPath[branchName]);
		}
	}
}