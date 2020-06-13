using ServiceStack.DataAnnotations;
using System;

namespace Poc_NoSql.Models
{
    public class Tariff
    {
        [AutoIncrement] public long TariffId { get; set; }
        public string Level { get; set; }
        public string CompanyName { get; set; }
        public string ValidationType { get; set; }
        public string Category { get; set; }
        public string Funcionality { get; set; }
        public decimal TariffCurrency { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpirationDate { get; set; }
    }
}
