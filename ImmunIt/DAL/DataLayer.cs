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
            modelBuilder.Entity<ImmuneCard>().ToTable("ImmuneCards");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Manager>().ToTable("Managers");
            modelBuilder.Entity<Medic>().ToTable("Medics");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Vaccine>().ToTable("Vaccines");
        }

        /*Db Sets*/
        public DbSet<ImmuneCard> ImmuneCards { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Manager> managers { get; set; }
        public DbSet<Medic> medics { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }

    }
}