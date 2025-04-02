using Microsoft.EntityFrameworkCore;

namespace AssignmentDotNet.Model
{
    public class AssignmentDbContext : DbContext
    {
        public AssignmentDbContext(DbContextOptions<AssignmentDbContext> options) : base(options) { }
        public DbSet<Mobile> Mobile { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Discount> Discount { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sales>()
                .HasOne(s => s.Mobile)
                .WithMany(m => m.Sales)
                .HasForeignKey(s => s.MobileId);

            modelBuilder.Entity<Sales>()
                .HasOne(s => s.Discount)
                .WithMany()
                .HasForeignKey(s => s.DiscountId);

            modelBuilder.Entity<Discount>()
                .HasOne(d => d.Mobile)
                .WithMany(m => m.Discounts)
                .HasForeignKey(d => d.MobileId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
