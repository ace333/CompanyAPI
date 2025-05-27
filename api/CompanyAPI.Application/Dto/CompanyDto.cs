using System.ComponentModel.DataAnnotations;

namespace CompanyAPI.Application.Dto
{
    public sealed class CompanyDto
    {
        public static CompanyDto Empty() => new CompanyDto();

        public CompanyDto(int id, string isin, string name, string exchange, string ticker, string? website = null)
        {
            Id = id;
            Isin = isin;
            Name = name;
            Exchange = exchange;
            Ticker = ticker;
            Website = website;
        }

        protected CompanyDto() { }

        public int Id { get; private set; }
        public string Isin { get; private set; }
        public string Name { get; private set; }
        public string Exchange { get; private set; }        
        public string Ticker { get; private set; }
        public string? Website { get; private set; }
    }

}
