using System;
using System.IO;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace WitTextCommandConsole
{
	public class TestSpeechActivation
	{
		private static SpeechRecognitionEngine _speechRecognitionEngine;
		private static Action _processSpeechCommands;
		private static bool _recording;
		private static DateTime _lastRecordingTimestamp;

		public static void Process(Action processSpeechCommands)
		{
			_processSpeechCommands = processSpeechCommands;

			try
			{
				// create the engine
				_speechRecognitionEngine = createSpeechEngine("de-DE");

				// hook to events
				_speechRecognitionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);
				_speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);

				// load dictionary
				loadGrammarAndCommands();

				// use the system's default microphone
				_speechRecognitionEngine.SetInputToDefaultAudioDevice();

				// start listening
				_speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Voice recognition failed");
			}
		}

		/// <summary>
		/// Loads the grammar and commands.
		/// </summary>
		private static void loadGrammarAndCommands()
		{
			try
			{
				Choices texts = new Choices();
				string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\example.txt");
				foreach (string line in lines)
				{
					// skip commentblocks and empty lines..
					if (line.StartsWith("--") || line == String.Empty) continue;

					// split the line
					var parts = line.Split(new char[] { '|' });

					// add the text to the known choices of speechengine
					texts.Add(parts[0]);
				}
				Grammar wordsList = new Grammar(new GrammarBuilder(texts));
				_speechRecognitionEngine.LoadGrammar(wordsList);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		/// <summary>
		/// Handles the SpeechRecognized event of the engine control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Speech.Recognition.SpeechRecognizedEventArgs"/> instance containing the event data.</param>
		static void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			Console.WriteLine(e.Result.Text + " Confidence: " + e.Result.Confidence);
			if (_recording || (DateTime.Now - _lastRecordingTimestamp) < new TimeSpan(0,0,2) || e.Result.Confidence < 0.78)
				return;

			_recording = true;
			_processSpeechCommands();
			_lastRecordingTimestamp = DateTime.Now;
			_recording = false;
		}


		/// <summary>
		/// Handles the AudioLevelUpdated event of the engine control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Speech.Recognition.AudioLevelUpdatedEventArgs"/> instance containing the event data.</param>
		static void engine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
		{
			//prgLevel.Value = e.AudioLevel;
		}


		/// <summary>
		/// Creates the speech engine.
		/// </summary>
		/// <param name="preferredCulture">The preferred culture.</param>
		/// <returns></returns>
		private static SpeechRecognitionEngine createSpeechEngine(string preferredCulture)
		{
			foreach (RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())
			{
				if (config.Culture.ToString() == preferredCulture)
				{
					_speechRecognitionEngine = new SpeechRecognitionEngine(config);
					break;
				}
			}

			// if the desired culture is not found, then load default
			if (_speechRecognitionEngine == null)
			{
				MessageBox.Show("The desired culture is not installed on this machine, the speech-engine will continue using "
					+ SpeechRecognitionEngine.InstalledRecognizers()[0].Culture.ToString() + " as the default culture.",
					"Culture " + preferredCulture + " not found!");
				_speechRecognitionEngine = new SpeechRecognitionEngine(SpeechRecognitionEngine.InstalledRecognizers()[0]);
			}

			return _speechRecognitionEngine;
		}
	}
}