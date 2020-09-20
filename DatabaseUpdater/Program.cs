using IanByrne.ResearchProject.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IanByrne.ResearchProject.DatabaseUpdater
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*
            ---EF commands (because I ALWAYS forget them)---
            Add-Migration MigrationName -StartupProject DatabaseUpdater -Project Database
            Update-Database -StartupProject DatabaseUpdater -Project Database
            Remove-Migration -StartupProject DatabaseUpdater -Project Database
            */

            using (var db = new PostMortemContext())
            {
                await db.Database.MigrateAsync();

                await db.SaveChangesAsync();
            }
        }
    }
}
