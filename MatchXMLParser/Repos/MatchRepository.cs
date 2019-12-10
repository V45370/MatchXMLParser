using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchXMLParser.Repos
{
    public class MatchRepository : IRepository<Match>
    {

        MainDbContext _context;

        public MatchRepository()
        {
            _context = new MainDbContext();

        }
        public IEnumerable<Match> List
        {
            get
            {
                return _context.Matches;
            }

        }

        public void Add(Match entity)
        {
            _context.Matches.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Match entity)
        {
            _context.Matches.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Match entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

        }

        public Match FindById(int Id)
        {
            var result = (from r in _context.Matches where r.Id == Id select r).FirstOrDefault();
            return result;
        }
        public Match FindByExternalId(int externalId)
        {
            var result = (from r in _context.Matches where r.ExternalId == externalId select r).FirstOrDefault();
            return result;
        }

        public Match FindByDateAndTeams(DateTime date, string homeTeam, string awayTeam)
        {
            var result = _context.Matches.Where(m => m.Date == date && 
                                                        m.HomeTeam.FullName == homeTeam &&
                                                        m.AwayTeam.FullName == awayTeam).FirstOrDefault();
            return result;
        }

    }
}
