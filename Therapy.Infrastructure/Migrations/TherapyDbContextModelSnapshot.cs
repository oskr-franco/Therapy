﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Therapy.Infrastructure.Data;

#nullable disable

namespace Therapy.Infrastructure.Migrations
{
    [DbContext(typeof(TherapyDbContext))]
    partial class TherapyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Therapy.Domain.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Instructions")
                        .HasMaxLength(8000)
                        .HasColumnType("VARCHAR");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(44)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR");

                    b.Property<int?>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserTypeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.WorkoutExercise", b =>
                {
                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("time");

                    b.Property<short?>("Order")
                        .HasColumnType("smallint");

                    b.Property<short>("Reps")
                        .HasColumnType("smallint");

                    b.Property<short>("Sets")
                        .HasColumnType("smallint");

                    b.HasKey("WorkoutId", "ExerciseId");

                    b.HasIndex("ExerciseId");

                    b.ToTable("WorkoutExercises");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.Media", b =>
                {
                    b.HasOne("Therapy.Domain.Entities.Exercise", "Exercise")
                        .WithMany("Media")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("Therapy.Domain.Entities.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("Therapy.Domain.Entities.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.User", b =>
                {
                    b.HasOne("Therapy.Domain.Entities.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("UserTypeId");

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.WorkoutExercise", b =>
                {
                    b.HasOne("Therapy.Domain.Entities.Exercise", "Exercise")
                        .WithMany("WorkoutExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Therapy.Domain.Entities.Workout", "Workout")
                        .WithMany("WorkoutExercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.Exercise", b =>
                {
                    b.Navigation("Media");

                    b.Navigation("WorkoutExercises");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.User", b =>
                {
                    b.Navigation("RefreshToken")
                        .IsRequired();
                });

            modelBuilder.Entity("Therapy.Domain.Entities.UserType", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Therapy.Domain.Entities.Workout", b =>
                {
                    b.Navigation("WorkoutExercises");
                });
#pragma warning restore 612, 618
        }
    }
}
