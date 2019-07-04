using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Helsi.DataAccess.Models;

namespace Helsi.DataAccess
{
    public class HelsiContext : DbContext
    {
        public HelsiContext(DbContextOptions<HelsiContext> options) : base(options)
        {

        }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<AdditionalContact> AdditionalContacts { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Gender>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AdditionalContact>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ContactType>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
