using System;

namespace SitesMonitoring.BLL.Utils
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}