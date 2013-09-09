using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WebServer
{
	class Program
	{
		static void Main(string[] args)
		{
			var server = new HttpListener();
			server.Prefixes.Add("http://+:8080/test/");
			server.Stop();
			server.Start();

			server.BeginGetContext(req =>
			{
				var listener = (HttpListener)req.AsyncState;

				var context = listener.EndGetContext(req);
				var request = context.Request;
				var response = context.Response;

				string responseString = "<html><body> Test!</body></html>";
				byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
				response.ContentLength64 = buffer.Length;
				var output = response.OutputStream;
				output.Write(buffer, 0, buffer.Length);
				// You must close the output stream.
				output.Close();
			}, server);

			Console.ReadKey();
		}
	}
}
