using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace WitAIClient
{
	public class WitMessageRequestTask// : Task<string>
	{
		private const String WIT_URL = "https://api.wit.ai/message?q=";
		//private const String WIT_URL = "https://api.wit.ai/message?v=" + VERSION + "&q=";
		private const String VERSION = "20141022";
		private const String AUTHORIZATION_HEADER = "Authorization";
		private const String BEARER_FORMAT = "Bearer {0}";
		private const String ACCEPT_VERSION = "application/vnd.wit." + VERSION;
		private readonly String _accessToken;

		public WitMessageRequestTask() { _accessToken = File.ReadAllText("accessToken.txt"); }

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

		public ResultFromMessageRequest DoWork(String text)
		{
			String response = null;
			Console.WriteLine("Requesting ...." + text[0]);
			String getUrl = String.Format("{0}{1}", WIT_URL, HttpUtility.UrlEncode(text));
			Uri url = new Uri(getUrl);
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Headers.Add(AUTHORIZATION_HEADER, String.Format(BEARER_FORMAT, _accessToken));
			request.Accept = ACCEPT_VERSION;
			using (var webResponse = request.GetResponse())
			using (var responseStream = webResponse.GetResponseStream())
			{
				response = convertStreamToString(responseStream);
			}
			var resultFromMessageRequest = JsonSerializer.Create().Deserialize<ResultFromMessageRequest>(new JsonTextReader(new StringReader(response)));
			return resultFromMessageRequest;
		}
	}
}