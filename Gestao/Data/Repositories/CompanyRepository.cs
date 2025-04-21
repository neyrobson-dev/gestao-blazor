using Gestão.Domain;
using Gestão.Domain.Libraries.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Company>> GetAll(Guid applicationUserId, int pagaIndex, int pageSize, string? searchWord = "")
        {
            var items = await _context.Companies.Where(a => a.UserId == applicationUserId)
                .Where(a => string.IsNullOrEmpty(searchWord) || a.TradeName.Contains(searchWord) || a.LegalName.Contains(searchWord))
                .OrderBy(a => a.TradeName)
                .Skip((pagaIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _context.Companies
                .Where(a => a.UserId == applicationUserId)
                .Where(a => string.IsNullOrEmpty(searchWord) || a.TradeName.Contains(searchWord) || a.LegalName.Contains(searchWord))
                .CountAsync();
            int totalPages = (int)Math.Ceiling((decimal)count / pageSize);
            return new PaginatedList<Company>(items, pagaIndex, totalPages);
        }

        public async Task<Company?> Get(int id)
        {
            return await _context.Companies.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(Company entity)
        {
            _context.Companies.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Company entity)
        {
            _context.Companies.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Companies.FindAsync(id);
            if (entity != null)
            {
                _context.Companies.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    }
}
