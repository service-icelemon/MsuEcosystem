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
    public class PublicationRepository : IRepository<Publication>
    {
        private readonly MsuNewsContext _context;
        private bool disposed = false;

        public PublicationRepository(MsuNewsContext context)
        {
            _context = context;
        }

        public void Create(Publication entity)
        {
            _context.Publications.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Publication entity)
        {
            await _context.Publications.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(string id)
        {
            var publication = Get(id);
            _context.Publications.Remove(publication);
            _context.SaveChanges();
        }

        public IEnumerable<Publication> Get()
        {
            return _context.Publications.Include(i => i.EditedArticle);
        }

        public IEnumerable<Publication> Get(Expression<Func<Publication, bool>> expression)
        {
            return _context.Publications.Where(expression);
        }

        public async Task<IEnumerable<Publication>> GetAsync()
        {
            return await _context.Publications.Include(i => i.EditedArticle).ToListAsync();
        }

        public async Task<IEnumerable<Publication>> GetAsync(Expression<Func<Publication, bool>> expression)
        {
            return await _context.Publications.Include(i => i.EditedArticle).Where(expression).ToListAsync();
        }

        public Publication Get(string id)
        {
            return _context.Publications.Include(i => i.EditedArticle).FirstOrDefault(i => i.Id == id);
        }

        public async Task<Publication> GetAsync(string id)
        {
            return await _context.Publications.Include(i => i.EditedArticle).FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(Publication entity)
        {
            _context.Publications.Update(entity);
            _context.Entry(EntityState.Modified);
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
