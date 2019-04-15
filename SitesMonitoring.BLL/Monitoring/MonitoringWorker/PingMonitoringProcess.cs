using System.Net.NetworkInformation;
using SitesMonitoring.BLL.Utils;

namespace SitesMonitoring.BLL.Monitoring.MonitoringWorker
{
    public class PingMonitoringProcess : IMonitoringProcess
    {
        private readonly IMonitoringResultRepository _monitoringResultRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMonitoringRequest<PingReply> _pingMonitoringRequest;
        
        public PingMonitoringProcess(
            IMonitoringResultRepository monitoringResultRepository,
            IDateTimeProvider dateTimeProvider,
            IMonitoringRequest<PingReply> pingMonitoringRequest)
        {
            _monitoringResultRepository = monitoringResultRepository;
            _dateTimeProvider = dateTimeProvider;
            _pingMonitoringRequest = pingMonitoringRequest;
        }
        
        public void Start(MonitoringEntity entity)
        {
            PingReply reply = null;
            try
            {
                reply = _pingMonitoringRequest.Send(entity);
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