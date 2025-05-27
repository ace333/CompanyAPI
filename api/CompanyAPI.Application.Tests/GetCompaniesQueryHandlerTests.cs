using CompanyAPI.Application.Query;
using CompanyAPI.Infrastructure.Repositories;
using FluentAssertions;
using Moq;

namespace CompanyAPI.Application.Tests
{
    public class GetCompaniesQueryHandlerTests
    {
        private Mock<ICompanyRepository> _companyRepositoryMock;

        public GetCompaniesQueryHandlerTests()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
        }

        [Fact]
        public async Task GetCompaniesQueryHandler_Handle_ShouldFailIfQueryIsNull()
        {
            // arrange
            var queryHandler = new GetCompanyByIdQueryHandler(_companyRepositoryMock.Object);

            // act
            var action = async () => await queryHandler.Handle(null, default);

            // assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
