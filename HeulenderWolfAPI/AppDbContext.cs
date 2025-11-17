// AppDbContext.cs

using HeulenderWolfAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HeulenderWolfAPI
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseInMemoryDatabase("HeulenderWolfAPIDatabase");
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Organizacao> Organizacoes { get; set; }
        public DbSet<Pet> Pets { get; set; }
    }
}
