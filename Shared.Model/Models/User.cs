using System;
using System.Collections.Generic;

namespace IanByrne.ResearchProject.Shared.Models
{
    public class User
    {
        public uint Id { get; set; }
        public Guid CookieId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? WinDateTime { get; set; }
        public GameMode GameMode { get; set; }
        public virtual ICollection<SurveyAnswer> Answers { get; set; }
        public virtual ICollection<Transcript> Transcripts { get; set; }
    }
}
