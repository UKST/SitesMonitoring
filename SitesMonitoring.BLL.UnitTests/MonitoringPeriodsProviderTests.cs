using System;
using FluentAssertions;
using Moq;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.MonitoringWorker;
using SitesMonitoring.BLL.Utils;
using Xunit;

namespace SitesMonitoring.BLL.UnitTests
{
    public class MonitoringPeriodsProviderTests
    {
        private const int DefaultMonitoringPeriodInMinutes = 5;
        private readonly DateTime _now = new DateTime(2019, 1, 1);

        private readonly MonitoringPeriodsProvider _monitoringPeriodsProvider;

        private readonly Mock<IMonitoringSettings> _monitoringSettingsMock;
        private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

        public MonitoringPeriodsProviderTests()
        {
            _monitoringSettingsMock = new Mock<IMonitoringSettings>();
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();

            _monitoringPeriodsProvider = new MonitoringPeriodsProvider(
                _monitoringSettingsMock.Object,
                _dateTimeProviderMock.Object);
        }

        [Fact]
        public void GetMonitoringStartDueTime_TimeRequiredToStart_MonitoringPeriodDueTime()
        {
            // Arrange
            _dateTimeProviderMock.SetupGet(i => i.Now).Returns(_now);
            _monitoringSettingsMock.SetupGet(i => i.AvailableMonitoringPeriodsInMinutes)
                .Returns(new[] {DefaultMonitoringPeriodInMinutes});

            // Act
            var dueTime = _monitoringPeriodsProvider.GetMonitoringStartDueTime();

            // Assert
            dueTime.Should().Be(TimeSpan.FromMinutes(DefaultMonitoringPeriodInMinutes));
        }

        [Fact]
        public void GetMonitoringStartDueTime_NoMonitoringPeriods_InfiniteDueTime()
        {
            // Arrange
            _dateTimeProviderMock.SetupGet(i => i.Now).Returns(_now);
            _monitoringSettingsMock.SetupGet(i => i.AvailableMonitoringPeriodsInMinutes)
                .Returns(new int[0]);
            _monitoringSettingsMock.SetupGet(i => i.AvailableMonitoringPeriodsInHours)
                .Returns(new int[0]);

            // Act
            var dueTime = _monitoringPeriodsProvider.GetMonitoringStartDueTime();

            // Assert
            dueTime.Should().Be(TimeSpan.MaxValue);
        }
    }
}