using ApiGameCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGameCatalog.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Get(int page, int amount);
        Task<Game> Get(Guid Id);
        Task<List<Game>> Get(string title, string publisher);
        Task Insert(Game game);
        Task Update(Game game);
        Task Remove(Guid Id);
    }
}
