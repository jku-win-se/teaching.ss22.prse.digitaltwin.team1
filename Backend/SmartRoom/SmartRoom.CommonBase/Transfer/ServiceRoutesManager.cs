using Microsoft.Extensions.Configuration;
using SmartRoom.CommonBase.Transfer.Contracts;

namespace SmartRoom.CommonBase.Transfer
{
    public class ServiceRoutesManager : IServiceRoutesManager
    {
        private string _baseDataServiceURL => _configuration["Services:BaseDataService"];
        private string _transDataServiceURL => _configuration["Services:TransDataService"];
        private string _dataSimulatorURL => _configuration["Services:DataSimulatorService"];
        private string _apiKey => _configuration["ApiKey"];

        private readonly IConfiguration _configuration;
        public ServiceRoutesManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BaseDataService => string.IsNullOrEmpty(_baseDataServiceURL) ? throw new ArgumentNullException("No configuration available") : _baseDataServiceURL;
        public string TransDataService => string.IsNullOrEmpty(_transDataServiceURL) ? throw new ArgumentNullException("No configuration available") : _transDataServiceURL;
        public string DataSimulatorService => string.IsNullOrEmpty(_dataSimulatorURL) ? throw new ArgumentNullException("No configuration available") : _dataSimulatorURL;
        public string ApiKey => string.IsNullOrEmpty(_apiKey) ? throw new ArgumentNullException("No configuration available") : _apiKey;
    }
}
