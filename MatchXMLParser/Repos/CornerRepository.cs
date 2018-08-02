using MatchXMLParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Repos
{
    class CornerRepository : IRepository<Corner>
    {
        MainDbContext _context;

        public CornerRepository()
        {
            _context = new MainDbContext();

        }
        public IEnumerable<Corner> List
        {
            get
            {
                return _context.Corners;
            }

        }

        public void Add(Corner entity)
        {
            _context.Corners.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Corner entity)
        {
            _context.Corners.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Corner entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public Corner FindById(int Id)
        {
            var result = (from r in _context.Corners where r.Id == Id select r).FirstOrDefault();
            return result;
        }
        public Corner FindByExternalId(string externalId)
        {
            var result = (from r in _context.Corners where r.ExternalId == externalId select r).FirstOrDefault();
            return result;
        }
    }
}
