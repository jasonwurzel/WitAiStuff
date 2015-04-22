using System;
using System.IO;
using System.Linq;
using WitAIClient;
using WitAIClient.ProcessIntents;

namespace WitTextCommandConsole
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			TestSpeechActivation.Process(ProcessSpeechCommands);

			while (true)
			{
				
			}
		}

		private static void ProcessSpeechCommands()
		{
			using (var audioRecorder = new AudioRecorder())
			{
				//Console.WriteLine("Press enter to start recording speech command!");
				//Console.ReadLine();
				Console.WriteLine("Recording...To finish recording and process spoken command, press enter again!");
				audioRecorder.StartRecording();
				Console.ReadLine();
				audioRecorder.StopRecording();

				ResultFromMessageRequest response;
				using (var audioStream = File.OpenRead(audioRecorder.AudioFileName))
					response = new WitSpeechRequestTask().DoWork(audioStream);

				ProcessOutcomes(response);
				Console.WriteLine("finished");
			}
		}

		private static void ProcessTextCommands()
		{
			var command = Console.ReadLine();
			var result = new WitMessageRequestTask().DoWork(command);
			ProcessOutcomes(result);
		}

		private static void ProcessOutcomes(ResultFromMessageRequest result)
		{
			foreach (var outcome in result.Outcomes)
			{
				Console.WriteLine("Confidence: {0} Intent: {1}", outcome.Confidence, outcome.Intent);
			}

			if (result.Outcomes.Any())
			{
				var mostConfidentOutcome = result.Outcomes.OrderByDescending(outcome => outcome.Confidence).First();
				if (mostConfidentOutcome.Confidence < 0.2)
					return;

				ProcessOutcome.Go(mostConfidentOutcome, SafetyQuery);
			}
		}

		private static bool SafetyQuery()
		{
			Console.WriteLine("Are you sure? (y or n)");
			var answer = Console.ReadLine();

			return answer != null && answer.ToLower().StartsWith("y");
		}
	}
}