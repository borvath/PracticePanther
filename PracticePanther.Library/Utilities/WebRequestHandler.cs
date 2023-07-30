using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PracticePanther.Library.Utilities;

public class WebRequestHandler {
	private const string host = "localhost";
	private const string port = "7216";
	private HttpClient Client { get; }
    
	public WebRequestHandler() {
		Client = new HttpClient();
	}
	public async Task<string?> Get(string url) {
		try {
			return await Client.GetStringAsync($"https://{host}:{port}{url}").ConfigureAwait(false);
		}
		catch (Exception) { } return null;
	}
	public async Task<string?> Delete(string url) {
		try {
			using var request = new HttpRequestMessage(HttpMethod.Delete, $"https://{host}:{port}{url}");
			using HttpResponseMessage response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
			return (response.IsSuccessStatusCode) ? await response.Content.ReadAsStringAsync() : "ERROR";
		}
		catch (Exception) { } return null;
	}
	public async Task<string> Post(string url, object obj)
	{
		using var request = new HttpRequestMessage(HttpMethod.Post, $"https://{host}:{port}{url}");
		request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
		using HttpResponseMessage response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
		return (response.IsSuccessStatusCode) ? await response.Content.ReadAsStringAsync() : "ERROR";
	}
}
