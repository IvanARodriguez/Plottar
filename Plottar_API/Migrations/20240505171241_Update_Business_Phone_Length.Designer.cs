﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plottar_API.Data;

#nullable disable

namespace Plottar_API.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240505171241_Update_Business_Phone_Length")]
    partial class Update_Business_Phone_Length
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Plottar_API.Models.Business", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ImageUrl")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Businesses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("32754ab1-20e1-4c42-a4b0-7066f5c0db20"),
                            Address = "3801 Vitruvian Way",
                            City = "Addison",
                            Country = "United States",
                            CreationDate = new DateTime(2024, 5, 5, 17, 12, 40, 579, DateTimeKind.Utc).AddTicks(6225),
                            Name = "Hitab",
                            PostalCode = "38001",
                            State = "Texas",
                            UpdatedDate = new DateTime(2024, 5, 5, 17, 12, 40, 579, DateTimeKind.Utc).AddTicks(6227)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
