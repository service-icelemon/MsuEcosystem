using Domain.Entitties.News;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistence.Repositories.News
{
    public class DraftRepository : IRepository<Draft>
    {
        private readonly MsuNewsContext _context;
        private bool disposed = false;

        public DraftRepository(MsuNewsContext context)
        {
            _context = context;
        }

        public void Create(Draft entity)
        {
            _context.Drafts.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Draft entity)
        {
            await _context.Drafts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(string id)
        {
            var draft = Get(id);
            _context.Drafts.Remove(draft);
            _context.SaveChanges();
        }

        public IEnumerable<Draft> Get()
        {
            return _context.Drafts;
        }


        public IEnumerable<Draft> Get(Expression<Func<Draft, bool>> expression)
        {
            return _context.Drafts.Where(expression);
        }

        public async Task<IEnumerable<Draft>> GetAsync()
        {
            return await _context.Drafts.ToListAsync();
        }

        public async Task<IEnumerable<Draft>> GetAsync(Expression<Func<Draft, bool>> expression)
        {
            return await _context.Drafts.Where(expression).ToListAsync();
        }

        public Draft Get(string id)
        {
            return _context.Drafts.FirstOrDefault(i => i.Id == id);
        }

        public async Task<Draft> GetAsync(string id)
        {
            return await _context.Drafts.FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(Draft entity)
        {
            _context.Drafts.Update(entity);
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
