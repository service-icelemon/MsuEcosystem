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
    class EditionTypeRepository : IRepository<EditionType>
    {
        private readonly MsuLibraryContext _context;
        private bool disposed = false;

        public EditionTypeRepository(MsuLibraryContext context)
        {
            _context = context;
        }

        public void Create(EditionType entity)
        {
            _context.EditionTypes.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(EditionType entity)
        {
            await _context.EditionTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(string id)
        {
            var type = Get(id);
            _context.EditionTypes.Remove(type);
            _context.SaveChanges();
        }

        public IEnumerable<EditionType> Get()
        {
            return _context.EditionTypes;
        }


        public IEnumerable<EditionType> Get(Expression<Func<EditionType, bool>> expression)
        {
            return _context.EditionTypes.Where(expression);
        }

        public async Task<IEnumerable<EditionType>> GetAsync()
        {
            return await _context.EditionTypes.ToListAsync();
        }

        public async Task<IEnumerable<EditionType>> GetAsync(Expression<Func<EditionType, bool>> expression)
        {
            return await _context.EditionTypes.Where(expression).ToListAsync();
        }

        public EditionType Get(string id)
        {
            return _context.EditionTypes.FirstOrDefault(i => i.Id == id);
        }

        public async Task<EditionType> GetAsync(string id)
        {
            return await _context.EditionTypes.FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(EditionType entity)
        {
            _context.EditionTypes.Update(entity);
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
