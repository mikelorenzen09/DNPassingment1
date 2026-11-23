namespace EfcRepositories;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

public class EfcUserRepository : IUserRepository
{
    private readonly AppDbContext ctx;

    public EfcUserRepository(AppDbContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<User> AddAsync(User user)
    {
        EntityEntry<User> entityEntry = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(User user)
    {
        if (!await ctx.Users.AnyAsync(u => u.UserID == user.UserID))
        {
            throw new NotFoundException($"User with ID '{user.UserID}' not found");
        }

        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.UserID == id);
        if (existing is null)
        {
            throw new NotFoundException($"User with ID '{id}' not found");
        }

        ctx.Users.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        User? user = await ctx.Users.SingleOrDefaultAsync(u => u.UserID == id);
        if (user is null)
        {
            throw new NotFoundException($"User with ID '{id}' not found");
        }
        return user;
    }

    public IQueryable<User> GetMany()
    {
        return ctx.Users.AsQueryable();
    }
}
