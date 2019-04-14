using System.Net.NetworkInformation;
using System.Text;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.Utils;

namespace SitesMonitoring.BLL.Monitoring.MonitoringWorker
{
    public class PingMonitoringProcess : IMonitoringProcess
    {
        private readonly IRepository<MonitoringResult> _monitoringResultRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        
        private const int Timeout = 1024;
        private const string BufferString = "32 bites data buffer            ";

        public PingMonitoringProcess(
            IRepository<MonitoringResult> monitoringResultRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _monitoringResultRepository = monitoringResultRepository;
            _dateTimeProvider = dateTimeProvider;
        }
        
        public void Start(MonitoringEntity entity)
        {
            var pingSender = new System.Net.NetworkInformation.Ping();
            var options = new PingOptions
            {
                DontFragment = true
            };
    
            var buffer = Encoding.ASCII.GetBytes(BufferString);

            var reply = pingSender.Send(entity.Address, Timeout, buffer, options);
            
            _monitoringResultRepository.Create(new MonitoringResult
            {
                Data = reply?.Status,
                CreatedDate = _dateTimeProvider.Now,
                MonitoringEntityId = entity.Id
            });
        }
    }
}