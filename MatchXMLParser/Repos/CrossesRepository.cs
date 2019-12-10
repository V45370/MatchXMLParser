using MatchXMLParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Repos
{
    class CrossesRepository : IRepository<Cross>
    {
        MainDbContext _context;

        public CrossesRepository()
        {
            _context = new MainDbContext();
        }
        public IEnumerable<Cross> List
        {
            get
            {
                return _context.Crosses;
            }

        }

        public void Add(Cross entity)
        {
            _context.Crosses.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Cross entity)
        {
            _context.Crosses.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Cross entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public Cross FindById(int Id)
        {
            var result = (from r in _context.Crosses where r.Id == Id select r).FirstOrDefault();
            return result;
        }
        public Cross FindByExternalId(int externalId)
        {
            var result = (from r in _context.Crosses where r.ExternalId == externalId select r).FirstOrDefault();
            return result;
        }
    }
}
