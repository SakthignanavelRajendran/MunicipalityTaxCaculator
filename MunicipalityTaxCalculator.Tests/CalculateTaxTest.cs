using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MunicipalityTaxCaculator.configuration;
using MunicipalityTaxCaculator.Entity;
using MunicipalityTaxCaculator.Interface;
using MunicipalityTaxCaculator.Models;
using MunicipalityTaxCaculator.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityTaxCalculator.Tests
{
    class CalculateTaxTest
    {
        private ITaxservice _taxservice;
        private ILogger<CalculateTax> _logger;
        private IOptions<MongoConfiguration> settings;
        private readonly ILogger<Taxservice> _loggers;
       
        [Test]
        public void test()
        {
            List<TaxModel> result = new List<TaxModel>();
            ICalculateTax calculate = null;
            List<ResultModel> results = new List<ResultModel>
            {
                new ResultModel { date = "2020.01.01", municipality = "Municipality2", rule = "2", taxAmount = 0.3}
            };
            List<TaxModel> output = new List<TaxModel>
            {
                new TaxModel { result = results}
            };
            List<dailyFromDate> dailyFromDates = new List<dailyFromDate>();
            List<Rules> list = new List<Rules>
            {
                new Rules { id = "62555720d348dd58c88aacfc", name = "Municipality2", rule = "1", yearlyTax = 0.3, yearFromDate =  "2020.01.01", yearToDate = "2020.12.31", monthlyTax = 0.2, monthFromDate = "2020.01.01", monthToDate = "2020.01.31", weekFromDate = "2020.01.06", weekToDate = "2020.01.12", dailyTax = 0, weeklyTax = 0.1, dailyFromDate = dailyFromDates}
            };
            CalculateTax calculateTax = new CalculateTax(_taxservice, _logger);
            calculate = calculateTax.CreateRuleType("2");
            result = calculate.CalculateMunicipalTax("Municipality2", "2020.01.01", list);
            //Negative Scenario
            Assert.AreNotEqual(output, result, "Tax Not matching");
        }
    }
}
