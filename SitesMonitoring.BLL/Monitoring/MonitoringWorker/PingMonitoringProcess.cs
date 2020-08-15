using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI;
using SitesMonitoring.BLL.Utils;

namespace SitesMonitoring.BLL.Monitoring.MonitoringWorker
{
    public class PingMonitoringProcess : IMonitoringProcess
    {
        private readonly ILogger<PingMonitoringProcess> _logger;
        private readonly IRepository<MonitoringResult> _monitoringResultRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMonitoringRequest<PingReply> _pingMonitoringRequest;

        public PingMonitoringProcess(
            ILogger<PingMonitoringProcess> logger,
            IRepository<MonitoringResult> monitoringResultRepository,
            IDateTimeProvider dateTimeProvider,
            IMonitoringRequest<PingReply> pingMonitoringRequest)
        {
            _logger = logger;
            _monitoringResultRepository = monitoringResultRepository;
            _dateTimeProvider = dateTimeProvider;
            _pingMonitoringRequest = pingMonitoringRequest;
        }

        public async Task StartAsync(MonitoringEntity entity)
        {
            var result = new MonitoringResult
            {
                CreatedDate = _dateTimeProvider.Now,
                MonitoringEntityId = entity.Id
            };
            result.SetData(new PingMonitoringResultData(await GetIpStatus(entity)));

            _monitoringResultRepository.Create(result);
            await _monitoringResultRepository.SaveChangesAsync();
        }

        private async Task<IPStatus> GetIpStatus(MonitoringEntity entity)
        {
            try
            {
                var reply = await _pingMonitoringRequest.SendAsync(entity);
                return reply.Status;
            }
            catch (PingException e)
            {
                _logger.LogWarning(e, $"Unable to retrieve ip status for '{entity.Address}'");
                return IPStatus.Unknown;
            }
        }
    }
}