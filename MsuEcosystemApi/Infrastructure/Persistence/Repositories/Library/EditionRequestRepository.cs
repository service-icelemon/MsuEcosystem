using Domain.Entitties.Library;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistence.Repositories.Library
{
    public class EditionRequestRepository : IRepository<EditionRequest>
    {
        private readonly MsuLibraryContext _context;
        private bool disposed = false;

        public EditionRequestRepository(MsuLibraryContext context)
        {
            _context = context;
        }

        public void Create(EditionRequest entity)
        {
            _context.EditionRequests.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(EditionRequest entity)
        {
            await _context.EditionRequests.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(string id)
        {
            var author = Get(id);
            _context.EditionRequests.Remove(author);
            _context.SaveChanges();
        }

        public IEnumerable<EditionRequest> Get()
        {
            return _context.EditionRequests.Include(i => i.PickUpPoint);
        }


        public IEnumerable<EditionRequest> Get(Expression<Func<EditionRequest, bool>> expression)
        {
            return _context.EditionRequests.Include(i => i.PickUpPoint).Where(expression);
        }

        public async Task<IEnumerable<EditionRequest>> GetAsync()
        {
            return await _context.EditionRequests.Include(i => i.PickUpPoint).ToListAsync();
        }

        public async Task<IEnumerable<EditionRequest>> GetAsync(Expression<Func<EditionRequest, bool>> expression)
        {
            return await _context.EditionRequests.Include(i => i.PickUpPoint).Where(expression).ToListAsync();
        }

        public EditionRequest Get(string id)
        {
            return _context.EditionRequests.Include(i => i.PickUpPoint).FirstOrDefault(i => i.Id == id);
        }

        public async Task<EditionRequest> GetAsync(string id)
        {
            return await _context.EditionRequests.Include(i => i.PickUpPoint).FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(EditionRequest entity)
        {
            _context.EditionRequests.Update(entity);
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
