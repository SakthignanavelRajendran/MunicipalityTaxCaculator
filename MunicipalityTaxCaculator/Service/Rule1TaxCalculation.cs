using Microsoft.Extensions.Logging;
using MunicipalityTaxCaculator.Entity;
using MunicipalityTaxCaculator.Interface;
using MunicipalityTaxCaculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MunicipalityTaxCaculator.Service
{
    public class Rule1TaxCalculation : ICalculateTax
    {
        public List<TaxModel> CalculateMunicipalTax(string municipality1, string date, [Optional] List<Rules> list)
        {
            Logger.writeLog("CalculateMunicipalTax with rule1 functionality started with details" + list);
            List<TaxModel> taxModels = new List<TaxModel>();
            double totalTax = 0;
            DateTime givenDate = Convert.ToDateTime(date);
            DateTime? yearlystartDate = null;
            DateTime? yearlyendDate = null;
            DateTime? monthlystartDate = null;
            DateTime? monthlyendDate = null;
            DateTime? weeklystartDate = null;
            DateTime? weeklyendDate = null;
            DateTime? dailystartDate = null;
            DateTime? dailyendDate = null;
            try
            {
                if (list != null && list.Count != 0)
                {
                    foreach (var element in list)
                    {
                        if (!string.IsNullOrEmpty(element.yearFromDate.ToString()))
                        {
                            yearlystartDate = Convert.ToDateTime(element.yearFromDate);
                        }
                        if (!string.IsNullOrEmpty(element.yearToDate.ToString()))
                        {
                            yearlyendDate = Convert.ToDateTime(element.yearToDate);
                        }
                        if (!string.IsNullOrEmpty(element.monthFromDate.ToString()))
                        {
                            monthlystartDate = Convert.ToDateTime(element.monthFromDate);
                        }
                        if (!string.IsNullOrEmpty(element.monthToDate.ToString()))
                        {
                            monthlyendDate = Convert.ToDateTime(element.monthToDate);
                        }
                        if (!string.IsNullOrEmpty(element.weekFromDate.ToString()))
                        {
                            weeklystartDate = Convert.ToDateTime(element.weekFromDate);
                        }
                        if (!string.IsNullOrEmpty(element.weekToDate.ToString()))
                        {
                            weeklyendDate = Convert.ToDateTime(element.weekToDate);
                        }
                        if (element.dailyFromDate.Count > 0)
                        {
                            foreach (var value in element.dailyFromDate)
                            {
                                if (Convert.ToDateTime(value.dates) == givenDate)
                                {
                                    dailystartDate = Convert.ToDateTime(value.dates);
                                }
                            }

                        }
                        if (!string.IsNullOrEmpty(element.yearlyTax.ToString()) && element.yearlyTax != 0)
                        {
                            if (givenDate >= yearlystartDate && givenDate <= yearlyendDate)
                            {
                                totalTax += element.yearlyTax;
                            }
                        }
                        if (!string.IsNullOrEmpty(element.monthlyTax.ToString()) && element.monthlyTax != 0)
                        {
                            if (givenDate >= monthlystartDate && givenDate <= monthlyendDate)
                            {
                                totalTax += element.monthlyTax;
                            }
                        }
                        if (!string.IsNullOrEmpty(element.weeklyTax.ToString()) && element.weeklyTax != 0)
                        {
                            if (givenDate >= weeklystartDate && givenDate <= weeklyendDate)
                            {
                                totalTax += element.weeklyTax;
                            }
                        }
                        if (!string.IsNullOrEmpty(element.dailyTax.ToString()) && element.dailyTax != 0)
                        {
                            if (givenDate == dailystartDate)
                            {
                                totalTax += element.dailyTax;
                            }

                        }

                    }
                }

                List<ResultModel> resultModels = new List<ResultModel>()
            {
               new ResultModel() { municipality = municipality1 , date = date, rule = "1", taxAmount = totalTax }
            };
                taxModels = new List<TaxModel>()
            {
                new TaxModel() { result = resultModels}
            };
            }
            catch (Exception e)
            {
                Logger.writeLog(e.Message);
            }
            return taxModels;
        }

        public ICalculateTax CreateRuleType(string ruleType)
        {
            throw new NotImplementedException();
        }


    }
}
