using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shared.Entity;
using System;
using System.Linq;

namespace Shared.DataBase
{

    public class SqlDatabase : IDataBase
    {
        readonly DbContext db;
        public SqlDatabase(DbContext db)
        {
            this.db = db;
        }
        public IRepository<BaseEntity> Data(string Name)
        {
            return new SQLRepository<BaseEntity>(this.db.Set(Name.GetType()));
        }

        public IRepository<T> DataSet<T>() where T : BaseEntity
        {
            return new SQLRepository<T>(this.db.Set<T>());
        }

        public void Dispose()
        {
            this.db.Dispose();
        }

        public class SQLRepository<T> : IRepository<T> where T : BaseEntity
        {
            readonly DbSet ds;
            public SQLRepository(DbSet ds)
            {
                this.ds = ds;
            }
            public void Insert(T obj)
            {
                this.ds.Add(obj);
            }

            public IQueryable<T> Query()
            {
                return this.ds.OfType<T>().AsNoTracking().AsQueryable<T>();
            }
            public void Update(T obj)
            {
                this.ds.Attach(obj);
            }
        }
    }
}