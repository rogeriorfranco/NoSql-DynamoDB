using Microsoft.AspNetCore.Mvc;
using Poc_NoSql.Infra.Repositories;
using System.Threading.Tasks;

namespace Poc_NoSql.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TariffController : ControllerBase
    {
        private readonly ITariffRepository _tariffRepository;
        public TariffController(ITariffRepository tariffRepository) => _tariffRepository = tariffRepository;

        [HttpGet("listAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var tariff = await _tariffRepository.GetAllAsync();

            return Ok(tariff);
        }

        [HttpGet("listCompanyName/{companyName}")]
        public async Task<IActionResult> GetByCompanyNameAsync(string companyName)
        {
            var tariff = await _tariffRepository.GetByCompanyNameAsync(companyName);

            return Ok(tariff);
        }

        [HttpGet("ContainsCompanyName/{companyName}")]
        public async Task<IActionResult> GetByContainsNomeAsync(string companyName)
        {
            var tariff = await _tariffRepository.GetByContainsCompanyNameAsync(companyName);

            return Ok(tariff);
        }

        [HttpGet("listMaxCompanyName/{companyName}")]
        public async Task<IActionResult> GetMaxTarifaAsync(string companyName)
        {
            var tariff = await _tariffRepository.GetyCompanyNameMaxTariffAsync(companyName);

            return Ok(tariff);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertAsync([FromBody]Models.Tariff data)
        {
            var tariff = await _tariffRepository.InsertAsync(data);

            return Ok(tariff);
        }
    }
}
