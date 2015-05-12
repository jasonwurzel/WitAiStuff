using System;
using System.IO;
using NAudio.Wave;

namespace WitAIClient
{
	public class AudioRecorder : IDisposable
	{
		private WaveFileWriter _waveFileWriter;
		private WaveIn _waveIn;
		private WaveFormat _waveFormat;


		public AudioRecorder()
		{

			_waveFormat = new WaveFormat(8000, 1);

			_waveIn = new WaveIn(WaveCallbackInfo.FunctionCallback());

			_waveIn.DataAvailable += waveIn_DataAvailable;
			_waveIn.WaveFormat = _waveFormat;
		}

		/// <summary>
		/// Comment
		/// </summary>
		public string AudioFileName
		{
			get { return _waveFileWriter == null ? null : _waveFileWriter.Filename; }
		}



		public void StartRecording()
		{
			var tempFileName = Path.ChangeExtension(Path.GetTempFileName(), ".wav");

			_waveFileWriter = new WaveFileWriter(tempFileName, _waveFormat);

			_waveIn.StartRecording();

		}

		public void StopRecording()
		{
			_waveIn.StopRecording();
			_waveFileWriter.Flush();
			_waveFileWriter.Close();
			//_waveFileWriter.Dispose();
			//_waveFileWriter = null;
		}

		void waveIn_DataAvailable(object sender, WaveInEventArgs e)
		{
			_waveFileWriter.Write(e.Buffer, 0, e.Buffer.Length);

			// todo: call this when you play frame to speakers
			//enhancer.RegisterFramePlayed(....);

			//_enhancer.Write(e.Buffer); // frite signal recorded by microphone
			//bool moreFrames;
			//do
			//{
			//	short[] cancelBuffer = new short[e.Buffer.Length]; // contains cancelled audio signal
			//	if (_enhancer.Read(cancelBuffer, out moreFrames))
			//	{
			//		//_shorts.AddRange(cancelBuffer);
			//		foreach (var s in cancelBuffer)
			//		{
			//			_waveFileWriter.WriteSample(s);
			//		}

			//		//_waveFileWriter.WriteSamples(cancelBuffer, 0, cancelBuffer.Length);
			//		//_waveFileWriter.Write(cancelBuffer);
			//		//SendToNetwork(cancelBuffer);
			//	}
			//} while (moreFrames);
		}


		/// <summary>
		/// Führt anwendungsspezifische Aufgaben aus, die mit dem Freigeben, Zurückgeben oder Zurücksetzen von nicht verwalteten Ressourcen zusammenhängen.
		/// </summary>
		public void Dispose()
		{
			_waveFileWriter.Dispose();
			if (File.Exists(_waveFileWriter.Filename))
				File.Delete(_waveFileWriter.Filename);

			_waveIn.Dispose();
		}
	}
}