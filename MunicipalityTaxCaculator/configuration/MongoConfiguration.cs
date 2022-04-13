using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalityTaxCaculator.configuration
{
    [ExcludeFromCodeCoverage]
    public class MongoConfiguration
    {
        public string MongoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
