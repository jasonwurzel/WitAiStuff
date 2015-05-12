using System;
using System.Collections.Generic;
using Core;

namespace WitAIClient.ProcessIntents
{
	public class ProcessOsCommand
	{
		private const string PcLock = "pc_lock";
		private const string PcRestart = "pc_restart";
		private const string PcShutdown = "pc_shutdown";

		public static readonly List<string> OsCommands = new List<string> { PcLock, PcRestart, PcShutdown };

		public static void Go(Outcome mostConfidentOutcome, Func<bool> safetyQuery)
		{
			switch (mostConfidentOutcome.Intent)
			{
				case PcLock:
					WinApiCalls.LockThisWorkStation();
					break;
				case PcRestart:
					WinApiCalls.RestartWorkstation(safetyQuery);
					break;
				case PcShutdown:
					WinApiCalls.ShutdownWorkstation(safetyQuery);
					break;
				default:
					Console.WriteLine("OS Command: {0}", mostConfidentOutcome.Intent); 
					break;
			}
		}
	}
}