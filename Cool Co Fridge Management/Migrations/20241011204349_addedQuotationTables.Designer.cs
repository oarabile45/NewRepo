﻿// <auto-generated />
using System;
using Cool_Co_Fridge_Management.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cool_Co_Fridge_Management.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241011204349_addedQuotationTables")]
    partial class addedQuotationTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.DeliveryNote", b =>
                {
                    b.Property<int>("DeliveryNoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeliveryNoteId"));

                    b.Property<DateTime>("DeliveredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeliveryDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<string>("ReceiverName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeliveryNoteId");

                    b.ToTable("DeliveryNotes");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Fault", b =>
                {
                    b.Property<int>("FaultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FaultId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FaultTechId")
                        .HasColumnType("int");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.HasKey("FaultId");

                    b.HasIndex("FaultTechId");

                    b.HasIndex("ID");

                    b.ToTable("Faults");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.FaultTech", b =>
                {
                    b.Property<int>("FaultTechId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FaultTechId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FaultTechId");

                    b.ToTable("FaultTech");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.FaultType", b =>
                {
                    b.Property<int>("FaultTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FaultTypeID"));

                    b.Property<string>("FaultTypeDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FaultTypeID");

                    b.ToTable("faultTypes");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.FridgeAllocation", b =>
                {
                    b.Property<int>("FridgeAllocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FridgeAllocationID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FridgeID")
                        .HasColumnType("int");

                    b.Property<int>("FridgeRequestID")
                        .HasColumnType("int");

                    b.Property<int>("FridgeRequestId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FridgeAllocationID");

                    b.HasIndex("FridgeID");

                    b.HasIndex("FridgeRequestId");

                    b.HasIndex("Id");

                    b.ToTable("FridgeAllocation");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.FridgeFault", b =>
                {
                    b.Property<int>("FridgeFaultID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FridgeFaultID"));

                    b.Property<int>("FaultTypeID")
                        .HasColumnType("int");

                    b.Property<int?>("ID")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("RepairDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("ReportedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.HasKey("FridgeFaultID");

                    b.HasIndex("FaultTypeID");

                    b.HasIndex("ID");

                    b.HasIndex("StatusID");

                    b.ToTable("fridgeFaults");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.FridgeRequest", b =>
                {
                    b.Property<int>("FridgeRequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FridgeRequestID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FridgeTypeID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FridgeRequestID");

                    b.HasIndex("FridgeTypeID");

                    b.ToTable("FridgeRequests");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Fridge_Stock", b =>
                {
                    b.Property<int>("StockID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockID"));

                    b.Property<bool>("Availability")
                        .HasColumnType("bit");

                    b.Property<int>("FridgeTypeID")
                        .HasColumnType("int");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("StockID");

                    b.HasIndex("FridgeTypeID");

                    b.ToTable("stock");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Fridge_Type", b =>
                {
                    b.Property<int>("FridgeTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FridgeTypeID"));

                    b.Property<bool>("Availablity")
                        .HasColumnType("bit");

                    b.Property<string>("FridgeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.HasKey("FridgeTypeID");

                    b.HasIndex("SupplierID");

                    b.ToTable("fridge_type");

                    b.HasData(
                        new
                        {
                            FridgeTypeID = 1,
                            Availablity = true,
                            FridgeType = "Chest Freezer",
                            SupplierID = 789
                        },
                        new
                        {
                            FridgeTypeID = 2,
                            Availablity = true,
                            FridgeType = "Drawer Fridge",
                            SupplierID = 456
                        },
                        new
                        {
                            FridgeTypeID = 3,
                            Availablity = true,
                            FridgeType = "Commercial Fridge",
                            SupplierID = 123
                        },
                        new
                        {
                            FridgeTypeID = 4,
                            Availablity = true,
                            FridgeType = "Mini Fridge",
                            SupplierID = 101
                        },
                        new
                        {
                            FridgeTypeID = 5,
                            Availablity = true,
                            FridgeType = "French Door Fridge",
                            SupplierID = 112
                        });
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.MaintenanceBooking", b =>
                {
                    b.Property<int>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ApprovedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaintenanceTechID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("BookingID");

                    b.HasIndex("MaintenanceTechID");

                    b.HasIndex("UserID");

                    b.ToTable("MaintenanceBooking");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.MaintenanceTech", b =>
                {
                    b.Property<int>("MaintenanceTechID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaintenanceTechID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaintenanceTechID");

                    b.ToTable("MaintenanceTech");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.OrderStatus", b =>
                {
                    b.Property<int>("OrderStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderStatusId"));

                    b.Property<string>("OrderDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderStatusId");

                    b.ToTable("orderStatus");

                    b.HasData(
                        new
                        {
                            OrderStatusId = 1,
                            OrderDesc = "Complete"
                        },
                        new
                        {
                            OrderStatusId = 2,
                            OrderDesc = "Pending"
                        },
                        new
                        {
                            OrderStatusId = 3,
                            OrderDesc = "Cancelled"
                        });
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.PurchaseOrder", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<int?>("DeliveryNoteId")
                        .HasColumnType("int");

                    b.Property<int>("FridgeTypeID")
                        .HasColumnType("int");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("OrderedDate")
                        .HasColumnType("date");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("DeliveryNoteId");

                    b.HasIndex("FridgeTypeID");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("SupplierId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.PurchaseRequest", b =>
                {
                    b.Property<int>("PurchaseRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseRequestId"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("FridgeTypeID")
                        .HasColumnType("int");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("int");

                    b.Property<int>("RequestQuantity")
                        .HasColumnType("int");

                    b.HasKey("PurchaseRequestId");

                    b.HasIndex("FridgeTypeID");

                    b.HasIndex("OrderStatusId");

                    b.ToTable("PurchaseRequests");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Quotation", b =>
                {
                    b.Property<int>("QuotationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuotationId"));

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentTerms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("QuotationAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("QuotationNotes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RFQID")
                        .HasColumnType("int");

                    b.Property<int>("RFQuotationRFQID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReceivedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SupplierId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierId1")
                        .HasColumnType("int");

                    b.HasKey("QuotationId");

                    b.HasIndex("RFQuotationRFQID");

                    b.HasIndex("SupplierId1");

                    b.ToTable("Quotations");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.QuotationItem", b =>
                {
                    b.Property<int>("QuotationItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuotationItemId"));

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("QuotationId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("QuotationItemId");

                    b.HasIndex("QuotationId");

                    b.ToTable("QuotationItems");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.RFQuotation", b =>
                {
                    b.Property<int>("RFQID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RFQID"));

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ItemDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrinceRange")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("RFQID");

                    b.HasIndex("SupplierId");

                    b.ToTable("RFQuotation");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Roles", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            RoleName = "Customer"
                        },
                        new
                        {
                            RoleID = 2,
                            RoleName = "Fault Technician"
                        },
                        new
                        {
                            RoleID = 3,
                            RoleName = "Maintenance Technician"
                        },
                        new
                        {
                            RoleID = 4,
                            RoleName = "Stock Controller"
                        },
                        new
                        {
                            RoleID = 5,
                            RoleName = "Customer Service"
                        },
                        new
                        {
                            RoleID = 6,
                            RoleName = "Purchasing Manager"
                        },
                        new
                        {
                            RoleID = 7,
                            RoleName = "Administrator"
                        });
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Status", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusID"));

                    b.Property<string>("StatusDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusID");

                    b.ToTable("statuses");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierId"));

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierId");

                    b.ToTable("suppliers");

                    b.HasData(
                        new
                        {
                            SupplierId = 789,
                            ContactNumber = "0860884402",
                            Email = "service@kic.co.za",
                            SupplierName = "KIC South Africa"
                        },
                        new
                        {
                            SupplierId = 456,
                            ContactNumber = "0317926900",
                            Email = "ayakhac16@gmail.com",
                            SupplierName = "Just Refrigeration"
                        },
                        new
                        {
                            SupplierId = 123,
                            ContactNumber = "0861113804",
                            Email = "stedon@maxicoolsa.co.za",
                            SupplierName = "Maxi-Cool S.A"
                        },
                        new
                        {
                            SupplierId = 101,
                            ContactNumber = "0102071600",
                            Email = "enquiries@zeroappliances.co.za",
                            SupplierName = "Zero Appliances"
                        },
                        new
                        {
                            SupplierId = 112,
                            ContactNumber = "0823422002",
                            Email = "sales@minty.co.za",
                            SupplierName = "Mintys Factory Shop"
                        });
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Users", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("ID");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Fault", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.FaultTech", "FaultTech")
                        .WithMany()
                        .HasForeignKey("FaultTechId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cool_Co_Fridge_Management.Models.MaintenanceBooking", "MaintenanceBooking")
                        .WithMany()
                        .HasForeignKey("ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FaultTech");

                    b.Navigation("MaintenanceBooking");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.FridgeAllocation", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.Fridge_Stock", "Fridge_Stock")
                        .WithMany()
                        .HasForeignKey("FridgeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cool_Co_Fridge_Management.Models.FridgeRequest", "FridgeRequest")
                        .WithMany()
                        .HasForeignKey("FridgeRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cool_Co_Fridge_Management.Models.Users", "users")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FridgeRequest");

                    b.Navigation("Fridge_Stock");

                    b.Navigation("users");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.FridgeFault", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.FaultType", "faultType")
                        .WithMany()
                        .HasForeignKey("FaultTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cool_Co_Fridge_Management.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("ID");

                    b.HasOne("Cool_Co_Fridge_Management.Models.Status", "status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");

                    b.Navigation("faultType");

                    b.Navigation("status");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.FridgeRequest", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.Fridge_Type", "FridgeType")
                        .WithMany()
                        .HasForeignKey("FridgeTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FridgeType");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Fridge_Stock", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.Fridge_Type", "Fridge_Type")
                        .WithMany()
                        .HasForeignKey("FridgeTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fridge_Type");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Fridge_Type", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.MaintenanceBooking", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.MaintenanceTech", null)
                        .WithMany("MaintenanceBookings")
                        .HasForeignKey("MaintenanceTechID");

                    b.HasOne("Cool_Co_Fridge_Management.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.PurchaseOrder", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.DeliveryNote", "DeliveryNote")
                        .WithMany()
                        .HasForeignKey("DeliveryNoteId");

                    b.HasOne("Cool_Co_Fridge_Management.Models.Fridge_Type", "Fridge_Type")
                        .WithMany()
                        .HasForeignKey("FridgeTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cool_Co_Fridge_Management.Models.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cool_Co_Fridge_Management.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryNote");

                    b.Navigation("Fridge_Type");

                    b.Navigation("OrderStatus");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.PurchaseRequest", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.Fridge_Type", "FridgeType")
                        .WithMany()
                        .HasForeignKey("FridgeTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cool_Co_Fridge_Management.Models.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FridgeType");

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Quotation", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.RFQuotation", "RFQuotation")
                        .WithMany()
                        .HasForeignKey("RFQuotationRFQID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cool_Co_Fridge_Management.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RFQuotation");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.QuotationItem", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.Quotation", "Quotations")
                        .WithMany("Items")
                        .HasForeignKey("QuotationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quotations");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.RFQuotation", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Users", b =>
                {
                    b.HasOne("Cool_Co_Fridge_Management.Models.Roles", "Roles")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.MaintenanceTech", b =>
                {
                    b.Navigation("MaintenanceBookings");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Quotation", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Cool_Co_Fridge_Management.Models.Roles", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
