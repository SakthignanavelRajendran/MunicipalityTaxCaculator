using MunicipalityTaxCaculator.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MunicipalityTaxCaculator.Entity;
using MunicipalityTaxCaculator.configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace MunicipalityTaxCaculator.Service
{
    public class Taxservice: ITaxservice
    {
        private readonly IMongoCollection<Rules> _Rules;
        private readonly MongoConfiguration _settings;
        private readonly ILogger<Taxservice> _logger;
        public Taxservice(IOptions<MongoConfiguration> settings, ILogger<Taxservice> logger)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _Rules = database.GetCollection<Rules>(_settings.MongoCollectionName);
            _logger = logger;
        }
        public async Task<List<Rules>> GetAllAsync()
        {
            List<Rules> masterList = new List<Rules>();
            masterList = await _Rules.Find(c => true).ToListAsync();
            return masterList;
        }
        public async Task<List<Rules>> GetByIdAsync(string id)
        {
            _logger.LogInformation("GetByIdAsync function started with municipality name" + id);
            List<Rules> filteredList = new List<Rules>();
            try
            {
                filteredList = await _Rules.Find<Rules>(c => c.name == id).ToListAsync();
                _logger.LogInformation("GetByIdAsync function returned details" + filteredList.ToString());
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }           
            return filteredList;
        }
    }
}
