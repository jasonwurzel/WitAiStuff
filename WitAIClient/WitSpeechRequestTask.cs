using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace WitAIClient
{
	public class WitSpeechRequestTask// : Task<string>
	{
		private const String WIT_SPEECH_URL = "https://api.wit.ai/speech";
		private const String VERSION = "20141022";
		private const String AUTHORIZATION_HEADER = "Authorization";
		private const String BEARER_FORMAT = "Bearer {0}";
		private const String ACCEPT_VERSION = "application/vnd.wit." + VERSION;
		private const String CONTENT_TYPE_HEADER = "Content-Type";
		private const String TRANSFER_ENCODING_HEADER = "Transfer-Encoding";
		private readonly String _accessToken;
		private String _contentType = "audio/wav";

		public WitSpeechRequestTask() { _accessToken = File.ReadAllText("accessToken.txt"); }
		
		public static String convertStreamToString(Stream inputStream)
		{
			StreamReader reader = new StreamReader(inputStream);
			StringBuilder sb = new StringBuilder();
			String line;

			while ((line = reader.ReadLine()) != null)
			{
				sb.Append(line);
			}

			return sb.ToString();
		}

		public ResultFromMessageRequest DoWork(Stream audioStream)
		{
			String response = null;
			Console.WriteLine("***Requesting ....");
			Uri url = new Uri(WIT_SPEECH_URL);
			var request = (HttpWebRequest) WebRequest.Create(url);
			request.Method = "POST";
			request.Headers.Add(AUTHORIZATION_HEADER, String.Format(BEARER_FORMAT, _accessToken));
			request.ContentType = _contentType;
			request.Accept = ACCEPT_VERSION;
			var requestStream = request.GetRequestStream();
			int bytesRead = 0;
			byte[] buffer = new byte[1024];
			while ((bytesRead = audioStream.Read(buffer, 0, buffer.Length)) != 0)
			{
				requestStream.Write(buffer, 0, bytesRead);
			}

			using (var webResponse = request.GetResponse())
			using (var responseStream = webResponse.GetResponseStream())
				response = convertStreamToString(responseStream);
			var resultFromMessageRequest = JsonSerializer.Create().Deserialize<ResultFromMessageRequest>(new JsonTextReader(new StringReader(response)));
			return resultFromMessageRequest;
		}
	}
}