﻿// <auto-generated />
using System;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240905155222_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Api.Models.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AnonymousUserName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("JobCategoryId")
                        .HasColumnType("uuid");

                    b.Property<int>("JobUserType")
                        .HasColumnType("integer");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SalaryType")
                        .HasColumnType("integer");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("JobCategoryId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Api.Models.JobCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("JobCategories");
                });

            modelBuilder.Entity("Api.Models.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("JobSkill", b =>
                {
                    b.Property<Guid>("JobsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SkillsId")
                        .HasColumnType("uuid");

                    b.HasKey("JobsId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("JobSkill");
                });

            modelBuilder.Entity("Api.Models.Job", b =>
                {
                    b.HasOne("Api.Models.JobCategory", "Category")
                        .WithMany("Jobs")
                        .HasForeignKey("JobCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("JobSkill", b =>
                {
                    b.HasOne("Api.Models.Job", null)
                        .WithMany()
                        .HasForeignKey("JobsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.JobCategory", b =>
                {
                    b.Navigation("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
