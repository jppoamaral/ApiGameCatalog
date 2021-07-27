using ApiGameCatalog.Entities;
using ApiGameCatalog.Exceptions;
using ApiGameCatalog.InputModel;
using ApiGameCatalog.Repositories;
using ApiGameCatalog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGameCatalog.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Get(int page, int amount)
        {
            var games = await _gameRepository.Get(page, amount);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Publisher = game.Publisher,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> Get(Guid id)
        {
            var game = await _gameRepository.Get(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Publisher = game.Publisher,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var gameEntity = await _gameRepository.Get(game.Title, game.Publisher);

            if (gameEntity.Count > 0)
                throw new GameRegisteredException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Title = game.Title,
                Publisher = game.Publisher,
                Price = game.Price
            };

            await _gameRepository.Insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Title = game.Title,
                Publisher = game.Publisher,
                Price = game.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var gameEntity = await _gameRepository.Get(id);

            if (gameEntity == null)
                throw new GameNotRegisteredException();

            gameEntity.Title = game.Title;
            gameEntity.Publisher = game.Publisher;
            gameEntity.Price = game.Price;

            await _gameRepository.Update(gameEntity);
        }

        public async Task Update(Guid id, double price)
        {
            var gameEntity = await _gameRepository.Get(id);

            if (gameEntity == null)
                throw new GameNotRegisteredException();

            gameEntity.Price = price;

            await _gameRepository.Update(gameEntity);
        }

        public async Task Remove(Guid id)
        {
            var game = await _gameRepository.Get(id);

            if (game == null)
                throw new GameNotRegisteredException();

            await _gameRepository.Remove(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
