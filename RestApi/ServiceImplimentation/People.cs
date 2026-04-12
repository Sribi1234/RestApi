using Microsoft.EntityFrameworkCore;
using RestApi.IService;


namespace RestApi.ServiceImplimentation
{
    public class People 
    {
      
        private readonly ILogger<People> _logger;

        public People( ILogger<People> logger)
        {
         
            _logger = logger;
        }

       
    }
}
