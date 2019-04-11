using System.Collections.Generic;
using System.Linq;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Endpoints
{
    public class Handler : IHandler
    {
        private readonly IRepository _repository;
        
        public Handler(IRepository repository)
        {
            _repository = repository;
        }
        
        public ICollection<string> HandleGet()
        {
            return _repository.GetAll().ToArray();
        }
    }
}