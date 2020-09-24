using IanByrne.ResearchProject.Database;
using System;
using System.Linq;

namespace IanByrne.ResearchProject.Shared.Models
{
    public static class UserExtensions
    {
        public static void EnsureCreated(this User user, PostMortemContext context)
        {
            var existingUser = context.Users.SingleOrDefault(x => x.CookieId == user.CookieId);

            if (existingUser == null)
            {
                context.Users.Add(new User()
                {
                    CookieId = user.CookieId,
                    CreatedDateTime = DateTime.UtcNow,
                    GameMode = user.GameMode
                });

                context.SaveChanges();
            }
        }

        public static void Save(this User user, PostMortemContext context)
        {
            var dbUser = context.Users.Single(x => x.CookieId == user.CookieId);
            dbUser.GameMode = user.GameMode;
            dbUser.UsedDevCommand = user.UsedDevCommand;

            context.SaveChanges();
        }
    }
}
