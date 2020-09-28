using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IanByrne.ResearchProject.Shared.Models
{
    public class Survey
    {
        public uint Id { get; set; }

        public uint UserId { get; set; }

        [Description("Age")]
        [Required]
        public int Age { get; set; }

        [Description("Age")]
        public Gender Gender { get; set; }

        [Description("Age")]
        public int Q1 { get; set; }

        [Description("Age")]
        public int Q2 { get; set; }

        [Description("Age")]
        public int Q3 { get; set; }

        [Description("Age")]
        public int Q4 { get; set; }

        [Description("Age")]
        public int Q5 { get; set; }

        [Description("Age")]
        public int Q6 { get; set; }

        [Description("Age")]
        public int Q7 { get; set; }

        [Description("Age")]
        public int Q8 { get; set; }

        [Description("Age")]
        public int Q9 { get; set; }

        [Description("Age")]
        public int Q10 { get; set; }

        [Description("Age")]
        public int Q11 { get; set; }

        [Description("Age")]
        public int Q12 { get; set; }

        [Description("Age")]
        public int Q13 { get; set; }

        [Description("Age")]
        public int Q14 { get; set; }

        [Description("Age")]
        public int Q15 { get; set; }

        [Description("Age")]
        public int Q16 { get; set; }

        [Description("Age")]
        public int Q17 { get; set; }

        [Description("Age")]
        public int Q18 { get; set; }

        [Description("Age")]
        public int Q19 { get; set; }

        [Description("Age")]
        public int Q20 { get; set; }

        [Description("Age")]
        public int Q21 { get; set; }

        [Description("Age")]
        public int Q22 { get; set; }

        [Description("Age")]
        public int Q23 { get; set; }

        [Description("Age")]
        public int Q24 { get; set; }

        [Description("Age")]
        public int Q25 { get; set; }

        [Description("Age")]
        public int Q26 { get; set; }

        [Description("Age")]
        public int Q27 { get; set; }

        [Description("Age")]
        public int Q28 { get; set; }

        [Description("Age")]
        public int Q29 { get; set; }

        [Description("Age")]
        public int Q30 { get; set; }

        [Description("Age")]
        public int Q31 { get; set; }

        public virtual User User { get; set; }
    }
}
