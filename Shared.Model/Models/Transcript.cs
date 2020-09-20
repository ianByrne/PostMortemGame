using System;

namespace IanByrne.ResearchProject.Shared.Models
{
    public class Transcript
    {
        public uint Id { get; set; }
        public DateTime DateTime { get; set; }
        public TranscriptMessageDirection Direction { get; set; }
        public string Message { get; set; }
        public virtual User User { get; set; }
        public virtual Bot Bot { get; set; }
    }
}
