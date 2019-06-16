using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NPVCalculator.Api.Contexts;
using NPVCalculator.Api.Models;

namespace NPVCalculator.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;
        
        public IParameterRepository Parameters { get; private set; }
        public ICalculationRepository Calculations { get; private set; }

        public UnitOfWork(DbContext context)
        {
            _context = context;
            
            Parameters = new ParameterRepository(_context);
            Calculations = new CalculationRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context = null;
        }
    }
}