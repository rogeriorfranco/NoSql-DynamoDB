using ServiceStack.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Poc_NoSql.Infra.Repositories
{
    public class TariffRepository : ITariffRepository
    {
        private readonly IPocoDynamo _pocoDynamo;
        public TariffRepository(IPocoDynamo pocoDynamo) => _pocoDynamo = pocoDynamo;


        public async Task<IEnumerable<Models.Tariff>> GetAllAsync(CancellationToken cancellationToken = default)
       => await Task.FromResult(_pocoDynamo.GetAll<Models.Tariff>());

        public async Task<IEnumerable<Models.Tariff>> GetByCompanyNameAsync(string companyName, CancellationToken cancellationToken = default)
        => await Task.FromResult(_pocoDynamo.FromScan<Models.Tariff>(x => x.CompanyName == companyName).Exec().ToArray());

        public async Task<IEnumerable<Models.Tariff>> GetByContainsCompanyNameAsync(string companyName, CancellationToken cancellationToken = default)
        => await Task.FromResult(_pocoDynamo.FromScan<Models.Tariff>(x => x.CompanyName.Contains(companyName)).Exec().ToArray());

        public async Task<decimal> GetyCompanyNameMaxTariffAsync(string companyName, CancellationToken cancellationToken = default)
        => await Task.FromResult(_pocoDynamo.FromScan<Models.Tariff>(x => x.CompanyName == companyName).Exec().Max(x => x.TariffCurrency));

        public async Task<Models.Tariff> GetByDtExpiracaoAsync(string companyName, string category, string funcionality)
        => await Task.FromResult(_pocoDynamo.FromScan<Models.Tariff>(x => x.ExpirationDate == null && x.CompanyName == companyName
        && x.Category == category && x.Funcionality == funcionality).Exec().FirstOrDefault());

        public async Task<bool> InsertAsync(Models.Tariff tarifaNova, CancellationToken cancellationToken = default)
        {
            var tarifaAntiga = await GetByDtExpiracaoAsync(tarifaNova.CompanyName, tarifaNova.Category, tarifaNova.Funcionality);

            if (tarifaAntiga != null)
            {
                tarifaAntiga.ExpirationDate = DateTime.UtcNow;

                await Task.Run(() => _pocoDynamo.PutItem(tarifaAntiga, true));
            }

            await Task.Run(() => _pocoDynamo.PutItem(tarifaNova, true));

            return true;
        }
    }
}

