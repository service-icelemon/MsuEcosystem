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
    public class PickUpPointRepository : IRepository<PickUpPoint>
    {
        private readonly MsuLibraryContext _context;
        private bool disposed = false;

        public PickUpPointRepository(MsuLibraryContext context)
        {
            _context = context;
        }

        public void Create(PickUpPoint pickUpPoint)
        {
            _context.PickUpPoints.Add(pickUpPoint);
            _context.SaveChanges();
        }

        public async Task CreateAsync(PickUpPoint pickUpPoint)
        {
            await _context.PickUpPoints.AddAsync(pickUpPoint);
            await _context.SaveChangesAsync();
        }

        public void Delete(string id)
        {
            var pickUpPoint = Get(id);
            _context.PickUpPoints.Remove(pickUpPoint);
            _context.SaveChanges();
        }

        public IEnumerable<PickUpPoint> Get()
        {
            return _context.PickUpPoints;
        }


        public IEnumerable<PickUpPoint> Get(Expression<Func<PickUpPoint, bool>> expression)
        {
            return _context.PickUpPoints.Where(expression);
        }

        public async Task<IEnumerable<PickUpPoint>> GetAsync()
        {
            return await _context.PickUpPoints.ToListAsync();
        }

        public async Task<IEnumerable<PickUpPoint>> GetAsync(Expression<Func<PickUpPoint, bool>> expression)
        {
            return await _context.PickUpPoints.Where(expression).ToListAsync();
        }

        public PickUpPoint Get(string id)
        {
            return _context.PickUpPoints.FirstOrDefault(i => i.Id == id);
        }

        public async Task<PickUpPoint> GetAsync(string id)
        {
            return await _context.PickUpPoints.FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(PickUpPoint entity)
        {
            _context.PickUpPoints.Update(entity);
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
