using System;
using System.Net.NetworkInformation;
using System.Text;
using SitesMonitoring.BLL.Utils;

namespace SitesMonitoring.BLL.Monitoring.MonitoringWorker
{
    public class PingMonitoringProcess : IMonitoringProcess
    {
        private readonly IMonitoringResultRepository _monitoringResultRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        
        private const int Timeout = 1024;
        private const string BufferString = "32 bites data buffer            ";

        public PingMonitoringProcess(
            IMonitoringResultRepository monitoringResultRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _monitoringResultRepository = monitoringResultRepository;
            _dateTimeProvider = dateTimeProvider;
        }
        
        public void Start(MonitoringEntity entity)
        {
            var pingSender = new Ping();
            var options = new PingOptions
            {
                DontFragment = true
            };
    
            var buffer = Encoding.ASCII.GetBytes(BufferString);

            PingReply reply = null;

            try
            {
                reply = pingSender.Send(entity.Address, Timeout, buffer, options);
            }
            catch (PingException)
            {
                // todo logging    
            }
            
            _monitoringResultRepository.Create(new MonitoringResult
            {
                Data = reply?.Status ?? IPStatus.Unknown,
                CreatedDate = _dateTimeProvider.Now,
                MonitoringEntityId = entity.Id
            });
        }
    }
}