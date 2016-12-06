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
    public interface IDataBase : IDisposable
    {
        IRepository<T> DataSet<T>() where T : BaseEntity;
        IRepository<BaseEntity> Data(string Name);
    }
    public interface IRepository<T> where T : BaseEntity
    {
        void Update(T obj);
        void Insert(T obj);

        IQueryable<T> Query();
    }
}