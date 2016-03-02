using MovieManagement.Models.DB_models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MovieManagement.DAL
{
    public class MoviesContext : DbContext
    {
        public MoviesContext()
            : base("MoviesContextConnString")
        {
            Database.SetInitializer<MoviesContext>(new MoviesInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


        // Table Sets
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Genres> Genres { get; set; }


    }
}