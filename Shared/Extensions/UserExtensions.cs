using IanByrne.ResearchProject.Database;
using System;
using System.Linq;

namespace IanByrne.ResearchProject.Shared.Models
{
    public static class UserExtensions
    {
        public static void EnsureCreated(this User user)
        {
			using (var db = new PostMortemContext())
			{
				var existingUser = db.Users.SingleOrDefault(x => x.CookieId == user.CookieId);

				if (existingUser == null)
				{
					db.Users.Add(new User()
					{
						CookieId = user.CookieId,
						CreatedDateTime = DateTime.UtcNow,
						GameMode = user.GameMode
					});

					db.SaveChanges();
				}
			}
		}
    }
}
