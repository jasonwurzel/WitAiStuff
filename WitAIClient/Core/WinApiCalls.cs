using System;
using System.Runtime.InteropServices;

namespace Core
{
	public class WinApiCalls
	{
		[DllImport("user32")]
		public static extern void LockWorkStation();
		public static void LockThisWorkStation()
		{
			//Console.WriteLine("Dummy Lock");
			LockWorkStation();
		}

		// Call InitiateShutdown 
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool InitiateShutdown(string lpMachineName,
			string lpMessage,
			UInt32 dwGracePeriod,
			UInt32 dwShutdownFlags,
			UInt32 dwReason);

		public static void ShutdownWorkstation(Func<bool> safetyQuery)
		{
			UInt32 flags = 0x8;
			UInt32 reason = 0;
			UInt32 gracePeriod = 5;
			if (safetyQuery())
				InitiateShutdown(Environment.MachineName, "", gracePeriod, flags, reason);
			//Console.WriteLine("Dummy Shutdown");
		}

		public static void RestartWorkstation(Func<bool> safetyQuery)
		{
			UInt32 flags = 0x4;
			UInt32 reason = 0;
			UInt32 gracePeriod = 5;
			if (safetyQuery())
				InitiateShutdown(Environment.MachineName, "", gracePeriod, flags, reason);
			//Console.WriteLine("Dummy Restart");
		}
	}
}