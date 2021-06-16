﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Students.DAL.Implementation.EF;

namespace Students.DAL.Implementation.Migrations
{
    [DbContext(typeof(StudentsContext))]
    partial class StudentsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Students.DAL.Entities.BoxingGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoachId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BoxingGroups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CoachId = "19759de3-ce1d-4cfd-8340-4e64eb245eb4",
                            Name = "Vityaz"
                        },
                        new
                        {
                            Id = 2,
                            CoachId = "19759de3-ce1d-4cfd-8340-4e64eb245eb4",
                            Name = "Warrior"
                        },
                        new
                        {
                            Id = 3,
                            CoachId = "060342c3-9dc3-4597-bae1-9f19c991ebe9",
                            Name = "Sarmat"
                        });
                });

            modelBuilder.Entity("Students.DAL.Entities.MedicalCertificate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClinicName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("datetime2");

                    b.Property<int>("Result")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("MedicalCertificates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClinicName = "Polyclinic 4",
                            DateOfIssue = new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 1
                        },
                        new
                        {
                            Id = 2,
                            ClinicName = "Polyclinic 4",
                            DateOfIssue = new DateTime(2020, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 0,
                            StudentId = 1
                        },
                        new
                        {
                            Id = 3,
                            ClinicName = "Polyclinic 13",
                            DateOfIssue = new DateTime(2019, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 1
                        },
                        new
                        {
                            Id = 4,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2018, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 2
                        },
                        new
                        {
                            Id = 5,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 0,
                            StudentId = 2
                        },
                        new
                        {
                            Id = 6,
                            ClinicName = "Polyclinic 4",
                            DateOfIssue = new DateTime(2020, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 0,
                            StudentId = 3
                        },
                        new
                        {
                            Id = 7,
                            ClinicName = "Polyclinic 1",
                            DateOfIssue = new DateTime(2021, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 3
                        },
                        new
                        {
                            Id = 8,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 4
                        },
                        new
                        {
                            Id = 9,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 5
                        },
                        new
                        {
                            Id = 10,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 6
                        },
                        new
                        {
                            Id = 11,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 7
                        },
                        new
                        {
                            Id = 12,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 8
                        },
                        new
                        {
                            Id = 13,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 9
                        },
                        new
                        {
                            Id = 14,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 10
                        },
                        new
                        {
                            Id = 15,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 11
                        },
                        new
                        {
                            Id = 16,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 12
                        },
                        new
                        {
                            Id = 17,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 13
                        },
                        new
                        {
                            Id = 18,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 14
                        },
                        new
                        {
                            Id = 19,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 15
                        },
                        new
                        {
                            Id = 20,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 16
                        },
                        new
                        {
                            Id = 21,
                            ClinicName = "VODC",
                            DateOfIssue = new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Result = 1,
                            StudentId = 17
                        });
                });

            modelBuilder.Entity("Students.DAL.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BornDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("BoxingGroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfEntry")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfFights")
                        .HasColumnType("int");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BoxingGroupId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BornDate = new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 1,
                            DateOfEntry = new DateTime(2015, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Height = 175,
                            Name = "Vasiliy",
                            NumberOfFights = 3,
                            Patronymic = "Konstantinovich",
                            Surname = "Sychev",
                            Weight = 88.0
                        },
                        new
                        {
                            Id = 2,
                            BornDate = new DateTime(1991, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 2,
                            DateOfEntry = new DateTime(2014, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Height = 183,
                            Name = "Igor",
                            NumberOfFights = 5,
                            Surname = "Zhuravlev",
                            Weight = 70.0
                        },
                        new
                        {
                            Id = 3,
                            BornDate = new DateTime(2001, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 1,
                            DateOfEntry = new DateTime(2018, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Height = 170,
                            Name = "Ivan",
                            NumberOfFights = 2,
                            Surname = "Pavlov",
                            Weight = 66.0
                        },
                        new
                        {
                            Id = 4,
                            BornDate = new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 2,
                            DateOfEntry = new DateTime(2013, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Height = 174,
                            Name = "Andrew",
                            NumberOfFights = 10,
                            Patronymic = "Sergeevich",
                            Surname = "Solovyev",
                            Weight = 72.0
                        },
                        new
                        {
                            Id = 5,
                            BornDate = new DateTime(1998, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 3,
                            DateOfEntry = new DateTime(2018, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 0,
                            Height = 165,
                            Name = "Vika",
                            NumberOfFights = 4,
                            Surname = "Zhukova",
                            Weight = 55.0
                        },
                        new
                        {
                            Id = 6,
                            BornDate = new DateTime(2001, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 2,
                            DateOfEntry = new DateTime(2018, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Height = 172,
                            Name = "Ivan",
                            NumberOfFights = 3,
                            Surname = "Shabanov",
                            Weight = 66.0
                        },
                        new
                        {
                            Id = 7,
                            BornDate = new DateTime(2000, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 3,
                            DateOfEntry = new DateTime(2020, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Height = 175,
                            Name = "Vlad",
                            NumberOfFights = 1,
                            Patronymic = "Sergeevich",
                            Surname = "Safonov",
                            Weight = 74.0
                        },
                        new
                        {
                            Id = 8,
                            BornDate = new DateTime(2000, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 1,
                            DateOfEntry = new DateTime(2016, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 0,
                            Height = 173,
                            Name = "Anastasia",
                            NumberOfFights = 6,
                            Patronymic = "Antonovna",
                            Surname = "Efimova",
                            Weight = 60.0
                        },
                        new
                        {
                            Id = 9,
                            BornDate = new DateTime(2000, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 2,
                            DateOfEntry = new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 0,
                            Height = 180,
                            Name = "Viktoria",
                            NumberOfFights = 1,
                            Surname = "Narkevich",
                            Weight = 60.0
                        },
                        new
                        {
                            Id = 10,
                            BornDate = new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 2,
                            DateOfEntry = new DateTime(2016, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Height = 178,
                            Name = "Dmitry",
                            NumberOfFights = 4,
                            Patronymic = "Dmitrievich",
                            Surname = "Kustovinov",
                            Weight = 69.0
                        },
                        new
                        {
                            Id = 11,
                            BornDate = new DateTime(1989, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 1,
                            DateOfEntry = new DateTime(2012, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Height = 166,
                            Name = "Alexey",
                            NumberOfFights = 10,
                            Surname = "Fedorov",
                            Weight = 54.0
                        },
                        new
                        {
                            Id = 12,
                            BornDate = new DateTime(1995, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 1,
                            DateOfEntry = new DateTime(2017, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Height = 169,
                            Name = "Evgeniy",
                            NumberOfFights = 6,
                            Surname = "Baranin",
                            Weight = 57.0
                        },
                        new
                        {
                            Id = 13,
                            BornDate = new DateTime(2003, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 3,
                            DateOfEntry = new DateTime(2015, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Height = 170,
                            Name = "Alexander",
                            NumberOfFights = 5,
                            Surname = "Kirillov",
                            Weight = 74.0
                        },
                        new
                        {
                            Id = 14,
                            BornDate = new DateTime(2003, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 3,
                            DateOfEntry = new DateTime(2012, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Height = 165,
                            Name = "Nikolay",
                            NumberOfFights = 6,
                            Surname = "Leshev",
                            Weight = 67.0
                        },
                        new
                        {
                            Id = 15,
                            BornDate = new DateTime(2004, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 1,
                            DateOfEntry = new DateTime(2016, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 0,
                            Height = 170,
                            Name = "Valeria",
                            NumberOfFights = 7,
                            Surname = "Malahova",
                            Weight = 52.0
                        },
                        new
                        {
                            Id = 16,
                            BornDate = new DateTime(2000, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 2,
                            DateOfEntry = new DateTime(2016, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 0,
                            Height = 180,
                            Name = "Julia",
                            NumberOfFights = 5,
                            Surname = "Belikova",
                            Weight = 63.0
                        },
                        new
                        {
                            Id = 17,
                            BornDate = new DateTime(1990, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 3,
                            DateOfEntry = new DateTime(2010, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 0,
                            Height = 174,
                            Name = "Tatyana",
                            NumberOfFights = 10,
                            Surname = "Lelikova",
                            Weight = 57.0
                        });
                });

            modelBuilder.Entity("Students.DAL.Entities.MedicalCertificate", b =>
                {
                    b.HasOne("Students.DAL.Entities.Student", "Student")
                        .WithMany("MedicalCertificates")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Students.DAL.Entities.Student", b =>
                {
                    b.HasOne("Students.DAL.Entities.BoxingGroup", "BoxingGroup")
                        .WithMany("Students")
                        .HasForeignKey("BoxingGroupId");

                    b.Navigation("BoxingGroup");
                });

            modelBuilder.Entity("Students.DAL.Entities.BoxingGroup", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Students.DAL.Entities.Student", b =>
                {
                    b.Navigation("MedicalCertificates");
                });
#pragma warning restore 612, 618
        }
    }
}
