using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts
{
	public class UserDbContext : DbContext
	{
		public UserDbContext(DbContextOptions<UserDbContext> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
    }
}

