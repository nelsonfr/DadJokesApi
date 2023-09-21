using dadjokes.Models;

namespace dadjokes.Clients
{
	public interface IDadJokesClient
	{
		Task<Joke?> GetRandomJokeAsync();

		Task<int> GetJokeCountAsync();
	}
}
