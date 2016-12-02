using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shared.Entity;
using MongoDB.Driver;
using System;
using System.Linq;

namespace WindowsApp.Models
{
 
    public class MongoDBContext : IDisposable
    {
        private readonly IMongoDatabase db;
        public MongoDBContext()
        {
            MongoClient client = new MongoClient("mongodb://192.168.1.109");
            db = client.GetDatabase("test");
        }
        public IMongoCollection<DailyPrice> DailyPrices
        {
            get
            {
                return this.db.GetCollection<DailyPrice>("DailyPrice");
            }
        }
        public IMongoCollection<SecondPrice> SecondPrices
        {
            get
            {
                return this.db.GetCollection<SecondPrice>("SecondPrice");
            }
        }

        public void Dispose()
        {
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DailyPrice> DailyPrices { get; set; }
        public DbSet<SecondPrice> SecondPrices { get; set; }
    }
}