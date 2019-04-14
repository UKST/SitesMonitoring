using System;

namespace SitesMonitoring.BLL.Utils
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}