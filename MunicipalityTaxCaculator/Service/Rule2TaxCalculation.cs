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
    public class Rule2TaxCalculation : ICalculateTax
    {
        public List<TaxModel> CalculateMunicipalTax(string municipality, string date, [Optional] List<Rules> list)
        {
           Logger.writeLog("CalculateMunicipalTax with rule2 functionality started with details" + list);
            List<TaxModel> taxModels = new List<TaxModel>();
            double totalTax = 0;
            double yearlyTax = 0;
            double monthlyTax = 0;
            double weeklyTax = 0;
            double dailyTax = 0;
            int yearlyInterval = 0;
            int monthlyInterval = 0;
            int weeklyInterval = 0;
            int dailyInterval = 0;
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
                                yearlyInterval = ((TimeSpan)(yearlyendDate - yearlystartDate)).Days;
                                yearlyTax = element.yearlyTax;
                            }
                        }
                        if (!string.IsNullOrEmpty(element.monthlyTax.ToString()) && element.monthlyTax != 0)
                        {
                            if (givenDate >= monthlystartDate && givenDate <= monthlyendDate)
                            {
                                monthlyInterval = ((TimeSpan)(monthlyendDate - monthlystartDate)).Days;
                                monthlyTax = element.monthlyTax;
                            }
                        }
                        if (!string.IsNullOrEmpty(element.weeklyTax.ToString()) && element.weeklyTax != 0)
                        {
                            if (givenDate >= weeklystartDate && givenDate <= weeklyendDate)
                            {
                                weeklyInterval = ((TimeSpan)(weeklyendDate - weeklystartDate)).Days;
                                weeklyTax = element.weeklyTax;
                            }
                        }
                        if (!string.IsNullOrEmpty(element.dailyTax.ToString()) && element.dailyTax != 0)
                        {
                            if (givenDate == dailystartDate)
                            {
                                dailyInterval = 1;
                                dailyTax = element.dailyTax;
                            }

                        }

                    }
                }

                int[] intervals = { yearlyInterval, monthlyInterval, weeklyInterval, dailyInterval };
                int shortInterval = intervals.Where(x => x != 0).DefaultIfEmpty().Min();

                if (shortInterval == yearlyInterval)
                {
                    totalTax = yearlyTax;
                }
                else if (shortInterval == monthlyInterval)
                {
                    totalTax = monthlyTax;
                }
                else if (shortInterval == weeklyInterval)
                {
                    totalTax = weeklyTax;
                }
                else
                {
                    totalTax = dailyTax;
                }

                List<ResultModel> resultModels = new List<ResultModel>()
            {
               new ResultModel() { municipality = municipality , date = date, rule = "2", taxAmount = totalTax }
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
