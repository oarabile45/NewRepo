using Cool_Co_Fridge_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Cool_Co_Fridge_Management.Data.Extentions
{
   public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Supplier>().HasData(
                new Supplier { SupplierId = 789, SupplierName = "KIC South Africa", ContactNumber = "0860884402", Email = "service@kic.co.za"},
                new Supplier { SupplierId = 456, SupplierName = "Just Refrigeration", ContactNumber = "0317926900", Email = "ayakhac16@gmail.com" },
                new Supplier { SupplierId = 123, SupplierName = "Maxi-Cool S.A", ContactNumber = "0861113804", Email = "stedon@maxicoolsa.co.za" },
                new Supplier { SupplierId = 101, SupplierName = "Zero Appliances", ContactNumber = "0102071600", Email = "enquiries@zeroappliances.co.za"},
                new Supplier { SupplierId = 112, SupplierName = "Mintys Factory Shop", ContactNumber = "0823422002", Email = "sales@minty.co.za" }
                );
            builder.Entity<Fridge_Type>().HasData(
                new Fridge_Type { FridgeTypeID = 001, FridgeType = "Chest Freezer", Availablity= true, SupplierID = 789},
                new Fridge_Type { FridgeTypeID = 002, FridgeType = "Drawer Fridge", Availablity = true, SupplierID = 456},
                new Fridge_Type { FridgeTypeID = 003, FridgeType = "Commercial Fridge", Availablity = true, SupplierID = 123},
                new Fridge_Type { FridgeTypeID = 004, FridgeType = "Mini Fridge", Availablity = true, SupplierID = 101},
                new Fridge_Type { FridgeTypeID = 005, FridgeType = "French Door Fridge", Availablity = true, SupplierID = 112}
                );
            builder.Entity<Roles>().HasData(
                new Roles { RoleID = 1, RoleName = "Customer"},
                new Roles { RoleID = 2, RoleName = "Fault Technician"},
                new Roles { RoleID = 3, RoleName = "Maintenance Technician"},
                new Roles { RoleID = 4, RoleName = "Stock Controller"},
                new Roles { RoleID = 5, RoleName = "Customer Service"},
                new Roles { RoleID = 6, RoleName = "Purchasing Manager"},
                new Roles { RoleID = 7, RoleName = "Administrator"}
                );
            builder.Entity<OrderStatus>().HasData(
                new OrderStatus { OrderStatusId = 1, OrderDesc = "Complete" },
                new OrderStatus { OrderStatusId = 2, OrderDesc = "Pending" },
                new OrderStatus { OrderStatusId = 3, OrderDesc = "Cancelled" }
                );
        }
    }
}
