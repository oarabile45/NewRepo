using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cool_Co_Fridge_Management.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Cool_Co_Fridge_Management.Data.Extentions;

namespace Cool_Co_Fridge_Management.Data
{
    public class ApplicationDbContext : DbContext //gonna add the application user part. remove it again if this doesn't work
    {
        public DbSet<Users> users { get; set; }
        public DbSet<Fridge_Stock> stock { get; set; }
        public DbSet<Fridge_Type> fridge_type { get; set; }
        public DbSet<Supplier> suppliers { get; set; }
        public DbSet<Status> statuses { get; set; }
        public DbSet<FaultType> faultTypes { get; set; }
        public DbSet<FridgeFault> fridgeFaults { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
        public DbSet<Roles> roles { get;set; }
        public DbSet<RFQuotation> RFQuotation { get; set; }
        public DbSet<PurchaseOrder> orders { get; set; }
        public DbSet<MaintenanceTech> MaintenanceTech { get; set; }
        public DbSet<FaultTech> FaultTech { get; set; }
        public DbSet<Fault> Faults { get; set; }
        public DbSet<OrderStatus> orderStatus { get; set; }
        public DbSet<DeliveryNote> DeliveryNotes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<FridgeAllocation> FridgeAllocation { get; set; }
        public DbSet<FridgeRequest> FridgeRequests { get; set; }
        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }
        public DbSet<FridgeCondition> FridgeConditions { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Users>().ToTable("Users");

            builder.Entity<Users>()
                .Property(u => u.RoleId)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Entity<Users>()
                .HasOne(u => u.Roles)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

           
                

            builder.Seed();

        }
        
    }
}
