using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MunicipalityTaxCaculator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalityTaxCaculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly ITaxservice _taxservice;
        private readonly ICalculateTax _calculateTax;
        private readonly ILogger<TaxCalculatorController> _logger;

        public TaxCalculatorController(ITaxservice taxservice, ICalculateTax calculateTax, ILogger<TaxCalculatorController> logger)
        {
            _taxservice = taxservice;
            _calculateTax = calculateTax;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<ICalculateTax> Get(string municipality, string date)
        {
            var tax =  _calculateTax.CalculateMunicipalTax(municipality, date);
            if (tax.Count == 0)
            {
                _logger.LogError("404 not found");
                return NotFound();
            }
            return Ok(tax);
        }
    }
}
