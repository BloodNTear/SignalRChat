using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class SignalRDBContext : DbContext
	{
		public DbSet<Account> accounts { get; set; }
		public DbSet<ChatGroup> chatGroups { get; set; }
		public DbSet<Message> messages { get; set; }
		public SignalRDBContext()
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

			string connectionString = config.GetConnectionString("default");

			optionsBuilder.UseSqlServer(connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Account>().HasMany(acc => acc.Messages).WithOne(mess => mess.FromAccount);

			modelBuilder.Entity<ChatGroup>().HasMany(cg => cg.Messages).WithOne(mess => mess.ToChatGroup);

			modelBuilder.Entity<ChatGroup>().HasMany(cg => cg.Accounts).WithMany(acc => acc.ChatGroups);

			modelBuilder.Entity<Account>().HasIndex(acc => acc.Username).IsUnique(true);

			modelBuilder.Entity<ChatGroup>().HasIndex(group =>group.GroupName).IsUnique(true);
		}
	}
}
