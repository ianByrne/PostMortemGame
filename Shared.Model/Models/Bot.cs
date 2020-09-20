using System.Collections;
using System.Collections.Generic;

namespace IanByrne.ResearchProject.Shared.Models
{
    public class Bot
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Transcript> Transcripts { get; set; }
    }
}
