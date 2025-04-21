using Gestão.Domain;
using Gestão.Domain.Enums;
using Gestão.Domain.Libraries.Utilities;

namespace Gestao.Data.Repositories
{
    public interface IFinancialTransactionRepository
    {
        Task Add(FinancialTransaction entity);
        Task Delete(int id);
        Task<FinancialTransaction?> Get(int id);
        Task<PaginatedList<FinancialTransaction>> GetAll(int companyId, TypeFinancialTransaction type, int pagaIndex, int pageSize, string? searchWord);
        Task Update(FinancialTransaction entity);
    }
}