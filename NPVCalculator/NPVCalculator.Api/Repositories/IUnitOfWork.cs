using System;
using System.Threading.Tasks;

namespace NPVCalculator.Api.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IParameterRepository Parameters { get; }
        ICalculationRepository Calculations { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}