using System;
using WindowsInput;
using WindowsInput.Native;
using WitAIClient.Core;

namespace WitAIClient.ProcessIntents
{
	public class ProcessOutcome
	{

		public static void Go(Outcome mostConfidentOutcome, Func<bool> safetyQuery)
		{
			var intent = mostConfidentOutcome.Intent;

			if (intent == "reminder")
			{
				ProcessReminder.Go(mostConfidentOutcome);
			}
			else if (ProcessOsCommand.OsCommands.Contains(intent))
			{
				ProcessOsCommand.Go(mostConfidentOutcome, safetyQuery);
			}
			else if (intent == "newmail")
			{
				ProcessNewMail.Go(mostConfidentOutcome);
			}
			else if(intent == "open_app")
			{
				CommonWindowsCalls.StartProcess(mostConfidentOutcome);
			}
			else if(intent == "open_trunk")
			{
				CommonWindowsCalls.Process(GlobalValues.TrunkSolutionPath);
			}
			else if(intent == "update_trunk")
			{
				CommonWindowsCalls.Process(@"C:\Programme\tortoiseSVN\bin\TortoiseProc.exe", @"/command:update /path:C:\Sourcen\trunk\trunk");
			}
			else if (intent == "commit_branch_svn")
			{
				// TODO: 
				/*{  "msg_id" : "3bb28716-27db-4938-a174-3abc9c1fb19d",  "_text" : "commit trunk",  "outcomes" : [ {    "_text" : "commit trunk",    "intent" : "commit_branch_svn",    "entities" : {      "branch_name" : [ {        "value" : "trunk"      } ]    },    "confidence" : 0.994  } ]}*/
				CommonWindowsCalls.Process(@"C:\Programme\tortoiseSVN\bin\TortoiseProc.exe", @"/command:commit /path:C:\Sourcen\trunk\trunk");
			}
			else if(intent == "log_trunk")
			{
				CommonWindowsCalls.Process(@"C:\Programme\tortoiseSVN\bin\TortoiseProc.exe", @"/command:log /path:C:\Sourcen\trunk\trunk");
			}
			else if(intent == "play_music")
			{
				CommonWindowsCalls.Process(@"C:\Program Files (x86)\MediaMonkey\MediaMonkey.exe");
			}
			else if(intent == "stop_music")
			{
				new InputSimulator().Keyboard.KeyPress(VirtualKeyCode.MEDIA_PLAY_PAUSE);
			}
			else if(intent == "start_music")
			{
				new InputSimulator().Keyboard.KeyPress(VirtualKeyCode.MEDIA_PLAY_PAUSE);
			}
			else if(intent == "music_next_track")
			{
				// TODO
			}
		}
	}
}