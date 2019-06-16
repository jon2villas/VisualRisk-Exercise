using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NPVCalculator.Api.Models;

namespace NPVCalculator.Api.Repositories
{
    public class CalculationRepository : Repository<Calculation>, ICalculationRepository
    {
        private readonly DbSet<Calculation> _calculations;

        public CalculationRepository(DbContext context)
            : base(context)
        {
            _calculations = context.Set<Calculation>();
        }

        public IEnumerable<Calculation> GetByParameterId(long parameterId)
        {
            return _calculations
                .Include(c => c.Parameter)
                .ThenInclude(p => p.CashFlows)
                .Where(c => c.ParameterId == parameterId)
                .OrderBy(c => c.DiscountRate)
                .ToList();
        }
    }
}