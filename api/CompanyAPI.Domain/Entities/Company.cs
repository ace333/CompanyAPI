using CompanyAPI.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace CompanyAPI.Domain.Entities
{
    public class Company : Entity
    {
        public Company(string isin, string name, string exchange, string ticker, string? website = null)
        {
            Isin = isin;
            Name = name;
            Exchange = exchange;
            Ticker = ticker;
            Website = website;
        }

        [MaxLength(12)]
        public string Isin { get; protected set; }
        [MaxLength(255)]
        public string Name { get; protected set; }
        [MaxLength(255)]
        public string Exchange { get; protected set; }
        [MaxLength(255)]
        public string Ticker { get; protected set; }
        [MaxLength(255)]
        public string? Website { get; protected set; }

        public void Update(string isin, string name, string exchange, string ticker, string? website = null)
        {
            Isin = isin;
            Name = name;
            Exchange = exchange;
            Ticker = ticker;
            Website = website;
        }
    }
}
