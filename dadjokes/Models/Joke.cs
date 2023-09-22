namespace dadjokes.Models
{
	public class Joke
	{
		public string _id { get; set; } = string.Empty;
		public string Setup { get; set; } = string.Empty;
		public string PunchLine { get; set; } = string.Empty;
		public Author Author { get; set; }

		public int Date { get; set; }
		public DateTime PostedSince
		{
			get
			{
				return DateTime.UnixEpoch.AddSeconds(Date);
			}
		}
	}
}
