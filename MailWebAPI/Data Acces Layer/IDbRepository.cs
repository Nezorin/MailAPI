using DataAccesLayer.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public interface IDbRepository
    {
        IQueryable<Mail> Get(Func<Mail, bool> selector);
        IQueryable<Mail> GetAll();
        Task<long> Add(Mail newEntity);
        Task<int> SaveChangesAsync();
    }
}