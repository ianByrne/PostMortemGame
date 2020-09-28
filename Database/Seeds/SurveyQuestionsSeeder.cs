//using IanByrne.ResearchProject.Shared.Models;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace IanByrne.ResearchProject.Database.Seeds
//{
//    public static class SurveyQuestionsSeeder
//    {
//        public static EntityTypeBuilder<SurveyQuestion> SeedSurveyQuestions(this EntityTypeBuilder<SurveyQuestion> entity)
//        {
//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 1,
//                    Type = SurveyQuestionType.Age,
//                    Question = "Age"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 2,
//                    Type = SurveyQuestionType.Gender,
//                    Question = "Gender"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 3,
//                    Type = SurveyQuestionType.LikertScale_ALot,
//                    Question = "To what extent did the game hold your attention?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 4,
//                    Type = SurveyQuestionType.LikertScale_ALot,
//                    Question = "To what extent did you feel you were focused on the game?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 5,
//                    Type = SurveyQuestionType.LikertScale_ALot,
//                    Question = "How much effort did you put into playing the game?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 6,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "Did you feel that you were trying you best?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 7,
//                    Type = SurveyQuestionType.LikertScale_ALot,
//                    Question = "To what extent did you lose track of time?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 8,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "To what extent did you feel consciously aware of being in the real world whilst playing?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 9,
//                    Type = SurveyQuestionType.LikertScale_ALot,
//                    Question = "To what extent did you forget about your everyday concerns?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 10,
//                    Type = SurveyQuestionType.LikertScale_VeryAware,
//                    Question = "To what extent were you aware of yourself in your surroundings?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 11,
//                    Type = SurveyQuestionType.LikertScale_ALot,
//                    Question = "To what extent did you notice events taking place around you?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 12,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "Did you feel the urge at any point to stop playing and see what was happening around you?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 13,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "To what extent did you feel that you were interacting with the game environment?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 14,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "To what extent did you feel as though you were separated from your real-world environment?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 15,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "To what extent did you feel that the game was something you were experiencing, rather than something you were just doing?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 16,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "To what extent was your sense of being in the game environment stronger than your sense of being in the real world?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 17,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "At any point did you find yourself become so involved that you were unaware you were even using controls?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 18,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "To what extent did you feel as though you were moving through the game according to you own will?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 19,
//                    Type = SurveyQuestionType.LikertScale_VeryDifficult,
//                    Question = "To what extent did you find the game challenging?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 20,
//                    Type = SurveyQuestionType.LikertScale_ALot,
//                    Question = "Were there any times during the game in which you just wanted to give up?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 21,
//                    Type = SurveyQuestionType.LikertScale_ALot,
//                    Question = "To what extent did you feel motivated while playing?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 22,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "To what extent did you find the game easy?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 23,
//                    Type = SurveyQuestionType.LikertScale_ALot,
//                    Question = "To what extent did you feel like you were making progress towards the end of the game?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 24,
//                    Type = SurveyQuestionType.LikertScale_VeryWell,
//                    Question = "How well do you think you performed in the game?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 25,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "To what extent did you feel emotionally attached to the game?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 26,
//                    Type = SurveyQuestionType.LikertScale_ALot,
//                    Question = "To what extent were you interested in seeing how the game's events would progress?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 27,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "How much did you want to \"win\" the game?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 28,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "Were you in suspense about whether or not you would win or lose the game?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 29,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "At any point did you find yourself become so involved that you wanted to speak to the game directly?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 30,
//                    Type = SurveyQuestionType.LikertScale_ALot,
//                    Question = "To what extent did you enjoy the graphics and the imagery?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 31,
//                    Type = SurveyQuestionType.LikertScale_ALot,
//                    Question = "How much would you say you enjoyed playing the game?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 32,
//                    Type = SurveyQuestionType.LikertScale_VeryMuchSo,
//                    Question = "When interrupted, were you disappointed that the game was over?"
//                });

//            entity.HasData(
//                new SurveyQuestion()
//                {
//                    Id = 33,
//                    Type = SurveyQuestionType.LikertScale_DefinitelyYes,
//                    Question = "Would you like to play the game again?"
//                });

//            return entity;
//        }
//    }
//}
