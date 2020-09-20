using System;
using System.Collections;
using System.Collections.Generic;

namespace IanByrne.ResearchProject.Shared.Models
{
    public class SurveyQuestion
    {
        public uint Id { get; set; }
        public string Question { get; set; }
        public SurveyQuestionType Type { get; set; }
        public virtual ICollection<SurveyAnswer> Answers { get; set; }
    }
}
