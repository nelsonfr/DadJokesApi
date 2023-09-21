using dadjokes.Models;

namespace dadjokes.Dtos
{
    public class DadJokeClientResponse
    {
        public bool Success { get; set; }
        public List<Joke> Body { get; set; } = new List<Joke> ();
    }
}
