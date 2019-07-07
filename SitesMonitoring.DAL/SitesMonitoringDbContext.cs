using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SitesMonitoring.BLL.Configs;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.DAL
{
    public class SitesMonitoringDbContext : DbContext
    {
        private readonly IOptions<AppConfig> _config;
        
        public DbSet<Site> Sites { get; set; }
        public DbSet<MonitoringEntity> MonitoringEntities { get; set; }
        public DbSet<MonitoringResult> MonitoringResults { get; set; }

        public SitesMonitoringDbContext(DbContextOptions options, IOptions<AppConfig> config)
            : base(options)
        {
            _config = config;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // todo - move connection string to config
            // todo - support different environments with different connection string: at least one with host=postgres and one for host=localhost (docker / dev deployment)
            optionsBuilder.UseNpgsql(_config.Value.ConnectionStrings.DbConnectionString);
        }
    }
}