﻿// <auto-generated />
using System;
using Cinema.Services.Functions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cinema.Services.Functions.Migrations
{
    [DbContext(typeof(FunctionsContext))]
    partial class FunctionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cinema.Services.Functions.Models.Function", b =>
                {
                    b.Property<Guid>("FunctionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<DateTime>("FunctionDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("FunctionId")
                        .HasName("PK__Function__43D3C33AA0D85188");

                    b.ToTable("Function", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
