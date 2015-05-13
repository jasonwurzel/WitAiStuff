using WitAIClient.Core;

namespace WitAIClient.ProcessIntents
{
	public class OpenMusicPlayer
	{
		public static void Process(Outcome mostConfidentOutcome)
		{
			CommonWindowsCalls.Process(@"C:\Program Files (x86)\MediaMonkey\MediaMonkey.exe");
		}
	}
}