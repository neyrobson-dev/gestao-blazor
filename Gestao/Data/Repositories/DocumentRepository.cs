using Microsoft.EntityFrameworkCore;
using Gestao.Domain;
using Gestao.Domain.Repositories;

namespace Gestao.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Document?> Get(int id)
        {
            return await _context.Documents.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(Document entity)
        {
            _context.Documents.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Document entity)
        {
            _context.Documents.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Documents.FindAsync(id);
            if (entity != null)
            {
                _context.Documents.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
