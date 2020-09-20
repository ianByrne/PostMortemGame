namespace IanByrne.ResearchProject.Shared.Models
{
    public class SendMessageRequest
    {
        public string UserCookieId { get; set; }
        public string BotName { get; set; }
        public string Message { get; set; }
    }
}
