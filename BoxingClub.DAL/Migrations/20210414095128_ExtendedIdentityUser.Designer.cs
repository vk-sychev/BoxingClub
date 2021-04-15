﻿// <auto-generated />
using System;
using BoxingClub.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BoxingClub.DAL.Implementation.Migrations
{
    /*
    [DbContext(typeof(BoxingClubContext))]
    [Migration("20210414095128_ExtendedIdentityUser")]
    partial class ExtendedIdentityUser
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
                            Id = "5189818d-2b4c-4509-88cd-7d83d9f1fd91",
                            AccessFailedCount = 0,
                            BornDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "adcdd681-36fd-4e31-be4c-a0d83248489a",
                            Email = "Manager1@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "MANAGER1@GMAIL.COM",
                            NormalizedUserName = "MANAGER1",
                            PasswordHash = "AQAAAAEAACcQAAAAEG3T/NnozQkRpUNejb8CS3Wf+AFI/FvfXA8sj4T1tEGyyZ18/8lI6CnbSoT77SCdUQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "47a5c985-7765-4581-afc1-a8f91b14beb0",
                            TwoFactorEnabled = false,
                            UserName = "Manager1"
                        },
                        new
                        {
                            Id = "532e9d3b-8503-4903-9575-99e70457d131",
                            AccessFailedCount = 0,
                            BornDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "2af03aac-a6d9-4a67-80c5-ffebe7f64ba8",
                            Email = "Manager2@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "MANAGER2@GMAIL.COM",
                            NormalizedUserName = "MANAGER2",
                            PasswordHash = "AQAAAAEAACcQAAAAEKiyOOKJrBGLRDYYADbN8UUhNfYXmsFtaxfbSURLGf8SS4PVJ60q8XawdSeITvU+ww==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ddd6cf09-106d-460f-b790-9ed01553b699",
                            TwoFactorEnabled = false,
                            UserName = "Manager2"
                        },
                        new
                        {
                            Id = "b65d237d-aa7d-409b-be76-3143f959c5f0",
                            AccessFailedCount = 0,
                            BornDate = new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "f741af9d-f554-420c-90c8-966e408f13e0",
                            Email = "Admin@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Vasya",
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEPbKEr8sWmSvMFtLoHA2A/J75WGQtOl0gtoZIvfw1vcaBZC9gyBRjPUxgyu1mChl/Q==",
                            Patronymic = "Konstantinovich",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "825735b4-6fa4-4564-b153-7b3d412b043b",
                            Surname = "Sychev",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        },
                        new
                        {
                            Id = "19759de3-ce1d-4cfd-8340-4e64eb245eb4",
                            AccessFailedCount = 0,
                            BornDate = new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "f25f5df4-3115-4862-978b-0c2ca1885f8b",
                            Description = "CMS in boxing",
                            Email = "Coach1@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Pavel",
                            NormalizedEmail = "COACH1@GMAIL.COM",
                            NormalizedUserName = "COACH1",
                            PasswordHash = "AQAAAAEAACcQAAAAEA6yp+FMcAwMcRJmmq8f7Q/+9NUJ/v9XnrQnNQe8b+snx8+fF6kQOvmVXLap4rjtqQ==",
                            Patronymic = "Nikolayevich",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "a1eca29c-eb29-431c-968c-b415a9409a02",
                            Surname = "Dorochin",
                            TwoFactorEnabled = false,
                            UserName = "Coach1"
                        },
                        new
                        {
                            Id = "060342c3-9dc3-4597-bae1-9f19c991ebe9",
                            AccessFailedCount = 0,
                            BornDate = new DateTime(1991, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "75ed5f26-91e5-4430-8400-1583125c76ea",
                            Description = "CMS in boxing",
                            Email = "Coach2@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Vlad",
                            NormalizedEmail = "COACH2@GMAIL.COM",
                            NormalizedUserName = "COACH2",
                            PasswordHash = "AQAAAAEAACcQAAAAECYJ6ir9Cr/zsTBqlcGaWebIhDzIA8Bo9JDc8xVlgUSYPLeDtnu8Rpb9uGfawykzJA==",
                            Patronymic = "Nikolayevich",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "64180435-d042-4109-8697-f5eece9835c3",
                            Surname = "Dorochin",
                            TwoFactorEnabled = false,
                            UserName = "Coach2"
                        },
                        new
                        {
                            Id = "a50a06a5-df07-4728-b6a0-93173c2ce4cf",
                            AccessFailedCount = 0,
                            BornDate = new DateTime(1970, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "ceb819ea-4c0e-4d52-9b7e-a73eb2af8ac5",
                            Description = "MS in boxing",
                            Email = "Coach3@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Sergey",
                            NormalizedEmail = "COACH3@GMAIL.COM",
                            NormalizedUserName = "COACH3",
                            PasswordHash = "AQAAAAEAACcQAAAAEOWThQhPNJrnt4UczkcOqfU5kXlYLdpw8b4Jodp9UrK0srZaPN+N9MGJYkwz8rLJvA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "03922e60-a760-4c7d-a201-566620987208",
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

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

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
                            Height = 175,
                            Name = "Vasiliy",
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
                            Height = 180,
                            Name = "Igor",
                            Surname = "Zhuravlev",
                            Weight = 87.0
                        },
                        new
                        {
                            Id = 3,
                            BornDate = new DateTime(2001, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BoxingGroupId = 1,
                            DateOfEntry = new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Height = 175,
                            Name = "Ivan",
                            Surname = "Pavlov",
                            Weight = 81.0
                        },
                        new
                        {
                            Id = 4,
                            BornDate = new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfEntry = new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Height = 176,
                            Name = "Andrew",
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
                            Id = "db460306-31c6-457a-989e-9e4317be99b9",
                            ConcurrencyStamp = "0defa253-5b52-40a6-b807-08e805b3768a",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "2dc83959-9dca-4cf3-855e-809741ce1e0d",
                            ConcurrencyStamp = "588857b3-4c40-4719-94e4-452ed3d547b1",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "5ca8984c-31f5-4883-b68e-d9ff541fbfb2",
                            ConcurrencyStamp = "be5b604c-f2dc-48a2-b6e6-a891d4e8878a",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "8da509ca-2005-457d-8ca3-105792f04013",
                            ConcurrencyStamp = "442c310a-e9db-4b21-83cc-5f26a31cefaa",
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
                            UserId = "b65d237d-aa7d-409b-be76-3143f959c5f0",
                            RoleId = "db460306-31c6-457a-989e-9e4317be99b9"
                        },
                        new
                        {
                            UserId = "5189818d-2b4c-4509-88cd-7d83d9f1fd91",
                            RoleId = "2dc83959-9dca-4cf3-855e-809741ce1e0d"
                        },
                        new
                        {
                            UserId = "532e9d3b-8503-4903-9575-99e70457d131",
                            RoleId = "2dc83959-9dca-4cf3-855e-809741ce1e0d"
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
    */
}
