using Repository.Models;

namespace Repository.Interface
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();

        Task<Author?> GetAuthorByIdAsync(int id);

        Task CreateAuthorAsync(Author author);

        Task<bool> DeleteAuthorAsync(int id);   
        Task<bool> UpdateAuthorAsync(Author author);

    }
}
