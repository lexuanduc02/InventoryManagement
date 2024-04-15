﻿// <auto-generated />
using System;
using InventoryManagement.Domains.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryManagement.Domains.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240412113213_remove_collumn_unit_from_table_merchandise_sale_invoice")]
    partial class remove_collumn_unit_from_table_merchandise_sale_invoice
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Merchandise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Merchandises");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.MerchandisePurchaseInvoice", b =>
                {
                    b.Property<Guid>("PurchaseInvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchandiseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<float>("MerchandisePrice")
                        .HasColumnType("real");

                    b.Property<float>("PurchasePrice")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PurchaseInvoiceId", "MerchandiseId");

                    b.HasIndex("MerchandiseId");

                    b.ToTable("MerchandisePurchaseInvoices");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.MerchandiseSaleInvoice", b =>
                {
                    b.Property<Guid>("SaleInvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchandiseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<float>("SellingPrice")
                        .HasColumnType("real");

                    b.Property<float>("Voucher")
                        .HasColumnType("real");

                    b.HasKey("SaleInvoiceId", "MerchandiseId");

                    b.HasIndex("MerchandiseId");

                    b.ToTable("MerchandiseSaleInvoices");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Partner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.PurchaseInvoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PartnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId");

                    b.HasIndex("UserId");

                    b.ToTable("PurchaseInvoices");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.SaleInvoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<string>("ShippingCarrier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("SaleInvoices");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDateOfEmployment")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("Area")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("StorageCapacity")
                        .HasColumnType("real");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("InventoryManagement.Models.CustomerModels.CustomerViewModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CustomerViewModel");
                });

            modelBuilder.Entity("InventoryManagement.Models.CustomerModels.UpdateCustomerRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UpdateCustomerRequest");
                });

            modelBuilder.Entity("InventoryManagement.Models.MerchandiseModels.UpdateProductRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehouseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UpdateProductRequest");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Merchandise", b =>
                {
                    b.HasOne("InventoryManagement.Domains.Entities.Category", "Category")
                        .WithMany("Merchandises")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagement.Domains.Entities.Warehouse", "Warehouse")
                        .WithMany("Merchandises")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.MerchandisePurchaseInvoice", b =>
                {
                    b.HasOne("InventoryManagement.Domains.Entities.Merchandise", "Merchandise")
                        .WithMany("MerchandisePurchaseInvoices")
                        .HasForeignKey("MerchandiseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagement.Domains.Entities.PurchaseInvoice", "PurchaseInvoice")
                        .WithMany("MerchandisePurchaseInvoices")
                        .HasForeignKey("PurchaseInvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merchandise");

                    b.Navigation("PurchaseInvoice");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.MerchandiseSaleInvoice", b =>
                {
                    b.HasOne("InventoryManagement.Domains.Entities.Merchandise", "Merchandise")
                        .WithMany("MerchandiseSaleInvoices")
                        .HasForeignKey("MerchandiseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagement.Domains.Entities.SaleInvoice", "SaleInvoice")
                        .WithMany("MerchandiseSaleInvoices")
                        .HasForeignKey("SaleInvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merchandise");

                    b.Navigation("SaleInvoice");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.PurchaseInvoice", b =>
                {
                    b.HasOne("InventoryManagement.Domains.Entities.Partner", "Partner")
                        .WithMany("PurchaseInvoices")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagement.Domains.Entities.User", "User")
                        .WithMany("PurchaseInvoices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partner");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.SaleInvoice", b =>
                {
                    b.HasOne("InventoryManagement.Domains.Entities.Customer", "Customer")
                        .WithMany("SaleInvoices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagement.Domains.Entities.User", "User")
                        .WithMany("SaleInvoices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.User", b =>
                {
                    b.HasOne("InventoryManagement.Domains.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Category", b =>
                {
                    b.Navigation("Merchandises");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Customer", b =>
                {
                    b.Navigation("SaleInvoices");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Merchandise", b =>
                {
                    b.Navigation("MerchandisePurchaseInvoices");

                    b.Navigation("MerchandiseSaleInvoices");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Partner", b =>
                {
                    b.Navigation("PurchaseInvoices");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.PurchaseInvoice", b =>
                {
                    b.Navigation("MerchandisePurchaseInvoices");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.SaleInvoice", b =>
                {
                    b.Navigation("MerchandiseSaleInvoices");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.User", b =>
                {
                    b.Navigation("PurchaseInvoices");

                    b.Navigation("SaleInvoices");
                });

            modelBuilder.Entity("InventoryManagement.Domains.Entities.Warehouse", b =>
                {
                    b.Navigation("Merchandises");
                });
#pragma warning restore 612, 618
        }
    }
}
