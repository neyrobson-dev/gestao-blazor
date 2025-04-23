using Microsoft.EntityFrameworkCore;
using Gestao.Domain.Libraries.Utilities;
using Gestao.Domain;
using Gestao.Domain.Repositories;

namespace Gestao.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Category>> GetAll(int companyId, int pagaIndex, int pageSize, string? searchWord = "")
        {
            var items = await _context.Categories.Where(a => a.CompanyId == companyId)
                .Where(a => string.IsNullOrEmpty(searchWord) || a.Name.Contains(searchWord))
                .OrderBy(a => a.Name)
                .Skip((pagaIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _context.Categories
                .Where(a => a.CompanyId == companyId)
                .Where(a => string.IsNullOrEmpty(searchWord) || a.Name.Contains(searchWord))
                .CountAsync();
            int totalPages = (int)Math.Ceiling((decimal)count / pageSize);
            return new PaginatedList<Category>(items, pagaIndex, totalPages);
        }

        public async Task<List<Category>> GetAll(int companyId)
        {
            return await _context.Categories.Where(a => a.CompanyId == companyId).ToListAsync();
        }

        public async Task<Category?> Get(int id)
        {
            return await _context.Categories.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(Category entity)
        {
            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Category entity)
        {
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Categories.FindAsync(id);
            if (entity != null)
            {
                _context.Categories.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
