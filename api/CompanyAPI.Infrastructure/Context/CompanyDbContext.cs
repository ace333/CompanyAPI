using CompanyAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Infrastructure.Context
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) 
            : base(options) { }

        public virtual DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .ToTable("Company")
                .HasData(new List<Company>
                {
                    new("US0378331005", "Apple Inc.", "NASDAQ", "AAPL", "http://www.apple.com") { Id = 1 },
                    new("US1104193065", "British Airways Plc", "Pink Sheets", "BAIRY") { Id = 2 },
                    new("NL0000009165", "Heineken NV", "Euronext Amsterdam", "HEIA") { Id = 3 },
                    new("JP3866800000", "Panasonic Corp", "Tokyo Stock Exchange", "6752", "http://www.panasonic.co.jp") { Id = 4 },
                    new("DE000PAH0038", "Porsche Automobil", "Deutsche Börse", "PAH3", "https://www.porsche.com/") { Id = 5 },
                });
        }
    }
}
