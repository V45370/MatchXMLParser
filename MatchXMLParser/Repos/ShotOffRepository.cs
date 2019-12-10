using MatchXMLParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Repos
{
    class ShotOffRepostirotry : IRepository<ShotOff>
    {
        MainDbContext _context;

        public ShotOffRepostirotry()
        {
            _context = new MainDbContext();
        }
        public IEnumerable<ShotOff> List
        {
            get
            {
                return _context.ShotsOff;
            }

        }

        public void Add(ShotOff entity)
        {
            _context.ShotsOff.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(ShotOff entity)
        {
            _context.ShotsOff.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(ShotOff entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public ShotOff FindByExternalId(int externalId)
        {
            var result = (from r in _context.ShotsOff where r.ExternalId == externalId select r).FirstOrDefault();
            return result;
        }
    }
}
