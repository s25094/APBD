﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RevenueRecognitionSystem.Context;

#nullable disable

namespace RevenueRecognitionSystem.Migrations
{
    [DbContext(typeof(RevenueRecognitionContext))]
    [Migration("20240617154442_InitialMigration24")]
    partial class InitialMigration24
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RevenueRecognitionSystem.Model.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Client");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.Discount", b =>
                {
                    b.Property<int>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiscountId"));

                    b.Property<DateTime>("EndDate")
                        .HasMaxLength(100)
                        .HasColumnType("datetime2");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.Property<int?>("SoftwareOrderOrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasMaxLength(100)
                        .HasColumnType("datetime2");

                    b.HasKey("DiscountId");

                    b.HasIndex("SoftwareOrderOrderId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<int?>("SubscriptionOrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("paymentAmount")
                        .HasColumnType("money");

                    b.HasKey("PaymentId");

                    b.HasIndex("ContractId");

                    b.HasIndex("SubscriptionOrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.Software", b =>
                {
                    b.Property<int>("SoftwareId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SoftwareId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("FullPrice")
                        .HasColumnType("money");

                    b.Property<string>("SoftwareDescrition")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SoftwareName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SubscritionPrice")
                        .HasColumnType("int");

                    b.Property<double>("Version")
                        .HasColumnType("float");

                    b.HasKey("SoftwareId");

                    b.ToTable("Softwares");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.SoftwareOrder", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("SoftwareId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("isPaid")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("ClientId");

                    b.HasIndex("SoftwareId");

                    b.ToTable("SoftwareOrders");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SoftwareOrder");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.Company", b =>
                {
                    b.HasBaseType("RevenueRecognitionSystem.Model.Client");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("KRS")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasDiscriminator().HasValue("Company");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.Individual", b =>
                {
                    b.HasBaseType("RevenueRecognitionSystem.Model.Client");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PESEL")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasDiscriminator().HasValue("Individual");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.Subscription", b =>
                {
                    b.HasBaseType("RevenueRecognitionSystem.Model.SoftwareOrder");

                    b.Property<int>("QuantityOfRenewalPeriod")
                        .HasColumnType("int");

                    b.Property<string>("RenewalPeriod")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubscriptionName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasDiscriminator().HasValue("Subscription");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.UpfrontContract", b =>
                {
                    b.HasBaseType("RevenueRecognitionSystem.Model.SoftwareOrder");

                    b.Property<int?>("DiscountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasMaxLength(100)
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasMaxLength(100)
                        .HasColumnType("datetime2");

                    b.Property<int>("Updates")
                        .HasColumnType("int");

                    b.Property<int>("isSigned")
                        .HasColumnType("int");

                    b.HasIndex("DiscountId");

                    b.HasDiscriminator().HasValue("UpfrontContract");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.Discount", b =>
                {
                    b.HasOne("RevenueRecognitionSystem.Model.SoftwareOrder", null)
                        .WithMany("Discounts")
                        .HasForeignKey("SoftwareOrderOrderId");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.Payment", b =>
                {
                    b.HasOne("RevenueRecognitionSystem.Model.UpfrontContract", "UpfrontContract")
                        .WithMany("Payments")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RevenueRecognitionSystem.Model.Subscription", null)
                        .WithMany("Payments")
                        .HasForeignKey("SubscriptionOrderId");

                    b.Navigation("UpfrontContract");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.SoftwareOrder", b =>
                {
                    b.HasOne("RevenueRecognitionSystem.Model.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RevenueRecognitionSystem.Model.Software", "Software")
                        .WithMany()
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Software");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.UpfrontContract", b =>
                {
                    b.HasOne("RevenueRecognitionSystem.Model.Discount", null)
                        .WithMany("UpfrontContracts")
                        .HasForeignKey("DiscountId");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.Discount", b =>
                {
                    b.Navigation("UpfrontContracts");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.SoftwareOrder", b =>
                {
                    b.Navigation("Discounts");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.Subscription", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Model.UpfrontContract", b =>
                {
                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}