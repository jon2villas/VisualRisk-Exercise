using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NPVCalculator.Api.Models;

namespace NPVCalculator.Api.Contexts
{
    public class NPVCalculatorContext : DbContext
    {
        public NPVCalculatorContext(DbContextOptions<NPVCalculatorContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<CashFlow> CashFlows { get; set; }
        public DbSet<Calculation> Calculations { get; set; }
    }
}