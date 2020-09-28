using System;
using System.Collections.Generic;

namespace IanByrne.ResearchProject.Shared.Models
{
    public class User
    {
        public User() { }

        public User(Guid cookieId, GameMode gameMode)
        {
            CookieId = cookieId;
            GameMode = gameMode;
        }

        public uint Id { get; set; }
        public Guid CookieId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? WinDateTime { get; set; }
        public GameMode GameMode { get; set; }
        public bool UsedDevCommand { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual ICollection<Transcript> Transcripts { get; set; }
    }
}
