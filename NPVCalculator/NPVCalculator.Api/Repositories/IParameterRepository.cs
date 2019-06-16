using System;
using System.Collections.Generic;
using NPVCalculator.Api.Models;

namespace NPVCalculator.Api.Repositories
{
    public interface IParameterRepository : IRepository<Parameter>
    {
        Parameter GetLast();
    }
}