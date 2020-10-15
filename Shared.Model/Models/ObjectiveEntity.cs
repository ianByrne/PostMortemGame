using System.Collections.Generic;

namespace IanByrne.ResearchProject.Shared.Models
{
    public class ObjectiveEntity
    {
        public ObjectiveEntity() { }

        public ObjectiveEntity(Objective objective)
        {
            Text = objective.Text;
            Target = objective.Target;
            RequiredFacts = objective.RequiredFacts == null || objective.RequiredFacts.Count == 0 ? null : string.Join(",", objective.RequiredFacts);
            Done = objective.Done;
        }

        public uint Id { get; set; }
        public string Text { get; set; }
        public MapLocation Target { get; set; }
        public string RequiredFacts { get; set; }
        public bool Done { get; set; }
        public virtual User User { get; set; }
    }
}
