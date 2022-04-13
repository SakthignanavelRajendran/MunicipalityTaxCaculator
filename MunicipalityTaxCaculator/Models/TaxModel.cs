using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalityTaxCaculator.Models
{
    public class TaxModel
    {
        public List<ResultModel> result { get; set; }
    }

    public class ResultModel
    {
        public string municipality { get; set; }
        public string date { get; set; }
        public string rule { get; set; }
        public double taxAmount { get; set; }
    }
}
