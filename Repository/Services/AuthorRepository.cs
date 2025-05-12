using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Repository.Interface;
using Repository.Models;



namespace Repository.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BlogDbContext db;

        public AuthorRepository(BlogDbContext dbContext)
        {
            db = dbContext;
        }
        public async Task CreateAuthorAsync(Author author)
        {
            await db.Author.AddAsync(author);
            await db.SaveChangesAsync();           
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await db.Author.FindAsync(id);
            if (author == null) { return false; }
            db.Author.Remove(author);
            await db.SaveChangesAsync(); 
            return true;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await db.Author.Include(p => p.Posts).ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await db.Author.Include(p => p.Posts).FirstOrDefaultAsync(x => x.Id == id);   
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            db.Author.Update(author);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
