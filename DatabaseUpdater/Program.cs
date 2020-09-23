using IanByrne.ResearchProject.Database;
using Microsoft.EntityFrameworkCore;
using System;
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

            if(args == null || args.Length != 1)
            {
                throw new ArgumentException($"Expecting one argument with connection string");
            }

            using (var db = new PostMortemContext(args[0]))
            {
                await db.Database.MigrateAsync();

                await db.SaveChangesAsync();
            }
        }
    }
}
