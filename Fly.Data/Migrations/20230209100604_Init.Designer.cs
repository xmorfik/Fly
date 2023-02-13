﻿// <auto-generated />
using System;
using Fly.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fly.Data.Migrations
{
    [DbContext(typeof(FlyDbContext))]
    [Migration("20230209100604_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Fly.Core.Entities.Aircraft", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<int?>("AircraftState")
                        .HasColumnType("integer");

                    b.Property<int?>("AirlineId")
                        .HasColumnType("integer");

                    b.Property<int?>("AirportId")
                        .HasColumnType("integer");

                    b.Property<int?>("FlightHours")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ManufactureDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModelType")
                        .HasColumnType("text");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.HasIndex("AirportId");

                    b.ToTable("Aircrafts");
                });

            modelBuilder.Entity("Fly.Core.Entities.AircraftLocation", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<int?>("AircraftId")
                        .HasColumnType("integer");

                    b.Property<int?>("Altitude")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("DirectionAngle")
                        .HasColumnType("integer");

                    b.Property<double?>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double?>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<int?>("Speed")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AircraftId");

                    b.ToTable("AircraftLocations");
                });

            modelBuilder.Entity("Fly.Core.Entities.Airline", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("RegistrationAddress")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("Fly.Core.Entities.Airport", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("AirporId")
                        .HasColumnType("text");

                    b.Property<int?>("CityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("Fly.Core.Entities.City", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Fly.Core.Entities.Flight", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<int?>("AircraftId")
                        .HasColumnType("integer");

                    b.Property<int?>("ArrivalAirportId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ArrivalDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("DepartureAirportId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DepartureDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AircraftId");

                    b.HasIndex("ArrivalAirportId");

                    b.HasIndex("DepartureAirportId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Fly.Core.Entities.Manager", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<int?>("AirlineId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.HasIndex("UserId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("Fly.Core.Entities.Message", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Fly.Core.Entities.Notification", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Header")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Fly.Core.Entities.Passenger", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<string>("Nationality")
                        .HasColumnType("text");

                    b.Property<string>("PassportNo")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("Fly.Core.Entities.Seat", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<int?>("AircraftId")
                        .HasColumnType("integer");

                    b.Property<int?>("Column")
                        .HasColumnType("integer");

                    b.Property<int?>("Row")
                        .HasColumnType("integer");

                    b.Property<int?>("SeatClass")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AircraftId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("Fly.Core.Entities.Ticket", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<int?>("FlightId")
                        .HasColumnType("integer");

                    b.Property<int?>("PassengerId")
                        .HasColumnType("integer");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<int?>("SeatId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("SoldDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("TicketState")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.HasIndex("PassengerId");

                    b.HasIndex("SeatId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1b472fdf-fa38-48ce-b7fc-85e72733965e",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "c6fdedca-a191-439c-8d28-4d77abcc8a85",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "893d684b-a096-44d6-9523-c733a2843a6c",
                            Name = "Passenger",
                            NormalizedName = "PASSENGER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Fly.Core.Entities.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Fly.Core.Entities.Aircraft", b =>
                {
                    b.HasOne("Fly.Core.Entities.Airline", "Airline")
                        .WithMany("Aircrafts")
                        .HasForeignKey("AirlineId");

                    b.HasOne("Fly.Core.Entities.Airport", "Airport")
                        .WithMany("Aircrafts")
                        .HasForeignKey("AirportId");

                    b.Navigation("Airline");

                    b.Navigation("Airport");
                });

            modelBuilder.Entity("Fly.Core.Entities.AircraftLocation", b =>
                {
                    b.HasOne("Fly.Core.Entities.Aircraft", "Aircraft")
                        .WithMany("AircraftLocations")
                        .HasForeignKey("AircraftId");

                    b.Navigation("Aircraft");
                });

            modelBuilder.Entity("Fly.Core.Entities.Airport", b =>
                {
                    b.HasOne("Fly.Core.Entities.City", "City")
                        .WithMany("Airports")
                        .HasForeignKey("CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("Fly.Core.Entities.Flight", b =>
                {
                    b.HasOne("Fly.Core.Entities.Aircraft", "Aircraft")
                        .WithMany("Flights")
                        .HasForeignKey("AircraftId");

                    b.HasOne("Fly.Core.Entities.Airport", "ArrivalAirport")
                        .WithMany("FlightsIn")
                        .HasForeignKey("ArrivalAirportId");

                    b.HasOne("Fly.Core.Entities.Airport", "DepartureAirport")
                        .WithMany("FlightsOut")
                        .HasForeignKey("DepartureAirportId");

                    b.Navigation("Aircraft");

                    b.Navigation("ArrivalAirport");

                    b.Navigation("DepartureAirport");
                });

            modelBuilder.Entity("Fly.Core.Entities.Manager", b =>
                {
                    b.HasOne("Fly.Core.Entities.Airline", "Airline")
                        .WithMany("Managers")
                        .HasForeignKey("AirlineId");

                    b.HasOne("Fly.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Airline");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fly.Core.Entities.Message", b =>
                {
                    b.HasOne("Fly.Core.Entities.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fly.Core.Entities.Notification", b =>
                {
                    b.HasOne("Fly.Core.Entities.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fly.Core.Entities.Passenger", b =>
                {
                    b.HasOne("Fly.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fly.Core.Entities.Seat", b =>
                {
                    b.HasOne("Fly.Core.Entities.Aircraft", "Aircraft")
                        .WithMany("Seats")
                        .HasForeignKey("AircraftId");

                    b.Navigation("Aircraft");
                });

            modelBuilder.Entity("Fly.Core.Entities.Ticket", b =>
                {
                    b.HasOne("Fly.Core.Entities.Flight", "Flight")
                        .WithMany("Tickets")
                        .HasForeignKey("FlightId");

                    b.HasOne("Fly.Core.Entities.Passenger", "Passenger")
                        .WithMany("Tickets")
                        .HasForeignKey("PassengerId");

                    b.HasOne("Fly.Core.Entities.Seat", "Seat")
                        .WithMany("Tickets")
                        .HasForeignKey("SeatId");

                    b.Navigation("Flight");

                    b.Navigation("Passenger");

                    b.Navigation("Seat");
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fly.Core.Entities.Aircraft", b =>
                {
                    b.Navigation("AircraftLocations");

                    b.Navigation("Flights");

                    b.Navigation("Seats");
                });

            modelBuilder.Entity("Fly.Core.Entities.Airline", b =>
                {
                    b.Navigation("Aircrafts");

                    b.Navigation("Managers");
                });

            modelBuilder.Entity("Fly.Core.Entities.Airport", b =>
                {
                    b.Navigation("Aircrafts");

                    b.Navigation("FlightsIn");

                    b.Navigation("FlightsOut");
                });

            modelBuilder.Entity("Fly.Core.Entities.City", b =>
                {
                    b.Navigation("Airports");
                });

            modelBuilder.Entity("Fly.Core.Entities.Flight", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Fly.Core.Entities.Passenger", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Fly.Core.Entities.Seat", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Fly.Core.Entities.User", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}