using dadjokes.Dtos;
using dadjokes.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text.Json.Serialization;

namespace dadjokes.Clients
{
    public class DadJokesClient:IDadJokesClient
	{
        private readonly string _baseUrl;
		private readonly string _dadJokesKey;
		private readonly IConfiguration _configuration;
		private readonly ILogger<DadJokesClient> _logger;
		private readonly string _dadJokesHost;
		private readonly IRestClient _restClient;

		public DadJokesClient(IRestClient restClient, IConfiguration configuration, ILogger<DadJokesClient> logger)
        {            
            _restClient = restClient;
            _configuration = configuration;
            _logger = logger;
			_dadJokesHost = _configuration.GetValue<string>("DadJokes:Host");
			_baseUrl = $"https://{_dadJokesHost}";
			_dadJokesKey = _configuration.GetValue<string>("DadJokes:Key");

		}

        public async Task<Joke?> GetRandomJokeAsync()
        {
            var response = await GetGenericClientReponse<DadJokeClientResponse>("/random/joke");
            return response.Body.FirstOrDefault();
        }

        public async Task<int> GetJokeCountAsync()
        {
            var response = await GetGenericClientReponse<DadJokeCountClientResponse>("/joke/count");
            return response.Body;
        }


		private async Task<T> GetGenericClientReponse<T>(string requestUri)
		{
			var request = new RestRequest($"{_baseUrl+requestUri}", Method.Get);
			request.AddHeader("X-RapidAPI-Key", _dadJokesKey );
			request.AddHeader("X-RapidAPI-Host", _dadJokesHost);

			try
			{
				var response = await _restClient.ExecuteAsync(request);

				if (response != null && response.StatusCode == HttpStatusCode.OK)
				{
					string responseContent = response.Content ?? string.Empty;
					return JsonConvert.DeserializeObject<T>(responseContent);
				}
			}

			catch (Exception ex)
			{
				_logger.Log(LogLevel.Error, ex.Message);
				throw;
			}
			return default;
		}


	}
}
