using Microsoft.AspNetCore.Mvc;
using SpaceShipGame;
using SpaceShipGameServer;
using System;

namespace SpaceShipGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceShipGameController : ControllerBase
    {
        [HttpPost]
        public object ReceiveMessage([FromBody] MessageDto messageDto)
        {
            if (!StartupServer.Games.ContainsKey(messageDto.GameId))
            {
                throw new Exception("Игра с таким id не найдена");
            }

            var game = StartupServer.Games[messageDto.GameId];

            game.Messages.Enqueue(messageDto);

            return Ok();
        }
    }
}
