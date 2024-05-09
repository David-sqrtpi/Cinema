﻿// <auto-generated />
using System;
using Cinema.Services.Functions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cinema.Services.Functions.SqliteMigrations
{
    [DbContext(typeof(LiteFunctionsContext))]
    [Migration("20240506034213_Initial-Sqlite")]
    partial class InitialSqlite
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Cinema.Services.Functions.Models.Function", b =>
                {
                    b.Property<Guid>("FunctionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FunctionDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("FunctionId");

                    b.ToTable("Functions");
                });
#pragma warning restore 612, 618
        }
    }
}
