using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using LearnWordsFast.DAL.EF;

namespace LearnWordsFast.DAL.EF.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20160131163507_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("LearnWordsFast.DAL.Models.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");
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
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.Translation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("LanguageId");

                    b.Property<string>("TranslationText");

                    b.HasKey("Id");
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
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.UserAdditionalLanguage", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("LanguageId");

                    b.HasKey("UserId", "LanguageId");
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
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.WordAdditionalTranslation", b =>
                {
                    b.Property<Guid>("WordId");

                    b.Property<Guid>("TranslationId");

                    b.HasKey("WordId", "TranslationId");
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
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.User", b =>
                {
                    b.HasOne("LearnWordsFast.DAL.Models.Language")
                        .WithMany()
                        .HasForeignKey("MainLanguageId");

                    b.HasOne("LearnWordsFast.DAL.Models.Language")
                        .WithMany()
                        .HasForeignKey("TrainingLanguageId");
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.UserAdditionalLanguage", b =>
                {
                    b.HasOne("LearnWordsFast.DAL.Models.Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("LearnWordsFast.DAL.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.Word", b =>
                {
                    b.HasOne("LearnWordsFast.DAL.Models.Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("LearnWordsFast.DAL.Models.Translation")
                        .WithMany()
                        .HasForeignKey("TranslationId");

                    b.HasOne("LearnWordsFast.DAL.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LearnWordsFast.DAL.Models.WordAdditionalTranslation", b =>
                {
                    b.HasOne("LearnWordsFast.DAL.Models.Translation")
                        .WithMany()
                        .HasForeignKey("TranslationId");

                    b.HasOne("LearnWordsFast.DAL.Models.Word")
                        .WithMany()
                        .HasForeignKey("WordId");
                });
        }
    }
}
