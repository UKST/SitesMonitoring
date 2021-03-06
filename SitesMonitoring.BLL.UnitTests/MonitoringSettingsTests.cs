using System.Linq;
using FluentAssertions;
using SitesMonitoring.BLL.Monitoring;
using Xunit;

namespace SitesMonitoring.BLL.UnitTests
{
    public class MonitoringSettingsTests
    {
        private const int MinutesInHour = 60;
        private const int HoursInDay = 24;

        private readonly MonitoringSettings _monitoringSettings = new MonitoringSettings();

        [Fact]
        public void AvailableMonitoringPeriodsInMinutes_ShouldHaveMoreThanOneItem()
        {
            // Assert
            _monitoringSettings.AvailableMonitoringPeriodsInMinutes.Count.Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public void AvailableMonitoringPeriodsInMinutes_ShouldBeMoreOrEqualThan1AndLessThan60()
        {
            // Arrange
            var expectedPeriods = _monitoringSettings.AvailableMonitoringPeriodsInMinutes.Where(i => i >= 1 && i < MinutesInHour);
            var totalPeriodsCount = _monitoringSettings.AvailableMonitoringPeriodsInMinutes.Count;

            // Assert
            totalPeriodsCount.Should().Be(expectedPeriods.Count());
        }

        [Fact]
        public void AvailableMonitoringPeriodsInMinutes_ShouldBeAnIntegerPartOfHour()
        {
            // Arrange
            var expectedPeriods = _monitoringSettings.AvailableMonitoringPeriodsInMinutes.Where(i => MinutesInHour % i == 0);
            var totalPeriodsCount = _monitoringSettings.AvailableMonitoringPeriodsInMinutes.Count;

            // Assert
            totalPeriodsCount.Should().Be(expectedPeriods.Count());
        }

        [Fact]
        public void AvailableMonitoringPeriodsInHours_ShouldHaveMoreThanOneItem()
        {
            // Assert
            _monitoringSettings.AvailableMonitoringPeriodsInHours.Count.Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public void AvailableMonitoringPeriodsInHours_ShouldBeMoreOrEqualThan1AndLessOrEqualThan24()
        {
            // Arrange
            var expectedPeriods = _monitoringSettings.AvailableMonitoringPeriodsInHours.Where(i => i >= 1 && i <= HoursInDay);
            var totalPeriodsCount = _monitoringSettings.AvailableMonitoringPeriodsInHours.Count;

            // Assert
            totalPeriodsCount.Should().Be(expectedPeriods.Count());
        }

        [Fact]
        public void AvailableMonitoringPeriodsInHours_ShouldBeAnIntegerPartOfDay()
        {
            // Arrange
            var expectedPeriods = _monitoringSettings.AvailableMonitoringPeriodsInHours.Where(i => HoursInDay % i == 0);
            var totalPeriodsCount = _monitoringSettings.AvailableMonitoringPeriodsInHours.Count;

            // Assert
            totalPeriodsCount.Should().Be(expectedPeriods.Count());
        }

        // todo other tests
    }
}