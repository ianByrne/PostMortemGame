using System.ComponentModel.DataAnnotations;

namespace IanByrne.ResearchProject.Shared.Models
{
    public class Survey
    {
        public uint Id { get; set; }

        public uint UserId { get; set; }

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "To what extent did the game hold your attention?")]
        [LikertType(LikertType.Alot)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q1 { get; set; }

        [Display(Name = "To what extent did you feel you were focused on the game?")]
        [LikertType(LikertType.Alot)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q2 { get; set; }

        [Display(Name = "How much effort did you put into playing the game?")]
        [LikertType(LikertType.Alot)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q3 { get; set; }

        [Display(Name = "Did you feel that you were trying you best?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q4 { get; set; }

        [Display(Name = "To what extent did you lose track of time?")]
        [LikertType(LikertType.Alot)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q5 { get; set; }

        [Display(Name = "To what extent did you feel consciously aware of being in the real world whilst playing?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q6 { get; set; }

        [Display(Name = "To what extent did you forget about your everyday concerns?")]
        [LikertType(LikertType.Alot)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q7 { get; set; }

        [Display(Name = "To what extent were you aware of yourself in your surroundings?")]
        [LikertType(LikertType.VeryAware)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q8 { get; set; }

        [Display(Name = "To what extent did you notice events taking place around you?")]
        [LikertType(LikertType.Alot)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q9 { get; set; }

        [Display(Name = "Did you feel the urge at any point to stop playing and see what was happening around you?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q10 { get; set; }

        [Display(Name = "To what extent did you feel that you were interacting with the game environment?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q11 { get; set; }

        [Display(Name = "To what extent did you feel as though you were separated from your real-world environment?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q12 { get; set; }

        [Display(Name = "To what extent did you feel that the game was something you were experiencing, rather than something you were just doing?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q13 { get; set; }

        [Display(Name = "To what extent was your sense of being in the game environment stronger than your sense of being in the real world?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q14 { get; set; }

        [Display(Name = "At any point did you find yourself become so involved that you were unaware you were even using controls?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q15 { get; set; }

        [Display(Name = "To what extent did you feel as though you were moving through the game according to you own will?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q16 { get; set; }

        [Display(Name = "To what extent did you find the game challenging?")]
        [LikertType(LikertType.VeryDifficult)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q17 { get; set; }

        [Display(Name = "Were there any times during the game in which you just wanted to give up?")]
        [LikertType(LikertType.Alot)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q18 { get; set; }

        [Display(Name = "To what extent did you feel motivated while playing?")]
        [LikertType(LikertType.Alot)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q19 { get; set; }

        [Display(Name = "To what extent did you find the game easy?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q20 { get; set; }

        [Display(Name = "To what extent did you feel like you were making progress towards the end of the game?")]
        [LikertType(LikertType.Alot)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q21 { get; set; }

        [Display(Name = "How well do you think you performed in the game?")]
        [LikertType(LikertType.VeryWell)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q22 { get; set; }

        [Display(Name = "To what extent did you feel emotionally attached to the game?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q23 { get; set; }

        [Display(Name = "To what extent were you interested in seeing how the game's events would progress?")]
        [LikertType(LikertType.Alot)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q24 { get; set; }

        [Display(Name = "How much did you want to \"win\" the game?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q25 { get; set; }

        [Display(Name = "Were you in suspense about whether or not you would win or lose the game?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q26 { get; set; }

        [Display(Name = "At any point did you find yourself become so involved that you wanted to speak to the game directly?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q27 { get; set; }

        [Display(Name = "To what extent did you enjoy the graphics and the imagery?")]
        [LikertType(LikertType.Alot)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q28 { get; set; }

        [Display(Name = "How much would you say you enjoyed playing the game?")]
        [LikertType(LikertType.Alot)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q29 { get; set; }

        [Display(Name = "When interrupted, were you disappointed that the game was over?")]
        [LikertType(LikertType.VeryMuchSo)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q30 { get; set; }

        [Display(Name = "Would you like to play the game again?")]
        [LikertType(LikertType.DefinitelyYes)]
        [Range(1, 7, ErrorMessage = "Please enter a value")]
        [Required(ErrorMessage ="Please enter a value")]
        public int Q31 { get; set; }

        public virtual User User { get; set; }
    }
}
