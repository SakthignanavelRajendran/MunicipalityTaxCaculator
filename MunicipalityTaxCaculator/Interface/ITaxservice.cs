using MunicipalityTaxCaculator.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalityTaxCaculator.Interface
{
    public interface ITaxservice
    {
        Task<List<Rules>> GetAllAsync();
        Task<List<Rules>> GetByIdAsync(string id);
    }
}
