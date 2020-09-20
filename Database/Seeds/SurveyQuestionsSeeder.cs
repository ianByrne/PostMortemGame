using IanByrne.ResearchProject.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IanByrne.ResearchProject.Database.Seeds
{
    public static class SurveyQuestionsSeeder
    {
        public static EntityTypeBuilder<SurveyQuestion> SeedSurveyQuestions(this EntityTypeBuilder<SurveyQuestion> entity)
        {
            entity.HasData(
                new SurveyQuestion()
                {
                    Id = 1,
                    Type = SurveyQuestionType.Age,
                    Question = "Age"
                });

            entity.HasData(
                new SurveyQuestion()
                {
                    Id = 2,
                    Type = SurveyQuestionType.Gender,
                    Question = "Gender"
                });

            return entity;
        }
    }
}
