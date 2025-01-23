﻿// <auto-generated />
using System;
using ASbackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ASbackend.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20250121170739_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ASbackend.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("adress")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("age")
                        .HasColumnType("integer");

                    b.Property<string>("cpf")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("duedate")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("fullname")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("injuryhistory")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("numberemergency")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("numberphone")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
