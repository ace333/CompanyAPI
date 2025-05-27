using CompanyAPI.Domain.Entities;
using FluentAssertions;

namespace CompanyAPI.Domain.Tests
{
    public class CompanyEntityTests
    {
        [Fact]
        public void Company_Update_ShouldCorrectlyUpdateEntity()
        {
            // arrange
            var company = new Company("DB1234567890", "Name", "Exchange", "Ticker");
            var newIsin = "AB0123456789";
            var newName = "NewName";
            var newExchange = "NewExchange";
            var newTicker = "NewTicker";
            var website = "http://website.com";

            // act
            company.Update(newIsin, newName, newExchange, newTicker, website);

            // assert
            company.Isin.Should().Be(newIsin);
            company.Name.Should().Be(newName);
            company.Exchange.Should().Be(newExchange);
            company.Ticker.Should().Be(newTicker);
            company.Website.Should().Be(website);
        }
    }
}
