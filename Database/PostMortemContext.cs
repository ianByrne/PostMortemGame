using IanByrne.ResearchProject.Database.Seeds;
using IanByrne.ResearchProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IanByrne.ResearchProject.Database
{
    public class PostMortemContext : DbContext
    {
        public PostMortemContext() { }
        public PostMortemContext(DbContextOptions<PostMortemContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Bot> Bots { get; set; }
        public DbSet<Transcript> Transcripts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                // Local dev connection
                options.UseMySql("server=localhost;database=postmortem;user=root;password=mysql;SslMode=None");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasOne(u => u.Survey)
                    .WithOne(s => s.User)
                    .HasForeignKey<Survey>(s => s.UserId);
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.HasKey(s => s.Id);
            });

            modelBuilder.Entity<Bot>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.SeedBots();
            });

            modelBuilder.Entity<Transcript>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.HasOne(t => t.User)
                    .WithMany(u => u.Transcripts);
                entity.HasOne(t => t.Bot)
                    .WithMany(b => b.Transcripts);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
