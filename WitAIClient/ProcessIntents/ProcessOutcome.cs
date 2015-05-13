﻿using System;
using System.Collections.Generic;
using WitAIClient.Core;

namespace WitAIClient.ProcessIntents
{
	public class ProcessOutcome
	{

		public static void Go(Outcome mostConfidentOutcome, Func<bool> safetyQuery)
		{
			Dictionary<string, Action<Outcome>> intentActions = new Dictionary<string, Action<Outcome>>
																{
																	{"reminder", ProcessReminder.Go}, 
																	{"newmail", ProcessNewMail.Go}, 
																	{"open_app", CommonWindowsCalls.StartProcess},
																	{"open_branch_svn", OpenBranchSvn.Process},
																	{"update_branch_svn", UpdateBranchSvn.Process},
																	{"commit_branch_svn", CommitBranchSvn.Process},
																	{"log_branch_svn", LogBranchSvn.Process},
																	{"play_music", OpenMusicPlayer.Process},
																	{"stop_music", StopMusicPlayer.Process},
																	{"start_music", StartMusicPlayer.Process},


																};
			var intent = mostConfidentOutcome.Intent;

			if (ProcessOsCommand.OsCommands.Contains(intent))
				ProcessOsCommand.Go(mostConfidentOutcome, safetyQuery);
			else
				intentActions[intent](mostConfidentOutcome);
		}
	}
}