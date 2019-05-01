using Microsoft.EntityFrameworkCore;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.DAL
{
    public class SitesMonitoringDbContext : DbContext
    {
        public DbSet<Site> Sites { get; set; }
        public DbSet<MonitoringEntity> MonitoringEntities { get; set; }
        public DbSet<MonitoringResult> MonitoringResults { get; set; }
 
        public SitesMonitoringDbContext(DbContextOptions options)
            : base(options)
        { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // todo - move connection string to config
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=sitesMonitoring;Username=postgres;Password=");
        }
    }
}