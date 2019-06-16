using System;
using System.Collections.Generic;
using NPVCalculator.Api.Models;

namespace NPVCalculator.Api.Repositories
{
    public interface ICalculationRepository : IRepository<Calculation>
    {
        IEnumerable<Calculation> GetByParameterId(long parameterId);
    }
}