using MatchXMLParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Repos
{
    class ShotOnRepostirotry : IRepository<ShotOn>
    {
        MainDbContext _context;

        public ShotOnRepostirotry()
        {
            _context = new MainDbContext();
        }
        public IEnumerable<ShotOn> List
        {
            get
            {
                return _context.ShotsOn;
            }

        }

        public void Add(ShotOn entity)
        {
            _context.ShotsOn.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(ShotOn entity)
        {
            _context.ShotsOn.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(ShotOn entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public ShotOn FindByExternalId(int externalId)
        {
            var result = (from r in _context.ShotsOn where r.ExternalId == externalId select r).FirstOrDefault();
            return result;
        }
    }
}
