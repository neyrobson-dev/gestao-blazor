using Gestão.Domain;
using Gestão.Domain.Libraries.Utilities;

namespace Gestao.Data.Repositories
{
    public interface ICompanyRepository
    {
        Task Add(Company company);
        Task Delete(int id);
        Task<Company?> Get(int id);
        Task<PaginatedList<Company>> GetAll(Guid applicationUserId, int pagaIndex, int pageSize, string? searchWord);
        Task Update(Company company);
    }
}