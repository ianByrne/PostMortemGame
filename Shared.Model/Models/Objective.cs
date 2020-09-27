using System.Collections.Generic;

namespace IanByrne.ResearchProject.Shared.Models
{
    public class Objective
    {
        public string Text { get; set; }
        public MapLocation Target { get; set; }
        public List<string> RequiredFacts { get; set; }
        public bool Done { get; set; }
    }
}
