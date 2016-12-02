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
    public interface IDataBase : IDisposable
    {
        IDataEntity<T> DataSet<T>() where T : class;
        IDataEntity Data(string Name);
    }
    public interface IDataEntity<T> where T : class
    {
        void Update(T obj);
        void Insert(T obj);

        IQueryable<T> Query();
    }
    public interface IDataEntity
    {
        void Update(object obj);
        void Insert(object Id);
        IQueryable Query();
    }

    public class EFDbContext : IDataBase
    {
        readonly DbContext db;
        public EFDbContext(DbContext db)
        {
            this.db = db;
        }
        public IDataEntity Data(string Name)
        {
            return new EFDataEntity(this.db.Set(Name.GetType()));
        }

        public IDataEntity<T> DataSet<T>() where T : class
        {
            return new EFDataEntity<T>(this.db.Set<T>());
        }

        public void Dispose()
        {
            this.db.Dispose();
        }

        public class EFDataEntity<T> : EFDataEntity, IDataEntity<T> where T : class
        {
            readonly DbSet<T> ds;
            public EFDataEntity(DbSet<T> ds) : base(ds)
            {
                this.ds = ds;
            }
            public void Insert(T obj)
            {
                this.ds.Add(obj);
            }

            public IQueryable<T> Query()
            {
                return this.ds.AsNoTracking().AsQueryable<T>();
            }

            public void Update(T obj)
            {
                this.ds.Attach(obj);
            }
        }
        public class EFDataEntity : IDataEntity
        {
            readonly DbSet ds;
            public EFDataEntity(DbSet ds)
            {
                this.ds = ds;
            }
            public void Insert(object Id)
            {
                this.ds.Add(Id);
            }

            public IQueryable Query()
            {
                return this.ds.AsQueryable();
            }

            public void Update(object obj)
            {
                this.ds.Attach(obj);
            }
        }

    }
    public class MongoDataBase : IDataBase
    {
        private readonly IMongoDatabase db;
        public MongoDataBase()
        {
            MongoClient client = new MongoClient("mongodb://192.168.1.109");
            db = client.GetDatabase("test");
        }

        public IDataEntity Data(string Name)
        {
            throw new NotImplementedException();
        }

        public IDataEntity<T> DataSet<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }
    }


    public class MyClass
    {

        public MyClass()
        {
            using (IDataBase db = new EFDbContext(new DbContext("DefaultConnection")))
            {
                var list = db.DataSet<DailyPrice>().Query().Where(m => m.Code == "SH600000").ToList();
            }
        }
    }

}