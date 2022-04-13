using Microsoft.Extensions.Logging;
using MunicipalityTaxCaculator.Entity;
using MunicipalityTaxCaculator.Interface;
using MunicipalityTaxCaculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Unity;

namespace MunicipalityTaxCaculator.Service
{
    public class CalculateTax : ICalculateTax
    {
        private readonly ITaxservice _taxservice;
        public static UnityContainer municipalityRulesCotainer = new UnityContainer();
        private readonly ILogger<CalculateTax> _logger;

        public CalculateTax(ITaxservice taxservice, ILogger<CalculateTax> logger)
        {
            _taxservice = taxservice;
            _logger = logger;
            municipalityRulesCotainer.RegisterType<ICalculateTax, Rule1TaxCalculation>("1");
            municipalityRulesCotainer.RegisterType<ICalculateTax, Rule2TaxCalculation>("2");
        }

        public List<TaxModel> CalculateMunicipalTax(string municipality, string date, [Optional] List<Rules> rulesList)
        {
            List<TaxModel> result = new List<TaxModel>();
            try
            {
                ICalculateTax calculate = null;
                string ruleType = string.Empty;
                List<TaxModel> taxModels = new List<TaxModel>();
                List<Rules> list = Task.Run(() => _taxservice.GetByIdAsync(municipality)).Result;
                if (list != null && list.Count != 0)
                {
                    foreach (var element in list)
                    {
                        if (element.rule != string.Empty)
                        {
                            ruleType = element.rule.ToString();
                        }
                    }
                }
                calculate = CreateRuleType(ruleType);
                result = calculate.CalculateMunicipalTax(municipality, date, list);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return result;
        }

        public ICalculateTax CreateRuleType(string ruleType)
        {
            ICalculateTax instance = null;
            try
            {
                instance = municipalityRulesCotainer.Resolve<ICalculateTax>(ruleType);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return instance;
        }
    }
}
