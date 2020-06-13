using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Poc_NoSql.Infra.Repositories
{
    public interface ITariffRepository
    {
        Task<IEnumerable<Models.Tariff>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Models.Tariff>> GetByCompanyNameAsync(string companyName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Models.Tariff>> GetByContainsCompanyNameAsync(string companyName, CancellationToken cancellationToken = default);
        Task<bool> InsertAsync(Models.Tariff tarifaNova, CancellationToken cancellationToken = default);
        Task<decimal> GetyCompanyNameMaxTariffAsync(string companyName, CancellationToken cancellationToken = default);
    }
}
