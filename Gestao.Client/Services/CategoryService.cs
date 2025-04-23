using Gestao.Domain;
using Gestao.Domain.Libraries.Utilities;
using Gestao.Domain.Repositories;
using System.Net.Http.Json;

namespace Gestao.Client.Services
{
    public class CategoryService : ICategoryRepository
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task Add(Category category)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAll(int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedList<Category>> GetAll(int companyId, int pageIndex, int pageSize, string? searchWord = "")
        {
            var url = $"/api/category?companyId={companyId}&pageIndex={pageIndex}&searchWord={searchWord}";
            var entities = await _httpClient.GetFromJsonAsync<PaginatedList<Category>>(url);

            return entities!;
        }

        public Task Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
