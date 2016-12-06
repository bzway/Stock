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
    public interface IDataBase : IDisposable
    {
        IRepository<T> DataSet<T>() where T : BaseEntity, new();
        IRepository<BaseEntity> Data(string Name);
    }
    public interface IRepository<T> where T : BaseEntity, new()
    {
        void Update(T obj);
        void Insert(T obj);

        IQueryable<T> Query();
    }
}