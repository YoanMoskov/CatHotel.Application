﻿// <auto-generated />
using System;
using CatHotel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CatHotel.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210817120055_AddedStyleAsSeparateEntity")]
    partial class AddedStyleAsSeparateEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CatHotel.Data.Models.Breed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Breeds");
                });

            modelBuilder.Entity("CatHotel.Data.Models.Cat", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdditionalInformation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("BreedId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("DATE");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BreedId");

                    b.HasIndex("UserId");

                    b.ToTable("Cats");
                });

            modelBuilder.Entity("CatHotel.Data.Models.CatGrooming", b =>
                {
                    b.Property<string>("CatId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GroomingId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CatId", "GroomingId");

                    b.HasIndex("GroomingId");

                    b.ToTable("CatsGroomings");
                });

            modelBuilder.Entity("CatHotel.Data.Models.CatReservation", b =>
                {
                    b.Property<string>("CatId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReservationId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CatId", "ReservationId");

                    b.HasIndex("ReservationId");

                    b.ToTable("CatsReservations");
                });

            modelBuilder.Entity("CatHotel.Data.Models.Grooming", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PaymentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StyleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.HasIndex("StyleId");

                    b.ToTable("Groomings");
                });

            modelBuilder.Entity("CatHotel.Data.Models.Payment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("MONEY");

                    b.Property<bool>("isPaid")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("CatHotel.Data.Models.Reservation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Arrival")
                        .HasColumnType("DATE");

                    b.Property<DateTime>("DateOfReservation")
                        .HasColumnType("DATE");

                    b.Property<DateTime>("Departure")
                        .HasColumnType("DATE");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("PaymentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ReservationState")
                        .HasColumnType("int");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.HasIndex("RoomTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("CatHotel.Data.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsTaken")
                        .HasColumnType("bit");

                    b.Property<string>("ReservationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("CatHotel.Data.Models.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("MONEY");

                    b.HasKey("Id");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("CatHotel.Data.Models.Style", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Styles");
                });

            modelBuilder.Entity("CatHotel.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("DATE");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
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
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

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
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CatHotel.Data.Models.Cat", b =>
                {
                    b.HasOne("CatHotel.Data.Models.Breed", "Breed")
                        .WithMany()
                        .HasForeignKey("BreedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatHotel.Data.Models.User", "User")
                        .WithMany("Cats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Breed");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CatHotel.Data.Models.CatGrooming", b =>
                {
                    b.HasOne("CatHotel.Data.Models.Cat", "Cat")
                        .WithMany("CatGroomings")
                        .HasForeignKey("CatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatHotel.Data.Models.Grooming", "Grooming")
                        .WithMany("CatGroomings")
                        .HasForeignKey("GroomingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cat");

                    b.Navigation("Grooming");
                });

            modelBuilder.Entity("CatHotel.Data.Models.CatReservation", b =>
                {
                    b.HasOne("CatHotel.Data.Models.Cat", "Cat")
                        .WithMany("CatsReservations")
                        .HasForeignKey("CatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatHotel.Data.Models.Reservation", "Reservation")
                        .WithMany("CatsReservations")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cat");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("CatHotel.Data.Models.Grooming", b =>
                {
                    b.HasOne("CatHotel.Data.Models.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.HasOne("CatHotel.Data.Models.Style", "Style")
                        .WithMany()
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("Style");
                });

            modelBuilder.Entity("CatHotel.Data.Models.Reservation", b =>
                {
                    b.HasOne("CatHotel.Data.Models.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.HasOne("CatHotel.Data.Models.RoomType", "RoomType")
                        .WithMany()
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatHotel.Data.Models.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId");

                    b.Navigation("Payment");

                    b.Navigation("RoomType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CatHotel.Data.Models.Room", b =>
                {
                    b.HasOne("CatHotel.Data.Models.Reservation", null)
                        .WithMany("Rooms")
                        .HasForeignKey("ReservationId");

                    b.HasOne("CatHotel.Data.Models.RoomType", "RoomType")
                        .WithMany()
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoomType");
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
                    b.HasOne("CatHotel.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CatHotel.Data.Models.User", null)
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

                    b.HasOne("CatHotel.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CatHotel.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CatHotel.Data.Models.Cat", b =>
                {
                    b.Navigation("CatGroomings");

                    b.Navigation("CatsReservations");
                });

            modelBuilder.Entity("CatHotel.Data.Models.Grooming", b =>
                {
                    b.Navigation("CatGroomings");
                });

            modelBuilder.Entity("CatHotel.Data.Models.Reservation", b =>
                {
                    b.Navigation("CatsReservations");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("CatHotel.Data.Models.User", b =>
                {
                    b.Navigation("Cats");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
