using dadjokes.Clients;
using dadjokes.Dtos;
using dadjokes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace dadjokes.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DadJokeController : ControllerBase
	{
		
		private readonly ILogger<DadJokeController> _logger;
		private readonly IDadJokesClient _client;

		public DadJokeController(ILogger<DadJokeController> logger, IDadJokesClient client)
		{
			_logger = logger;
			_client = client;
		}

		[Route("random")]
		[HttpGet]
		[ProducesResponseType(typeof(Joke), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<Joke>> GetRandomJoke()
		{
			var joke = await _client.GetRandomJokeAsync();
			return Ok(new DadJokeResponse { Joke = joke });
		}

		[Route("count")]
		[HttpGet]
		public async Task<ActionResult<Joke>> GetJoke()
		{
			var count = await _client.GetJokeCountAsync();
			return Ok(new DadJokeCountResponse { Count = count.GetValueOrDefault(0) });
		}
	}
}