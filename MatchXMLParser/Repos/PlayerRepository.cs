using MatchXMLParser.Models;
using MatchXMLParser.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchXMLParser.Repos
{
    public class PlayerRepository : IRepository<Player>
    {

        MainDbContext _context;

        public PlayerRepository()
        {
            _context = new MainDbContext();

        }
        public IEnumerable<Player> List
        {
            get
            {
                return _context.Players;
            }

        }

        public void Add(Player entity)
        {
            _context.Players.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Player entity)
        {
            _context.Players.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Player entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

        }

        public Player FindById(int Id)
        {
            var result = (from r in _context.Players where r.Id == Id select r).FirstOrDefault();
            return result;
        }
        public Player FindByExternalId(int externalId)
        {
            var result = (from r in _context.Players where r.ExternalId == externalId select r).FirstOrDefault();
            return result;
        }
    }
}
