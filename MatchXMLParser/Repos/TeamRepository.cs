using MatchXMLParser.Models;
using MatchXMLParser.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchXMLParser.Repos
{
    public class TeamRepository : IRepository<Team>
    {

        MainDbContext _context;

        public TeamRepository()
        {
            _context = new MainDbContext();

        }
        public IEnumerable<Team> List
        {
            get
            {
                return _context.Teams;
            }

        }

        public void Add(Team entity)
        {
            _context.Teams.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Team entity)
        {
            _context.Teams.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Team entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

        }

        public Team FindById(int Id)
        {
            var result = (from r in _context.Teams where r.Id == Id select r).FirstOrDefault();
            return result;
        }
        public Team FindByExternalId(int externalId)
        {
            var result = (from r in _context.Teams where r.ExternalId == externalId select r).FirstOrDefault();
            return result;
        }
    }
}
