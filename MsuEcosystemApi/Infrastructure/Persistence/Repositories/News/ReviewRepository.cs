using Domain.Entitties.News;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.News
{
    class ReviewRepository : IRepository<Review>
    {
        private readonly MsuNewsContext _context;
        private bool disposed = false;

        public ReviewRepository(MsuNewsContext context)
        {
            _context = context;
        }

        public void Create(Review entity)
        {
            _context.Reviews.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Review entity)
        {
            await _context.Reviews.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(string id)
        {
            var review = Get(id);
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }

        public IEnumerable<Review> Get()
        {
            return _context.Reviews.Include(i => i.Draft).ToList();
        }

        public IEnumerable<Review> Get(Expression<Func<Review, bool>> expression)
        {
            return _context.Reviews.Include(i => i.Draft).Where(expression);
        }

        public async Task<IEnumerable<Review>> GetAsync()
        {
            return await _context.Reviews.Include(i => i.Draft).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAsync(Expression<Func<Review, bool>> expression)
        {
            return await _context.Reviews.Include(i => i.Draft).Where(expression).ToListAsync();
        }

        public Review Get(string id)
        {
            return _context.Reviews.FirstOrDefault(i => i.Id == id);
        }


        public async Task<Review> GetAsync(string id)
        {
            return await _context.Reviews.Include(i => i.Draft).FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(Review entity)
        {
            _context.Reviews.Update(entity);
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
