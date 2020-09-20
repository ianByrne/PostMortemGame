using IanByrne.ResearchProject.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IanByrne.ResearchProject.Database.Seeds
{
    public static class BotsSeeder
    {
        public static EntityTypeBuilder<Bot> SeedBots(this EntityTypeBuilder<Bot> entity)
        {
            entity.HasData(
                new Bot()
                {
                    Id = 1,
                    Name = "Reggie"
                });

            entity.HasData(
                new Bot()
                {
                    Id = 2,
                    Name = "Cow"
                });

            entity.HasData(
                new Bot()
                {
                    Id = 3,
                    Name = "Olive"
                });

            entity.HasData(
                new Bot()
                {
                    Id = 4,
                    Name = "Clarence"
                });

            return entity;
        }
    }
}
