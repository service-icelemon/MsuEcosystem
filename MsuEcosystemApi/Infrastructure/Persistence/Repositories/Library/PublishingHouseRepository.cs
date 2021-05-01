using Domain.Entitties.Library;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Library
{
    public class PublishingHouseRepository : IRepository<PublishingHouse>
    {
        private readonly MsuLibraryContext _context;
        private bool disposed = false;

        public PublishingHouseRepository(MsuLibraryContext context)
        {
            _context = context;
        }

        public void Create(PublishingHouse entity)
        {
            _context.PublishingHouses.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(PublishingHouse entity)
        {
            await _context.PublishingHouses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(string id)
        {
            var publishingHouse = Get(id);
            _context.PublishingHouses.Remove(publishingHouse);
            _context.SaveChanges();
        }

        public IEnumerable<PublishingHouse> Get()
        {
            return _context.PublishingHouses;
        }


        public IEnumerable<PublishingHouse> Get(Expression<Func<PublishingHouse, bool>> expression)
        {
            return _context.PublishingHouses.Where(expression);
        }

        public async Task<IEnumerable<PublishingHouse>> GetAsync()
        {
            return await _context.PublishingHouses.ToListAsync();
        }

        public async Task<IEnumerable<PublishingHouse>> GetAsync(Expression<Func<PublishingHouse, bool>> expression)
        {
            return await _context.PublishingHouses.Where(expression).ToListAsync();
        }

        public PublishingHouse Get(string id)
        {
            return _context.PublishingHouses.FirstOrDefault(i => i.Id == id);
        }

        public async Task<PublishingHouse> GetAsync(string id)
        {
            return await _context.PublishingHouses.FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(PublishingHouse entity)
        {
            _context.PublishingHouses.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
