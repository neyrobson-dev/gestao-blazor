using Gestão.Domain.Libraries.Utilities;
using Gestão.Domain;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Account>> GetAll(int companyId, int pagaIndex, int pageSize, string? searchWord = "")
        {
            var items = await _context.Accounts.Where(a => a.CompanyId == companyId)
                .Where(a => string.IsNullOrEmpty(searchWord) || a.Description.Contains(searchWord))
                .OrderBy(a => a.Description)
                .Skip((pagaIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _context.Accounts
                .Where(a => a.CompanyId == companyId)
                .Where(a => string.IsNullOrEmpty(searchWord) || a.Description.Contains(searchWord))
                .CountAsync();
            int totalPages = (int)Math.Ceiling((decimal)count / pageSize);
            return new PaginatedList<Account>(items, pagaIndex, totalPages);
        }

        public async Task<List<Account>> GetAll(int companyId)
        {
            return await _context.Accounts.Where(a => a.CompanyId == companyId).ToListAsync();
        }

        public async Task<Account?> Get(int id)
        {
            return await _context.Accounts.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(Account entity)
        {
            _context.Accounts.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Account entity)
        {
            _context.Accounts.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Accounts.FindAsync(id);
            if (entity != null)
            {
                _context.Accounts.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
