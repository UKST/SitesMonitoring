using System.Net.NetworkInformation;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI;
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

            var result = new MonitoringResult
            {
                CreatedDate = _dateTimeProvider.Now,
                MonitoringEntityId = entity.Id
            };
            result.SetData(new PingMonitoringResultData {IPStatus = reply?.Status ?? IPStatus.Unknown});

            _monitoringResultRepository.Create(result);
        }
    }
}