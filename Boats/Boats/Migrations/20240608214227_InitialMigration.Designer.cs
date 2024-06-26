﻿// <auto-generated />
using System;
using Boats.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Boats.Migrations
{
    [DbContext(typeof(BoatsDbContext))]
    [Migration("20240608214227_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Boats.Model.BoatStandard", b =>
                {
                    b.Property<int>("IdBoatStandard")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBoatStandard"));

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdBoatStandard");

                    b.ToTable("BoatStandards");
                });

            modelBuilder.Entity("Boats.Model.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClient"));

                    b.Property<DateTime>("Birthday")
                        .HasMaxLength(100)
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdClientCategory")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdClient");

                    b.HasIndex("IdClientCategory");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Boats.Model.ClientCategory", b =>
                {
                    b.Property<int>("IdClientCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClientCategory"));

                    b.Property<int>("DiscountPerc")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdClientCategory");

                    b.ToTable("ClientCategories");
                });

            modelBuilder.Entity("Boats.Model.Reservation", b =>
                {
                    b.Property<int>("IdReservation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdReservation"));

                    b.Property<string>("CancelReason")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<int>("Fulfilled")
                        .HasColumnType("int");

                    b.Property<int>("IdBoatStandard")
                        .HasColumnType("int");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<int>("NumOfBoats")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("IdReservation");

                    b.HasIndex("IdBoatStandard");

                    b.HasIndex("IdClient");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Boats.Model.Sailboat", b =>
                {
                    b.Property<int>("IdSailboat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSailboat"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdBoatStandard")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("IdSailboat");

                    b.HasIndex("IdBoatStandard");

                    b.ToTable("Sailboats");
                });

            modelBuilder.Entity("Boats.Model.Sailboat_Reservation", b =>
                {
                    b.Property<int>("Sailboat")
                        .HasColumnType("int");

                    b.Property<int>("Reservation")
                        .HasColumnType("int");

                    b.Property<int?>("ReservationIdReservation")
                        .HasColumnType("int");

                    b.Property<int?>("SailboatIdSailboat")
                        .HasColumnType("int");

                    b.HasKey("Sailboat", "Reservation");

                    b.HasIndex("ReservationIdReservation");

                    b.HasIndex("SailboatIdSailboat");

                    b.ToTable("Sailboat_Reservations");
                });

            modelBuilder.Entity("Boats.Model.Client", b =>
                {
                    b.HasOne("Boats.Model.ClientCategory", "ClientCategory")
                        .WithMany("Clients")
                        .HasForeignKey("IdClientCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientCategory");
                });

            modelBuilder.Entity("Boats.Model.Reservation", b =>
                {
                    b.HasOne("Boats.Model.BoatStandard", "BoatStandard")
                        .WithMany()
                        .HasForeignKey("IdBoatStandard")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Boats.Model.Client", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoatStandard");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Boats.Model.Sailboat", b =>
                {
                    b.HasOne("Boats.Model.BoatStandard", "BoatStandard")
                        .WithMany()
                        .HasForeignKey("IdBoatStandard")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoatStandard");
                });

            modelBuilder.Entity("Boats.Model.Sailboat_Reservation", b =>
                {
                    b.HasOne("Boats.Model.Reservation", null)
                        .WithMany("Sailboat_Reservation")
                        .HasForeignKey("ReservationIdReservation");

                    b.HasOne("Boats.Model.Sailboat", null)
                        .WithMany("Sailboat_Reservation")
                        .HasForeignKey("SailboatIdSailboat");
                });

            modelBuilder.Entity("Boats.Model.Client", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Boats.Model.ClientCategory", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("Boats.Model.Reservation", b =>
                {
                    b.Navigation("Sailboat_Reservation");
                });

            modelBuilder.Entity("Boats.Model.Sailboat", b =>
                {
                    b.Navigation("Sailboat_Reservation");
                });
#pragma warning restore 612, 618
        }
    }
}
