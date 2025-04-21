using Gestão.Domain.Libraries.Utilities;
using Gestão.Domain;
using Microsoft.EntityFrameworkCore;
using Gestão.Domain.Enums;

namespace Gestao.Data.Repositories
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public FinancialTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<FinancialTransaction>> GetAll(int companyId, TypeFinancialTransaction type, int pagaIndex, int pageSize, string? searchWord = "")
        {
            var items = await _context.FinancialTransactions.Where(a => a.CompanyId == companyId && a.TypeFinancialTransaction == type)
                .Where(a => string.IsNullOrEmpty(searchWord) || a.Description.Contains(searchWord))
                .OrderByDescending(a => a.ReferenceDate)
                .Skip((pagaIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _context.FinancialTransactions
                .Where(a => a.CompanyId == companyId && a.TypeFinancialTransaction == type)
                .Where(a => string.IsNullOrEmpty(searchWord) || a.Description.Contains(searchWord))
                .CountAsync();
            int totalPages = (int)Math.Ceiling((decimal)count / pageSize);
            return new PaginatedList<FinancialTransaction>(items, pagaIndex, totalPages);
        }

        public async Task<FinancialTransaction?> Get(int id)
        {
            return await _context.FinancialTransactions
                .Include(a => a.Documents)
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(FinancialTransaction entity)
        {
            _context.FinancialTransactions.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(FinancialTransaction entity)
        {
            _context.FinancialTransactions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.FinancialTransactions.FindAsync(id);
            if (entity != null)
            {
                _context.FinancialTransactions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
