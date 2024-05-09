using Microsoft.EntityFrameworkCore;

namespace Cinema.Persistence;

public static class EntityFrameworkExtensions
{
	public static async Task<int> Create<T>(this DbContext dbContext, T entity) where T : class
	{
		dbContext.Add(entity);
		return await dbContext.SaveChangesAsync();
	}

	public static async Task<List<T>> ReadAll<T>(this DbSet<T> dbSet) where T : class
	{
		return await dbSet.ToListAsync();
	}

	public static async Task<T?> ReadById<T>(this DbSet<T> dbSet, Guid id) where T : class
	{
		return await dbSet.FindAsync(id);
	}
	public static async Task<int> UpdateExtension<T>(this DbContext dbContext, T entity) where T : class
	{
		dbContext.Update(entity);
		return await dbContext.SaveChangesAsync();
	}
	public static async Task<int> DeleteById<T>(this DbContext dbContext, Guid id) where T : class
	{
		var entityToDelete = await dbContext.FindAsync<T>(id);
		if (entityToDelete is null) return 0;

		dbContext.Remove(entityToDelete);
		return await dbContext.SaveChangesAsync();
	}
}