using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using My_Wedding_Manager.Models;

namespace My_Wedding_Manager.DataAccessLayer
{
    public class GuestsList: DbContext
    {
        public DbSet<Guest> dbGuestsList { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>().ToTable("TblGuestsList");
            base.OnModelCreating(modelBuilder);
        }
    }

}