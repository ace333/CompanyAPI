namespace CompanyAPI.Application.Validation
{
    public interface IIsinValidator
    {
        bool ValidateFormat(string isin);
        Task<bool> ValidateUniquenessAsync(string isin, int? id = null);
    }
}
