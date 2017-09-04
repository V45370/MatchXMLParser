using MatchXMLParser.Models;
using MatchXMLParser.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchXMLParser.Repos
{
    public class GoalRepository : IRepository<Goal>
    {

        MainDbContext _context;

        public GoalRepository()
        {
            _context = new MainDbContext();

        }
        public IEnumerable<Goal> List
        {
            get
            {
                return _context.Goals;
            }

        }

        public void Add(Goal entity)
        {
            _context.Goals.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Goal entity)
        {
            _context.Goals.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Goal entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

        }

        public Goal FindById(int Id)
        {
            var result = (from r in _context.Goals where r.Id == Id select r).FirstOrDefault();
            return result;
        }
        public Goal FindByExternalId(string externalId)
        {
            var result = (from r in _context.Goals where r.ExternalId == externalId select r).FirstOrDefault();
            return result;
        }
    }
}
