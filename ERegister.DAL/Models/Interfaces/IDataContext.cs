using System;
using System.Data.Entity;

namespace ERegister.DAL.Models.Interfaces
{
    public interface IDataContext:IDisposable
    {
        int SaveChanges();
        DbSet<T> Set<T>() where T : class;
    }
}
