using MunicipalityTaxCaculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using MunicipalityTaxCaculator.Entity;

namespace MunicipalityTaxCaculator.Interface
{
    public interface ICalculateTax
    {
        List<TaxModel> CalculateMunicipalTax(string municipality, string date, [Optional] List<Rules> list);
        ICalculateTax CreateRuleType(string ruleType);
    }
}
