﻿// <auto-generated />
using System;
using BBlockTest.Models.Data.Weather;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BBlockTest.Migrations.WeatherDb
{
    [DbContext(typeof(WeatherDbContext))]
    [Migration("20240203145805_InitialMigForPostgre")]
    partial class InitialMigForPostgre
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BBlockTest.Aggregate.Product.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("BBlockTest.Aggregate.Weather.Weather", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Degree")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Weathers", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
