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
    public class EditionRepository : IRepository<Edition>
    {
        private readonly MsuLibraryContext _context;
        private bool disposed = false;

        public EditionRepository(MsuLibraryContext context)
        {
            _context = context;
        }

        public void Create(Edition entity)
        {
            _context.Editions.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Edition entity)
        {
            await _context.Editions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(string id)
        {
            var edition = Get(id);
            _context.Editions.Remove(edition);
            _context.SaveChanges();
        }

        public IEnumerable<Edition> Get()
        {
            return _context.Editions
                .Include(i => i.PublishingHouse)
                .Include(i => i.Author)
                .Include(i => i.Genre)
                .Include(i => i.Type)
                .ToList();
        }


        public IEnumerable<Edition> Get(Expression<Func<Edition, bool>> expression)
        {
            return _context.Editions
                .Include(i => i.PublishingHouse)
                .Include(i => i.Author)
                .Include(i => i.Genre)
                .Include(i => i.Type)
                .Where(expression).ToList();
        }

        public async Task<IEnumerable<Edition>> GetAsync()
        {
            return await _context.Editions
                .Include(i => i.PublishingHouse)
                .Include(i => i.Author)
                .Include(i => i.Genre)
                .Include(i => i.Type)
                .ToListAsync();
        }

        public async Task<IEnumerable<Edition>> GetAsync(Expression<Func<Edition, bool>> expression)
        {
            return await _context.Editions
                .Include(i => i.PublishingHouse)
                .Include(i => i.Author)
                .Include(i => i.Genre)
                .Include(i => i.Type)
                .Where(expression).ToListAsync();
        }

        public Edition Get(string id)
        {
            return _context.Editions
                .Include(i => i.PublishingHouse)
                .Include(i => i.Author)
                .Include(i => i.Genre)
                .Include(i => i.Type)
                .FirstOrDefault(i => i.Id == id);
        }

        public async Task<Edition> GetAsync(string id)
        {
            return await _context.Editions
                .Include(i => i.PublishingHouse)
                .Include(i => i.Author)
                .Include(i => i.Genre)
                .Include(i => i.Type)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(Edition entity)
        {
            _context.Editions.Update(entity);
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
