using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shared.Entity;
using System;
using System.Linq;

namespace WindowsApp.DataBase
{

    public class EFDbContext : IDataBase
    {
        readonly DbContext db;
        public EFDbContext(DbContext db)
        {
            this.db = db;
        }
        public IRepository<BaseEntity> Data(string Name)
        {
            return new SQLRepository<BaseEntity>(this.db.Set<BaseEntity>());
        }

        public IRepository<T> DataSet<T>() where T : BaseEntity, new()
        {
            return new SQLRepository<T>(this.db.Set<T>());
        }

        public void Dispose()
        {
            this.db.Dispose();
        }

        public class SQLRepository<T> : IRepository<T> where T : BaseEntity, new()
        {
            readonly DbSet<T> ds;
            public SQLRepository(DbSet<T> ds)
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
    }
}