using ImmunIt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Web;

namespace ImmunIt.DAL
{
    /*This class handles interactions with database*/
    public class DataLayer : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ImmunCard>().ToTable("ImmunCards");
            modelBuilder.Entity<Medic>().ToTable("Medics");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Vaccine>().ToTable("Vaccines");
        }

        /*Db Sets*/
        public DbSet<ImmunCard> ImmunCards { get; set; }
        public DbSet<Medic> Medics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
    }
}