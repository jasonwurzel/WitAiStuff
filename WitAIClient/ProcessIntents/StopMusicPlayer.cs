using WindowsInput;
using WindowsInput.Native;

namespace WitAIClient.ProcessIntents
{
	public class StopMusicPlayer
	{
		public static ActionOutcome Process(Outcome mostConfidentOutcome)
		{
			new InputSimulator().Keyboard.KeyPress(VirtualKeyCode.MEDIA_PLAY_PAUSE);

			return ActionOutcome.Success;
		}
	}
}