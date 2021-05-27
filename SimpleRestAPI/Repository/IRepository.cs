using System;
namespace SimpleRestAPI.Repository
{
    public interface IRepository<T>
    {
        T GetDataSource();
    }
}
