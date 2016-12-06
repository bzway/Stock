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

    public class MongoDBContext : Shared.DataBase.MongoDataBase
    {

    }
}