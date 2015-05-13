using WindowsInput;
using WindowsInput.Native;

namespace WitAIClient.ProcessIntents
{
	public class StartMusicPlayer
	{
		public static void Process(Outcome mostConfidentOutcome)
		{
			new InputSimulator().Keyboard.KeyPress(VirtualKeyCode.MEDIA_PLAY_PAUSE);
		}
	}
}