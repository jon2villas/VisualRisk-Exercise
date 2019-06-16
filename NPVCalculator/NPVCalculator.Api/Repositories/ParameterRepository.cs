using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NPVCalculator.Api.Models;

namespace NPVCalculator.Api.Repositories
{
    public class ParameterRepository : Repository<Parameter>, IParameterRepository
    {
        private readonly DbSet<Parameter> _parameters;

        public ParameterRepository(DbContext context)
            : base(context)
        {
            _parameters = context.Set<Parameter>();
        }

        public Parameter GetLast()
        {
            return _parameters.LastOrDefault();
        }
    }
}