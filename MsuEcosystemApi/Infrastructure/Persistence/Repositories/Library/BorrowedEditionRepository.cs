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
    public class BorrowedEditionRepository : IRepository<BorrowedEdition>
    {
        private readonly MsuLibraryContext _context;
        private bool disposed = false;

        public BorrowedEditionRepository(MsuLibraryContext context)
        {
            _context = context;
        }

        public void Create(BorrowedEdition entity)
        {
            _context.BorrowedEditions.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(BorrowedEdition entity)
        {
            await _context.BorrowedEditions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(string id)
        {
            var edition = Get(id);
            _context.BorrowedEditions.Remove(edition);
            _context.SaveChanges();
        }

        public IEnumerable<BorrowedEdition> Get()
        {
            return _context.BorrowedEditions.Include(i => i.Request);
        }


        public IEnumerable<BorrowedEdition> Get(Expression<Func<BorrowedEdition, bool>> expression)
        {
            return _context.BorrowedEditions.Include(i => i.Request).Where(expression);
        }

        public async Task<IEnumerable<BorrowedEdition>> GetAsync()
        {
            return await _context.BorrowedEditions.Include(i => i.Request).ToListAsync();
        }

        public async Task<IEnumerable<BorrowedEdition>> GetAsync(Expression<Func<BorrowedEdition, bool>> expression)
        {
            return await _context.BorrowedEditions.Include(i => i.Request).Where(expression).ToListAsync();
        }

        public BorrowedEdition Get(string id)
        {
            return _context.BorrowedEditions.Include(i => i.Request).FirstOrDefault(i => i.Id == id);
        }

        public async Task<BorrowedEdition> GetAsync(string id)
        {
            return await _context.BorrowedEditions.Include(i => i.Request).FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(BorrowedEdition entity)
        {
            _context.BorrowedEditions.Update(entity);
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
