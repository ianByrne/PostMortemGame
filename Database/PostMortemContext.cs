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
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
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
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<SurveyQuestion>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.SeedSurveyQuestions();
            });

            modelBuilder.Entity<SurveyAnswer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Answers);
                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers);
            });

            modelBuilder.Entity<Bot>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.SeedBots();
            });

            modelBuilder.Entity<Transcript>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transcripts);
                entity.HasOne(d => d.Bot)
                    .WithMany(p => p.Transcripts);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
