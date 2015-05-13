using System;

namespace WitAIClient
{
	public interface IWitCoordinator
	{
		void StopListening();
	}


public class WitMic {
    static public int SAMPLE_RATE = 16000;
	static private AudioFormat CHANNEL = AudioFormat.CHANNEL_IN_MONO;
	static private AudioFormat FORMAT = AudioFormat.ENCODING_PCM_16BIT;
    private bool _isRecording = false;
	private AudioRecord aRecorder = null;
	private PipedInputStream _inStream;
	private PipedOutputStream _outStream;
	IWitCoordinator _witCoordinator;
    protected bool _detectSpeechStop;

	//Handler _handler = new Handler() {
	//	public void handleMessage(Message msg) {
	//		_witCoordinator.stopListening();
	//	}
	//};


	//public native int VadInit();
	//public native int VadStillTalking(short[] arr, int length);
	//public native void VadClean();

    public WitMic(IWitCoordinator witCoordinator, bool detectSpeechStop) {
		//in = new PipedInputStream();
		//out = new PipedOutputStream();
		//in.connect(out);
        _witCoordinator = witCoordinator;
        _detectSpeechStop = detectSpeechStop;
    }

	
	public void startRecording()
    {
        if (!_isRecording) {
            aRecorder = getRecorder();
            if (aRecorder.getState() == AudioState.STATE_INITIALIZED) {
                _isRecording = true;
                aRecorder.startRecording();
                SamplesReaderThread s = new SamplesReaderThread(this, _outStream, getMinBufferSize());
                s.start();
            } else {
				Console.WriteLine("***AudioRecord not initialized, calling stop for cleaning!");
                stopRecording();
            }
        }
    }

    public void stopRecording()
    {
        if (_isRecording) {
            aRecorder.stop();
            aRecorder.release();
            _isRecording = false;
        }
    }


    public bool toggle()
    {
        bool started;

        if (!_isRecording) {
            startRecording();
            started = true;
        } else {
            stopRecording();
            started = false;
        }

        return started;
    }

    public bool isRecording() {
        return _isRecording;
    }


    public AudioRecord getRecorder()
    {
        int bufferSize;
        AudioRecord audioRecord;

        bufferSize = getMinBufferSize();
        audioRecord = new AudioRecord(
                MediaRecorder.AudioSource.MIC, SAMPLE_RATE,
                CHANNEL, FORMAT, bufferSize);

        return audioRecord;
    }

    protected int getMinBufferSize()
    {
        int bufferSize = AudioRecord.getMinBufferSize(SAMPLE_RATE,
                CHANNEL, FORMAT) * 10;

        return bufferSize;
    }

    public PipedInputStream getInputStream()
    {
        return _inStream;
    }


     class SamplesReaderThread
     {
		 private PipedOutputStream iOut;
        private int iBufferSize;
        private WitMic _witMic;
	     public SamplesReaderThread(WitMic witMic, PipedOutputStream outStream, int bufferSize)
	     {
	     iOut = outStream;
            iBufferSize = bufferSize;
            _witMic = witMic;
	     }

	     public void start()
	     {
	     
	     }

        public void run()
        {
			//int nb;
			//int readBufferSize = iBufferSize;
			//byte[] bytes = new byte[readBufferSize];
			//short[] buffer = new short[readBufferSize];
			//int vadResult;
			//int skippingSamples = 0;


			////android.os.Process.setThreadPriority(android.os.Process.THREAD_PRIORITY_URGENT_AUDIO);
			//VadInit();
			//try {
			//	while ((nb = aRecorder.read(buffer, 0, readBufferSize)) > 0) {

			//		if (skippingSamples < SAMPLE_RATE) {
			//			skippingSamples += nb;
			//		}
			//		if (skippingSamples >= SAMPLE_RATE && _detectSpeechStop == true) {
			//			vadResult = VadStillTalking(buffer, nb);
			//			if (vadResult == 0) {
			//				//Stop the microphone via a Handler so the stopListeing function
			//				// of the IWitCoordinator interface is called on the Wit.startListening
			//				//calling thread
			//				_handler.sendEmptyMessage(0);
			//			}
			//		}
			//		short2byte(buffer, nb, bytes);
			//		iOut.write(bytes, 0, nb * 2);
			//	}
			//	iOut.close();
			//} catch (IOException e) {
			//	Log.d("SamplesReaderThread", "IOException: " + e.getMessage());
			//}
			//_witMic.VadClean();
        }

        protected void short2byte(short[] shorts, int nb, byte[] bytes)
        {
            for (int i = 0; i < nb; i++) {
                bytes[i * 2] = (byte)(shorts[i] & 0xff);
                bytes[i * 2 + 1] = (byte)((shorts[i] >> 8) & 0xff);
            }
        }
     }

	public class PipedInputStream
	{
	}

	public enum AudioFormat
	{
		CHANNEL_IN_MONO,
		ENCODING_PCM_16BIT
	}

	public class MediaRecorder
	{
		public class AudioSource
		{
			public static object MIC;
		}
	}

	internal class PipedOutputStream
	{
	}

	public class AudioRecord
	{
		public AudioRecord(object mic, int sampleRate, AudioFormat channel, AudioFormat format, int bufferSize)
		{
		
		}


		public AudioState getState()
		{
			return AudioState.STATE_INITIALIZED;
		}

		public void startRecording()
		{
		
		}

		public void stop()
		{
		
		}

		public void release()
		{
		
		}

		public static int getMinBufferSize(int sampleRate, AudioFormat channel, AudioFormat format) { return 0; }
	}

	public enum AudioState
	{
		STATE_INITIALIZED
	}
}


}