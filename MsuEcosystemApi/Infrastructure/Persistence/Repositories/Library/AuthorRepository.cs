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
    public class AuthorRepository : IRepository<Author>
    {
        private readonly MsuLibraryContext _context;
        private bool disposed = false;

        public AuthorRepository(MsuLibraryContext context)
        {
            _context = context;
        }

        public void Create(Author entity)
        {
            _context.Authors.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Author entity)
        {
            await _context.Authors.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(string id)
        {
            var author = Get(id);
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }

        public IEnumerable<Author> Get()
        {
            return _context.Authors;
        }


        public IEnumerable<Author> Get(Expression<Func<Author, bool>> expression)
        {
            return _context.Authors.Where(expression);
        }

        public async Task<IEnumerable<Author>> GetAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAsync(Expression<Func<Author, bool>> expression)
        {
            return await _context.Authors.Where(expression).ToListAsync();
        }

        public Author Get(string id)
        {
            return _context.Authors.FirstOrDefault(i => i.Id == id);
        }

        public async Task<Author> GetAsync(string id)
        {
            return await _context.Authors.FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(Author entity)
        {
            _context.Authors.Update(entity);
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
