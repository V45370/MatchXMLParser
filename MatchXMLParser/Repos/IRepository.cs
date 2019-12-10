using System.Collections.Generic;
using System;
using MatchXMLParser.Models;

namespace MatchXMLParser.Repos
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> List { get; }
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindByExternalId(int externalId);
    }
}
