using LearnWordsFast.DAL.Models;
using Microsoft.Data.Entity;

namespace LearnWordsFast.DAL.EF
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=learnwordsfast.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<WordAdditionalTranslation>()
                .HasKey(x => new { x.WordId, x.TranslationId});

            modelBuilder
                .Entity<WordAdditionalTranslation>()
                .HasOne(x => x.Word)
                .WithMany(x => x.AdditionalTranslations)
                .HasForeignKey(x => x.WordId);

            modelBuilder
                .Entity<WordAdditionalTranslation>()
                .HasOne(x => x.Translation)
                .WithMany()
                .HasForeignKey(x => x.TranslationId);

            modelBuilder
                .Entity<Word>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Word>()
                .Ignore(x => x.LastTraining)
                .Ignore(x => x.LastCorrectTraining);

            modelBuilder
                .Entity<Word>()
                .HasOne(x => x.Translation)
                .WithMany()
                .HasForeignKey(x => x.TranslationId)
                .IsRequired();

            modelBuilder
                .Entity<Word>()
                .HasOne(x => x.Language)
                .WithMany()
                .HasForeignKey(x => x.LanguageId);

            modelBuilder
                .Entity<Word>()
                .HasMany(x => x.TrainingHistories)
                .WithOne();

            modelBuilder
                .Entity<Language>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<TrainingHistory>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Translation>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<UserAdditionalLanguage>()
                .HasKey(x => new { x.UserId, x.LanguageId });

            modelBuilder
                .Entity<UserAdditionalLanguage>()
                .HasOne(x => x.User)
                .WithMany(x => x.AdditionalLanguages)
                .HasForeignKey(x => x.UserId);

            modelBuilder
                .Entity<UserAdditionalLanguage>()
                .HasOne(x => x.Language)
                .WithMany()
                .HasForeignKey(x => x.LanguageId);

            modelBuilder
                .Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<User>()
                .HasOne(x => x.MainLanguage)
                .WithMany()
                .HasForeignKey(x => x.MainLanguageId)
                .IsRequired();

            modelBuilder
                .Entity<User>()
                .HasOne(x => x.TrainingLanguage)
                .WithMany()
                .HasForeignKey(x => x.TrainingLanguageId)
                .IsRequired();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; } 
        public DbSet<Language> Languages { get; set; } 
        public DbSet<Translation> Translations { get; set; } 
    }
}