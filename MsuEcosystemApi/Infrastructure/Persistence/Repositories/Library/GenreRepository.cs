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
    public class GenreRepository : IRepository<Genre>
    {
        private readonly MsuLibraryContext _context;
        private bool disposed = false;

        public GenreRepository(MsuLibraryContext context)
        {
            _context = context;
        }

        public void Create(Genre entity)
        {
            _context.Genres.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Genre entity)
        {
            await _context.Genres.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(string id)
        {
            var genre = Get(id);
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }

        public IEnumerable<Genre> Get()
        {
            return _context.Genres;
        }


        public IEnumerable<Genre> Get(Expression<Func<Genre, bool>> expression)
        {
            return _context.Genres.Where(expression);
        }

        public async Task<IEnumerable<Genre>> GetAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<IEnumerable<Genre>> GetAsync(Expression<Func<Genre, bool>> expression)
        {
            return await _context.Genres.Where(expression).ToListAsync();
        }

        public Genre Get(string id)
        {
            return _context.Genres.FirstOrDefault(i => i.Id == id);
        }

        public async Task<Genre> GetAsync(string id)
        {
            return await _context.Genres.FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(Genre entity)
        {
            _context.Genres.Update(entity);
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
