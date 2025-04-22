using Gestão.Domain;
using Gestão.Domain.Libraries.Utilities;

namespace Gestao.Data.Repositories
{
    public interface IAccountRepository
    {
        Task Add(Account entity);
        Task Delete(int id);
        Task<Account?> Get(int id);
        Task<List<Account>> GetAll(int companyId);
        Task<PaginatedList<Account>> GetAll(int companyId, int pagaIndex, int pageSize, string? searchWord = "");
        Task Update(Account entity);
    }
}