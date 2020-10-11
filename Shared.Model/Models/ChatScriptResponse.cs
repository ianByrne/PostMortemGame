namespace IanByrne.ResearchProject.Shared.Models
{
    public class ChatScriptResponse
    {
        public string[] DialogueOptions { get; set; }
        public string[] NewFacts { get; set; }
        public ChatScriptStats Stats { get; set; }
    }

    public class ChatScriptStats
    {
        public int Length { get; set; }
        public int Input { get; set; }
        public int UserFirstLine { get; set; }
    }
}
