using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackaton2.Model
{
    public class ModelBase : DbContext
    {
        public DbSet<Sight> Sight { get; set; } = null!;
        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<SubCategory> SubCategory { get; set; } = null!;
        public ModelBase(DbContextOptions<ModelBase> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Coordinate>().HasNoKey();
        //}
    }
}
