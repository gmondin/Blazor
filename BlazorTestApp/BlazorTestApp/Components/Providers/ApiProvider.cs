using System.Text;
using System.Net.Http.Json;
using System.Text.Json;
using BlazorTestApp.Components.Components.Loading;

namespace BlazorTestAppComponents.Providers
{
	public class ApiProvider
	{
		private readonly HttpClient _httpClient;
		private readonly LoadingProvider _LoadingProvider;

		public ApiProvider(HttpClient httpClient, LoadingProvider loadingProvider)
		{
			_LoadingProvider = loadingProvider;
			_httpClient = httpClient;
		}

		/// <summary>
		/// get and automaticly put the loading on the screen
		/// return a new instance of the object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="method"></param>
		/// <param name="showLoading"></param>
		/// <returns></returns>
		public async Task<T> GetAsync<T>(string method, bool showLoading = true)
		{
			try
			{
				if (showLoading) _LoadingProvider.Show();
				var response = await _httpClient.GetAsync(method);
				if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<T>();
				else return Activator.CreateInstance<T>();
			}
			catch { return Activator.CreateInstance<T>(); }
			finally { if (showLoading) _LoadingProvider.Hide(); }
		}

		/// <summary>
		/// post and automaticly put the loading on the screen
		/// return a new instance of the object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="method"></param>
		/// <param name="objectToSend"></param>
		/// <param name="showLoading"></param>
		/// <returns></returns>
		public async Task<T?> PostAsync<T>(string method, object objectToSend, bool showLoading = true)
		{
			try
			{
				if (showLoading) _LoadingProvider.Show();
				var response = await _httpClient.PostAsJsonAsync(method, objectToSend);
				if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<T>();
				else return Activator.CreateInstance<T>();
			}
			catch { return Activator.CreateInstance<T>(); }
			finally { if (showLoading) _LoadingProvider.Hide(); }
		}

		/// <summary>
		/// put and automaticly put the loading on the screen
		/// return true if is ok and false if is error
		/// </summary>
		/// <param name="method"></param>
		/// <param name="objectToSend"></param>
		/// <param name="showLoading"></param>
		/// <returns></returns>
		public async Task<bool> PutAsync(string method, object objectToSend, bool showLoading = true)
		{
			try
			{
				if (showLoading) _LoadingProvider.Show();
				var content = JsonSerializer.Serialize(objectToSend);
				var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
				var response = await _httpClient.PutAsync(method, bodyContent);
				if (response.IsSuccessStatusCode) return true;
				else return false;
			}
			catch { return false; }
			finally { if (showLoading) _LoadingProvider.Hide(); }
		}

		/// <summary>
		/// delete and automaticly put the loading on the screen
		/// return true if is ok and false if is error
		/// </summary>
		/// <param name="method"></param>
		/// <param name="showLoading"></param>
		/// <returns></returns>
		public async Task<bool> DeleteAsync(string method, bool showLoading = true)
		{
			try
			{
				if (showLoading) _LoadingProvider.Show();
				var response = await _httpClient.DeleteAsync(method);
				if (response.IsSuccessStatusCode) return true;
				else return false;
			}
			catch { return false; }
			finally { if (showLoading) _LoadingProvider.Hide(); }
		}
	}
}

