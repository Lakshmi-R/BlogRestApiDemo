using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Models;


namespace Repository.Services;
// file-scoped namespace 
public class PostRepository : IPostRepository
{
    private readonly BlogDbContext dbContext;

    public PostRepository(BlogDbContext blogDbContext)
    {
        dbContext = blogDbContext;
    }
    public async Task<Post> CreatePostAysnc(Post post)
    {         
        dbContext.Posts.Add(post);
        await dbContext.SaveChangesAsync();
        return post;
    }

    public async Task<IEnumerable<Post>> GetAllPostAsync()
    {
       return await dbContext.Posts.Include(p => p.Author).ToListAsync();
    }

    public async Task<Post?> GetPostByIdAsync(int id)
    {
        return await dbContext.Posts.Include(p => p.Author).FirstOrDefaultAsync(x => x.Id == id);   
    }
}
