using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shared.Entity;
using MongoDB.Driver;
using System;
using System.Linq;

namespace WindowsApp.DataBase
{

    public class MongoDataBase : IDataBase
    {
        private readonly IMongoDatabase db;
        public MongoDataBase()
        {
            MongoClient client = new MongoClient("mongodb://192.168.1.109");
            db = client.GetDatabase("test");
        }

        public IRepository<BaseEntity> Data(string Name)
        {
            return new MongoRepository<BaseEntity>(this.db, Name);
        }

        public IRepository<T> DataSet<T>() where T : BaseEntity, new()
        {
            return new MongoRepository<T>(this.db, typeof(T).Name);
        }

        public void Dispose()
        {

        }

        public class MongoRepository<T> : IRepository<T> where T : BaseEntity, new()
        {
            private readonly IMongoDatabase db;

            private readonly string name;

            public MongoRepository(IMongoDatabase db, string name)
            {
                this.db = db;
                this.name = name;
            }
            public void Insert(T obj)
            {
                this.db.GetCollection<T>(this.name).InsertOne(obj);
            }

            public IQueryable<T> Query()
            {
                return this.db.GetCollection<T>(this.name).AsQueryable();
            }

            public void Update(T obj)
            {

                this.db.GetCollection<T>(this.name).UpdateOne(m => m.Id == obj.Id, null);
            }
        }
    }
}