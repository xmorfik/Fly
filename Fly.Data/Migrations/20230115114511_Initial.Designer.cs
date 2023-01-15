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
    [Migration("20230115114511_Initial")]
    partial class Initial
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

                    b.Property<int?>("AirlineId")
                        .HasColumnType("integer");

                    b.Property<int?>("AirportId")
                        .HasColumnType("integer");

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
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DirectionAngle")
                        .HasColumnType("integer");

                    b.Property<bool?>("IsFlying")
                        .HasColumnType("boolean");

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

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("Fly.Core.Entities.Client", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients");
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
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DepartureAirportId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DepartureDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AircraftId");

                    b.HasIndex("ArrivalAirportId");

                    b.HasIndex("DepartureAirportId");

                    b.ToTable("Flights");
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

                    b.Property<int?>("ClientId")
                        .HasColumnType("integer");

                    b.Property<int?>("FlightId")
                        .HasColumnType("integer");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsSold")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsUsed")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<int?>("SeatId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("SoldDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("FlightId");

                    b.HasIndex("SeatId");

                    b.ToTable("Tickets");
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

            modelBuilder.Entity("Fly.Core.Entities.Seat", b =>
                {
                    b.HasOne("Fly.Core.Entities.Aircraft", "Aircraft")
                        .WithMany("Seats")
                        .HasForeignKey("AircraftId");

                    b.Navigation("Aircraft");
                });

            modelBuilder.Entity("Fly.Core.Entities.Ticket", b =>
                {
                    b.HasOne("Fly.Core.Entities.Client", "Client")
                        .WithMany("Tickets")
                        .HasForeignKey("ClientId");

                    b.HasOne("Fly.Core.Entities.Flight", "Flight")
                        .WithMany("Tickets")
                        .HasForeignKey("FlightId");

                    b.HasOne("Fly.Core.Entities.Seat", "Seat")
                        .WithMany("Tickets")
                        .HasForeignKey("SeatId");

                    b.Navigation("Client");

                    b.Navigation("Flight");

                    b.Navigation("Seat");
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
                });

            modelBuilder.Entity("Fly.Core.Entities.Airport", b =>
                {
                    b.Navigation("Aircrafts");

                    b.Navigation("FlightsIn");

                    b.Navigation("FlightsOut");
                });

            modelBuilder.Entity("Fly.Core.Entities.Client", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Fly.Core.Entities.Flight", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Fly.Core.Entities.Seat", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
