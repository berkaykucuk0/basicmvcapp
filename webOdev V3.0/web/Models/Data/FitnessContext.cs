using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using web.DAL;
using web.Models;

namespace web.Models.Data
{
    public class FitnessContext: DbContext  // db den miras alınıyor databasedeki tabloları birbirine bağlıyor
    {
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Duyuru> Duyuru { get; set; }
        public DbSet<Modul> Modul { get; set; }
        public DbSet<Oneri> Oneri { get; set; }
        public DbSet<Referans> Referans { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Takim> Takim { get; set; }
        public DbSet<LoginViewModel> User { get; set; }

        // Constructor with connection string name
        public FitnessContext() : base("DefaultConnection")
        {
        }
    }
}