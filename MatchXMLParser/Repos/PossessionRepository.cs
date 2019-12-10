using MatchXMLParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Repos
{
    public class PossessionRepository : IRepository<Possession>
    {
        MainDbContext _context;

        public PossessionRepository()
        {
            _context = new MainDbContext();

        }
        public IEnumerable<Possession> List
        {
            get
            {
                return _context.Possessions;
            }

        }

        public void Add(Possession entity)
        {
            _context.Possessions.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Possession entity)
        {
            _context.Possessions.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Possession entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public Possession FindById(int Id)
        {
            var result = (from r in _context.Possessions where r.Id == Id select r).FirstOrDefault();
            return result;
        }
        public Possession FindByExternalId(int externalId)
        {
            var result = (from r in _context.Possessions where r.ExternalId == externalId select r).FirstOrDefault();
            return result;
        }
    }
}
