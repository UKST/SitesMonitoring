using Microsoft.EntityFrameworkCore;
using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.DAL
{
    public class SitesMonitoringDbContext : DbContext
    {
        public DbSet<Site> Sites { get; set; }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=sitesMonitoring;Username=postgres");
        }
    }
}