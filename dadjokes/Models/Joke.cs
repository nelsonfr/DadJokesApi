namespace dadjokes.Models
{
	public class Joke
	{
		public string _id { get; set; }
		public string Setup { get; set; }
		public string PunchLine { get; set; }
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
