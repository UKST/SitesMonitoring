using System;

namespace SitesMonitoring.BLL.ErrorHandling
{
    public class ApplicationValidationException : Exception
    {
        public ApplicationValidationException(string message) : base(message)
        {
        }
    }
}