using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalityTaxCaculator.Entity
{
    [ExcludeFromCodeCoverage]
    [BsonIgnoreExtraElements]
    public class Rules
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        public string rule { get; set; }
        public double yearlyTax { get; set; }
        public string yearFromDate { get; set; }
        public string yearToDate { get; set; }

        public double monthlyTax { get; set; }
        public string monthFromDate { get; set; }
        public string monthToDate { get; set; }

        public double weeklyTax { get; set; }
        public string weekFromDate { get; set; }
        public string weekToDate { get; set; }
        public double dailyTax { get; set; }
        public List<dailyFromDate> dailyFromDate { get; set; }
        public string dailyToDate { get; set; }
    }

    public class dailyFromDate
    {
        public string dates { get; set; }
    }

}
