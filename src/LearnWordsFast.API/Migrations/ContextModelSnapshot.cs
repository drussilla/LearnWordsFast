using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LearnWordsFast.DAL.EF;

namespace LearnWordsFast.API.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896");

            modelBuilder.Entity("LearnWordsFast.DAL.Models.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.TrainingHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsCorrect");

                    b.Property<float>("Score");

                    b.Property<int>("Type");

                    b.Property<Guid?>("WordId");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.ToTable("TrainingHistory");
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.Translation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("LanguageId");

                    b.Property<string>("TranslationText");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Translations");
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<Guid>("MainLanguageId");

                    b.Property<string>("Password");

                    b.Property<Guid>("TrainingLanguageId");

                    b.HasKey("Id");

                    b.HasIndex("MainLanguageId");

                    b.HasIndex("TrainingLanguageId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.UserAdditionalLanguage", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("LanguageId");

                    b.HasKey("UserId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAdditionalLanguage");
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.Word", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDateTime");

                    b.Property<string>("Context");

                    b.Property<Guid>("LanguageId");

                    b.Property<string>("Original");

                    b.Property<Guid>("TranslationId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("TranslationId");

                    b.HasIndex("UserId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.WordAdditionalTranslation", b =>
                {
                    b.Property<Guid>("WordId");

                    b.Property<Guid>("TranslationId");

                    b.HasKey("WordId", "TranslationId");

                    b.HasIndex("TranslationId");

                    b.HasIndex("WordId");

                    b.ToTable("WordAdditionalTranslation");
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.TrainingHistory", b =>
                {
                    b.HasOne("LearnWordsFast.DAL.Models.Word")
                        .WithMany()
                        .HasForeignKey("WordId");
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.Translation", b =>
                {
                    b.HasOne("LearnWordsFast.DAL.Models.Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.User", b =>
                {
                    b.HasOne("LearnWordsFast.DAL.Models.Language")
                        .WithMany()
                        .HasForeignKey("MainLanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LearnWordsFast.DAL.Models.Language")
                        .WithMany()
                        .HasForeignKey("TrainingLanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.UserAdditionalLanguage", b =>
                {
                    b.HasOne("LearnWordsFast.DAL.Models.Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LearnWordsFast.DAL.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.Word", b =>
                {
                    b.HasOne("LearnWordsFast.DAL.Models.Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LearnWordsFast.DAL.Models.Translation")
                        .WithMany()
                        .HasForeignKey("TranslationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LearnWordsFast.DAL.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.WordAdditionalTranslation", b =>
                {
                    b.HasOne("LearnWordsFast.DAL.Models.Translation")
                        .WithMany()
                        .HasForeignKey("TranslationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LearnWordsFast.DAL.Models.Word")
                        .WithMany()
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
