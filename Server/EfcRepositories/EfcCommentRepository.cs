namespace EfcRepositories;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppDbContext ctx;

    public EfcCommentRepository(AppDbContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        EntityEntry<Comment> entityEntry = await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Comment comment)
    {
        if (!await ctx.Comments.AnyAsync(c => c.CommentID == comment.CommentID))
        {
            throw new NotFoundException($"Comment with ID '{comment.CommentID}' not found");
        }

        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(c => c.CommentID == id);
        if (existing is null)
        {
            throw new NotFoundException($"Comment with ID '{id}' not found");
        }

        ctx.Comments.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = await ctx.Comments.SingleOrDefaultAsync(c => c.CommentID == id);
        if (comment is null)
        {
            throw new NotFoundException($"Comment with ID '{id}' not found");
        }
        return comment;
    }

    public IQueryable<Comment> GetMany()
    {
        return ctx.Comments.AsQueryable();
    }
}
