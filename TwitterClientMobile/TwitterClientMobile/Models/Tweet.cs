namespace TwitterClientMobile.Models
{
    public class Tweet
    {
        public ulong StatusId { get; set; }

        public string ScreenName { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public string MediaUrl { get; set; }
    }
}
