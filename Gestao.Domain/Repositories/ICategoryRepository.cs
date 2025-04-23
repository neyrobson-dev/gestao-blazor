using Gestao.Domain;
using Gestao.Domain.Libraries.Utilities;

namespace Gestao.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task Add(Category category);
        Task Delete(int id);
        Task<Category?> Get(int id);
        Task<List<Category>> GetAll(int companyId);
        Task<PaginatedList<Category>> GetAll(int companyId, int pagaIndex, int pageSize, string? searchWord = "");
        Task Update(Category category);
    }
}