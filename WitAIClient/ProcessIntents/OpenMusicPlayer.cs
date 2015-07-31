using WitAIClient.Core;

namespace WitAIClient.ProcessIntents
{
	public class OpenMusicPlayer
	{
		public static ActionOutcome Process(Outcome mostConfidentOutcome)
		{
			CommonWindowsCalls.Process(@"C:\Program Files (x86)\MediaMonkey\MediaMonkey.exe");

			return ActionOutcome.Success;
		}
	}
}