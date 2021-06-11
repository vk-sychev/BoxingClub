﻿// <auto-generated />
using System;
using BoxingClub.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BoxingClub.DAL.Migrations
{
    [DbContext(typeof(BoxingClubContext))]
    [Migration("20210504142010_ExtendingStudent")]
    partial class ExtendingStudent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BoxingClub.DAL.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BornDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "fda7dfec-9828-41b2-bd9c-53dccbef2bb8",
                            AccessFailedCount = 0,
                            BornDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "376b69ce-cb56-4818-8fc6-1766a3fd1056",
                            Email = "Manager1@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "MANAGER1@GMAIL.COM",
                            NormalizedUserName = "MANAGER1",
                            PasswordHash = "AQAAAAEAACcQAAAAECoctslF1IOzV2GuN7cR4jMDrjphvCQ5hot9EwS6FS2l6ArqkbL/uIpN6eY4PRoK6g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "15716645-b063-4e3b-a05b-66bdfd45f423",
                            TwoFactorEnabled = false,
                            UserName = "Manager1"
                        },
                        new
                        {
                            Id = "2d4254a5-7782-4b9c-a987-42a83d30669a",
                            AccessFailedCount = 0,
                            BornDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "9d63a534-74e2-4961-af01-cfe76832095e",
                            Email = "Manager2@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "MANAGER2@GMAIL.COM",
                            NormalizedUserName = "MANAGER2",
                            PasswordHash = "AQAAAAEAACcQAAAAEM0dbYapCDXjxbaJnkFIq8BPbsj2a4zTgyTxRTvKl1y511mJAujOS8t+MjI1XrDPEg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0592b015-ff51-43d6-8f9f-854cc298f32b",
                            TwoFactorEnabled = false,
                            UserName = "Manager2"
                        },
                        new
                        {
                            Id = "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef",
                            AccessFailedCount = 0,
                            BornDate = new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "33d4def5-32b5-4024-8d91-0ec34eb0582b",
                            Email = "Admin@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Vasya",
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEN3XUhBrY6ARrtgiaXHJYZFp+Csp4mF7y3A2E8ORK7xnVsMo9dBNZ8j2gFU5hZB7gg==",
                            Patronymic = "Konstantinovich",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "81323b35-8ebf-41ed-bca9-d5b91471be7b",
                            Surname = "Sychev",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        },
                        new
                        {
                            Id = "19759de3-ce1d-4cfd-8340-4e64eb245eb4",
                            AccessFailedCount = 0,
                            BornDate = new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "e7c1d2cc-c985-461e-a7ac-cdaf2b3cf91d",
                            Description = "CMS in boxing",
                            Email = "Coach1@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Pavel",
                            NormalizedEmail = "COACH1@GMAIL.COM",
                            NormalizedUserName = "COACH1",
                            PasswordHash = "AQAAAAEAACcQAAAAEN8AGfIBfrQecqSblJdFubADH3AqLNI2ZI8k6m6pv7bLMbZtTWaHxVx7LPt7PjC13Q==",
                            Patronymic = "Nikolayevich",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d9ffe769-7a38-4f3e-a011-caeeb7763c9e",
                            Surname = "Dorochin",
                            TwoFactorEnabled = false,
                            UserName = "Coach1"
                        },
                        new
                        {
                            Id = "060342c3-9dc3-4597-bae1-9f19c991ebe9",
                            AccessFailedCount = 0,
                            BornDate = new DateTime(1991, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "9b6f5891-a725-49ba-b3dc-a979e5ffec69",
                            Description = "CMS in boxing",
                            Email = "Coach2@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Vlad",
                            NormalizedEmail = "COACH2@GMAIL.COM",
                            NormalizedUserName = "COACH2",
                            PasswordHash = "AQAAAAEAACcQAAAAEEx6ZTBINskDTJuoEfjM190eh1taoDMB3LeNjhhrDSgzf+bGkHuKii9zHxkksoAn2Q==",
                            Patronymic = "Nikolayevich",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "855ac4c8-0bb0-4759-82cf-16c42e5a295e",
                            Surname = "Dorochin",
                            TwoFactorEnabled = false,
                            UserName = "Coach2"
                        },
                        new
                        {
                            Id = "a50a06a5-df07-4728-b6a0-93173c2ce4cf",
                            AccessFailedCount = 0,
                            BornDate = new DateTime(1970, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "c48010c3-db34-459c-a1ea-6359e37a5d9e",
                            Description = "MS in boxing",
                            Email = "Coach3@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Sergey",
                            NormalizedEmail = "COACH3@GMAIL.COM",
                            NormalizedUserName = "COACH3",
                            PasswordHash = "AQAAAAEAACcQAAAAEP/9bD9do2YO1Ds2/xT238LaRvGFInRrPqcdCHmqPyX/5JiU0LHMisYtXQLX3+MI3g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "11d59884-6d2e-4422-944a-b8235dd1ce38",
                            Surname = "Goncharov",
                            TwoFactorEnabled = false,
                            UserName = "Coach3"
                        });
                });

            modelBuilder.Entity("BoxingClub.DAL.Entities.BoxingGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoachId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

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

            modelBuilder.Entity("BoxingClub.DAL.Entities.Student", b =>
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

                    b.Property<bool>("Experienced")
                        .HasColumnType("bit");

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
                            DateOfEntry = new DateTime(2020, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Experienced = false,
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
                            DateOfEntry = new DateTime(2019, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Experienced = false,
                            Height = 180,
                            Name = "Igor",
                            NumberOfFights = 5,
                            Surname = "Zhuravlev",
                            Weight = 87.0
                        },
                        new
                        {
                            Id = 3,
                            BornDate = new DateTime(2001, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 1,
                            DateOfEntry = new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Experienced = false,
                            Height = 175,
                            Name = "Ivan",
                            NumberOfFights = 2,
                            Surname = "Pavlov",
                            Weight = 81.0
                        },
                        new
                        {
                            Id = 4,
                            BornDate = new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfEntry = new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Experienced = false,
                            Height = 176,
                            Name = "Andrew",
                            NumberOfFights = 10,
                            Patronymic = "Sergeevich",
                            Surname = "Solovyev",
                            Weight = 73.0
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "291c0120-8c27-47c5-83fe-9d7deb36f73c",
                            ConcurrencyStamp = "7f5ed190-c3f3-4a17-8136-6beb32ae1ed9",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6",
                            ConcurrencyStamp = "b1b11e16-8fe8-4261-9f54-b0b478d431f3",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "db460306-31c6-457a-989e-9e4317be99b9",
                            ConcurrencyStamp = "5b91e6d3-700d-491a-83b1-d57da38d1243",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "8da509ca-2005-457d-8ca3-105792f04013",
                            ConcurrencyStamp = "e8a0a39a-a363-4d62-9058-649f6fcc60a1",
                            Name = "Coach",
                            NormalizedName = "COACH"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef",
                            RoleId = "291c0120-8c27-47c5-83fe-9d7deb36f73c"
                        },
                        new
                        {
                            UserId = "fda7dfec-9828-41b2-bd9c-53dccbef2bb8",
                            RoleId = "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6"
                        },
                        new
                        {
                            UserId = "2d4254a5-7782-4b9c-a987-42a83d30669a",
                            RoleId = "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6"
                        },
                        new
                        {
                            UserId = "19759de3-ce1d-4cfd-8340-4e64eb245eb4",
                            RoleId = "8da509ca-2005-457d-8ca3-105792f04013"
                        },
                        new
                        {
                            UserId = "060342c3-9dc3-4597-bae1-9f19c991ebe9",
                            RoleId = "8da509ca-2005-457d-8ca3-105792f04013"
                        },
                        new
                        {
                            UserId = "a50a06a5-df07-4728-b6a0-93173c2ce4cf",
                            RoleId = "8da509ca-2005-457d-8ca3-105792f04013"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BoxingClub.DAL.Entities.BoxingGroup", b =>
                {
                    b.HasOne("BoxingClub.DAL.Entities.ApplicationUser", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachId");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("BoxingClub.DAL.Entities.Student", b =>
                {
                    b.HasOne("BoxingClub.DAL.Entities.BoxingGroup", "BoxingGroup")
                        .WithMany("Students")
                        .HasForeignKey("BoxingGroupId");

                    b.Navigation("BoxingGroup");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BoxingClub.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BoxingClub.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoxingClub.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BoxingClub.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BoxingClub.DAL.Entities.BoxingGroup", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}