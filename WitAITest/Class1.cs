using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioProcessingModuleCs.Media;
using AudioProcessingModuleCs.Media.Dsp.WebRtc;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using WitAIClient;

namespace WitAITest
{

		/// <summary>
		/// Kommentar
		/// </summary>
		[TestFixture]
		public class BasicWitCallTest
		{
			//private WebRtcFilter _enhancer;
			private List<short> _shorts = new List<short>();

			/// <summary>
			/// Kommentar2
			/// </summary>
			[Test, Explicit]
			public void TestVoiceActivation()
			{
				//new Thread(TestInBackground).Start();
				TestInBackground();
				while (true)
				{
				}
			}

			private void TestInBackground()
			{
				File.Delete(@"W:\fastTemp\out.wav");
				//_enhancer = new WebRtcFilter(240, 100, new AudioFormat(8000), new AudioFormat(8000), true, true, true);
				

				
			}


			// Handle the SpeechRecognized event.
			static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
			{
				Console.WriteLine("Recognized text: " + e.Result.Text);
			}

			/// <summary>
			/// Kommentar2
			/// </summary>
			[Test, Explicit]
			public void TestMicrosoft()
			{
				// the recognition engine
				SpeechRecognitionEngine speechRecognitionEngine = null;

				// create the engine with a custom method (i will describe that later)
				speechRecognitionEngine = createSpeechEngine("de-DE");

				// hook to the needed events
				speechRecognitionEngine.AudioLevelUpdated +=
				  new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);
				speechRecognitionEngine.SpeechRecognized +=
				  new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);

				// load a custom grammar, also described later
				//loadGrammarAndCommands();

				// use the system's default microphone, you can also dynamically
				// select audio input from devices, files, or streams.
				speechRecognitionEngine.SetInputToDefaultAudioDevice();

				// start listening in RecognizeMode.Multiple, that specifies
				// that recognition does not terminate after completion.
				speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
			}

			private void engine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
			{
			
			}

			// Recognized-event 
			void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
			{
			}

			private SpeechRecognitionEngine createSpeechEngine(string preferredCulture)
			{
				SpeechRecognitionEngine speechRecognitionEngine = null;
				foreach (RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())
				{
					if (config.Culture.ToString() == preferredCulture)
					{
						speechRecognitionEngine = new SpeechRecognitionEngine(config);
						break;
					}
				}

				// if the desired culture is not installed, then load default
				if (speechRecognitionEngine == null)
				{
					MessageBox.Show("The desired culture is not installed " +
						"on this machine, the speech-engine will continue using "
						+ SpeechRecognitionEngine.InstalledRecognizers()[0].Culture.ToString() +
						" as the default culture.", "Culture " + preferredCulture + " not found!");
					speechRecognitionEngine = new SpeechRecognitionEngine();
				}

				return speechRecognitionEngine;
			}

			/// <summary>
			/// Kommentar2
			/// </summary>
			[Test, Explicit]
			public void Test01()
			{
				new WitMessageRequestTask().DoWork("Remind me to call Alexandra in 10 Minutes");
			}

			/// <summary>
			/// Kommentar2
			/// </summary>
			[Test, Explicit]
			public void Test02()
			{
				using (var audioStream = File.OpenRead(@"W:\fastTemp\out.wav"))
				{
					var response = new WitSpeechRequestTask().DoWork(audioStream);
				}
			}

			/// <summary>
			/// Kommentar2
			/// </summary>
			[Test]
			public void TestDeserializeResult01()
			{
				var response = @"{
	""msg_id"": ""1657ecab-d623-43a9-913e-4cf0f0304c65"",
	""_text"": ""Remind me to call Alexandra in 10 Minutes"",
	""outcomes"": [{
		""_text"": ""Remind me to call Alexandra in 10 Minutes"",
		""intent"": ""reminder"",
		""entities"": {
			""reminder"": [{
				""value"": ""call Alexandra""
			}],
			""datetime"": [{
				""grain"": ""second"",
				""type"": ""value"",
				""value"": ""2014-10-27T15:11:35.000+01:00""
			}]
		},
		""confidence"": 0.66
	}]
}";
				var resultFromMessageRequest = JsonSerializer.Create().Deserialize<ResultFromMessageRequest>(new JsonTextReader(new StringReader(response)));
				resultFromMessageRequest.MessageId.Should().Be("1657ecab-d623-43a9-913e-4cf0f0304c65");
				resultFromMessageRequest.Text.Should().Be("Remind me to call Alexandra in 10 Minutes");
				resultFromMessageRequest.Outcomes.Count.Should().Be(1);
				var firstOutcome = resultFromMessageRequest.Outcomes.ElementAt(0);
				firstOutcome.Confidence.Should().Be(0.66);
				firstOutcome.Text.Should().Be("Remind me to call Alexandra in 10 Minutes");
				firstOutcome.Entities.Reminder.Count.Should().Be(1);
				firstOutcome.Entities.Reminder.ElementAt(0).Value.Should().Be("call Alexandra");
				firstOutcome.Entities.DateTime.Count.Should().Be(1);
				firstOutcome.Entities.DateTime.ElementAt(0).Grain.Should().Be("second");
				firstOutcome.Entities.DateTime.ElementAt(0).Type.Should().Be("value");
				firstOutcome.Entities.DateTime.ElementAt(0).Value.Should().Be(new DateTime(2014,10,27,15,11,35));
			}
			
			/// <summary>
			/// Kommentar2
			/// </summary>
			[Test]
			public void TestDeserializeResult02()
			{
				var response = @"{
	""msg_id"": ""73f1bb51-d0d3-4968-baee-fd13003a6496"",
	""_text"": ""remind me to clean tonight"",
	""outcomes"": [{
		""_text"": ""remind me to clean tonight"",
		""intent"": ""reminder"",
		""entities"": {
			""datetime"": [{
				""to"": {
					""value"": ""2014-11-07T00:00:00.000+01:00"",
					""grain"": ""hour""
				},
				""from"": {
					""value"": ""2014-11-06T18:00:00.000+01:00"",
					""grain"": ""hour""
				},
				""type"": ""interval""
			}],
			""reminder"": [{
				""value"": ""clean""
			}],
			""contact"": [{
				""value"": ""me""
			}]
		},
		""confidence"": 0.657
	}]
}";
				var resultFromMessageRequest = JsonSerializer.Create().Deserialize<ResultFromMessageRequest>(new JsonTextReader(new StringReader(response)));
				resultFromMessageRequest.MessageId.Should().Be("73f1bb51-d0d3-4968-baee-fd13003a6496");
				resultFromMessageRequest.Text.Should().Be("remind me to clean tonight");
				resultFromMessageRequest.Outcomes.Count.Should().Be(1);
				var firstOutcome = resultFromMessageRequest.Outcomes.ElementAt(0);
				firstOutcome.Confidence.Should().Be(0.657);
				firstOutcome.Text.Should().Be("remind me to clean tonight");
				firstOutcome.Entities.Reminder.Count.Should().Be(1);
				firstOutcome.Entities.Reminder.ElementAt(0).Value.Should().Be("clean");
				firstOutcome.Entities.DateTime.Count.Should().Be(1);
				firstOutcome.Entities.DateTime.ElementAt(0).Grain.Should().BeNull();
				firstOutcome.Entities.DateTime.ElementAt(0).Type.Should().Be("interval");
				firstOutcome.Entities.DateTime.ElementAt(0).Value.Should().NotHaveValue();
				firstOutcome.Entities.DateTime.ElementAt(0).From.Value.Should().Be(new DateTime(2014, 11, 06, 18, 00, 00));
				firstOutcome.Entities.DateTime.ElementAt(0).To.Value.Should().Be(new DateTime(2014, 11, 07, 00, 00, 00));

			}
			/// <summary>
			/// Kommentar2
			/// </summary>
			[Test]
			public void TestDeserializeResult03()
			{
				var response = @"{
	""msg_id"": ""16097f81-d7d8-415a-9ad0-9ad6b4c9a881"",
	""_text"": ""new appointment tonight at the cinema"",
	""outcomes"": [{
		""_text"": ""new appointment tonight at the cinema"",
		""intent"": ""appointment"",
		""entities"": {
			""message_subject"": [{
				""value"": ""cinema""
			}]
		},
		""confidence"": 0.994
	}]
}";
				var jsonTextReader = new JsonTextReader(new StringReader(response));
				var resultFromMessageRequest = JsonSerializer.Create().Deserialize<ResultFromMessageRequest>(jsonTextReader);
				resultFromMessageRequest.MessageId.Should().Be("16097f81-d7d8-415a-9ad0-9ad6b4c9a881");
				resultFromMessageRequest.Text.Should().Be("new appointment tonight at the cinema");
				resultFromMessageRequest.Outcomes.Count.Should().Be(1);
				var firstOutcome = resultFromMessageRequest.Outcomes.ElementAt(0);
				firstOutcome.Confidence.Should().Be(0.994);
				firstOutcome.Text.Should().Be("new appointment tonight at the cinema");
				firstOutcome.Entities.Message_Subject.Count.Should().Be(1);
				firstOutcome.Entities.Message_Subject.ElementAt(0).Value.Should().Be("cinema");
			}
		}

	//@Override
	//protected void onPostExecute(String result) {
	//}
	//}

	
}


