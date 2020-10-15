using System.Collections.Generic;
using System.Linq;

namespace IanByrne.ResearchProject.Shared.Models
{
    public class Objective
    {
        public Objective() { }

        public Objective(ObjectiveEntity obj)
        {
            Text = obj.Text;
            Target = obj.Target;
            RequiredFacts = obj.RequiredFacts?.Split(',').ToList();
            Done = obj.Done;
        }

        public string Text { get; set; }
        public MapLocation Target { get; set; }
        public List<string> RequiredFacts { get; set; }
        public bool Done { get; set; }
    }
}
