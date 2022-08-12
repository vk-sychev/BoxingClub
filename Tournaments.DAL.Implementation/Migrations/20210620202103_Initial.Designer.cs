﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tournaments.DAL.Implementation.EF;

namespace Tournaments.DAL.Implementation.Migrations
{
    [DbContext(typeof(TournamentsContext))]
    [Migration("20210620202103_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tournaments.DAL.Entities.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsMedCertificateRequired")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Moscow",
                            Country = "Russia",
                            Date = new DateTime(2021, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsMedCertificateRequired = false,
                            Name = "Moscow boxing championship"
                        },
                        new
                        {
                            Id = 2,
                            City = "Voronezh",
                            Country = "Russia",
                            Date = new DateTime(2021, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsMedCertificateRequired = false,
                            Name = "Voronezh Boxing League"
                        },
                        new
                        {
                            Id = 3,
                            City = "Gomel",
                            Country = "Belarus",
                            Date = new DateTime(2021, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsMedCertificateRequired = true,
                            Name = "International Boxing Competition"
                        },
                        new
                        {
                            Id = 4,
                            City = "St. Petersburg",
                            Country = "Russia",
                            Date = new DateTime(2021, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsMedCertificateRequired = false,
                            Name = "International boxing tournament - Cup of the Governor of St. Petersburg"
                        });
                });

            modelBuilder.Entity("Tournaments.DAL.Entities.TournamentRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StudentHeight")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("StudentWeight")
                        .HasColumnType("int");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("TournamentRequests");
                });

            modelBuilder.Entity("Tournaments.DAL.Entities.TournamentRequest", b =>
                {
                    b.HasOne("Tournaments.DAL.Entities.Tournament", "Tournament")
                        .WithMany("TournamentRequests")
                        .HasForeignKey("TournamentId");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("Tournaments.DAL.Entities.Tournament", b =>
                {
                    b.Navigation("TournamentRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
