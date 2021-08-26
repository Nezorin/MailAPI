using DataAccesLayer.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class DbRepository : IDbRepository
    {
        private readonly DataContext _context;

        public DbRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<long> Add(Mail newEntity)
        {
            var entity = await _context.AddAsync(newEntity);
            return entity.Entity.Id;
        }

        public IQueryable<Mail> Get(Func<Mail, bool> selector)
        {
            return _context.Mails.Where(selector).AsQueryable();
        }

        public IQueryable<Mail> GetAll()
        {
            return _context.Mails.AsQueryable();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

