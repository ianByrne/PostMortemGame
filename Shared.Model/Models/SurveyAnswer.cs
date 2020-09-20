using System;

namespace IanByrne.ResearchProject.Shared.Models
{
    public class SurveyAnswer
    {
        public uint Id { get; set; }
        public string Answer { get; set; }
        public virtual SurveyQuestion Question { get; set; }
        public virtual User User { get; set; }
    }
}
