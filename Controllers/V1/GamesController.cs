using ApiGameCatalog.Exceptions;
using ApiGameCatalog.InputModel;
using ApiGameCatalog.Services;
using ApiGameCatalog.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGameCatalog.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int amount = 5)
        {
            var games = await _gameService.Get(page, amount);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }

        [HttpGet("{gameID:guid}")]
        public async Task<ActionResult<GameViewModel>> Get([FromRoute] Guid gameID)
        {
            var game = await _gameService.Get(gameID);
            if (game == null)
                return NoContent();
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.Insert(gameInputModel);

                return Ok(game);
            }
            catch(GameRegisteredException ex)
            {
                return UnprocessableEntity("There is an existing game with this title made by this publisher");
            }
        }

        [HttpPut("{gameID:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameID, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(gameID, gameInputModel);
                return Ok();
            }
            catch(GameNotRegisteredException ex)
            {
                return NotFound("This game does not exist");
            }
        }

        [HttpPatch("{gameID:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameID, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(gameID, price);
                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("This game does not exist");
            }
        }

        [HttpDelete("{gameID:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid gameID)
        {
            try
            {
                await _gameService.Remove(gameID);
                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("This game does not exist");
            }
        }
    }
}
