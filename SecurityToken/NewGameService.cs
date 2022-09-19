using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SecurityToken
{
    public static class NewGameService
    {
		const string url = "http://localhost:33828/api/spaceshipgame/newgame";

		public static Dictionary<string, List<string>> GameUsers = new Dictionary<string, List<string>>();

		public static string StartNewGame (List<string> users)
        {
			string gameId = GetResponsePost(url, "application/json", JsonConvert.SerializeObject(users));
			gameId = gameId.Replace("\"", "");
			GameUsers.Add(gameId, users);
			return gameId;
		}

		private static string GetResponsePost(string endPoint, string type, string data)
		{
			var http = (HttpWebRequest)WebRequest.Create(new Uri(endPoint));
			http.ContentType = type;
			http.Method = "POST";
			http.Accept = "application/json";

			byte[] bytes = Encoding.UTF8.GetBytes(data);

			using (Stream postData = http.GetRequestStream())
			{
				postData.Write(bytes, 0, bytes.Length);
				postData.Close();
			}

			try
			{
				WebResponse response = http.GetResponse();
				Stream stream = response.GetResponseStream();

				var sr = new StreamReader(stream);
				var responseBody = sr.ReadToEnd();

				return (string)(object)responseBody;
			}
			catch (Exception ex)
			{
				WebException wex = (WebException)ex;
				var s = wex.Response.GetResponseStream();
				string ss = "";
				int lastNum = 0;
				do
				{
					lastNum = s.ReadByte();
					ss += (char)lastNum;
				} while (lastNum != -1);
				s.Close();
				s = null;

				throw new Exception("ERROR: " + ss, ex);
			}

		}
	}
}
