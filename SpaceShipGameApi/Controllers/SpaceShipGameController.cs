using Interfaces;
using Microsoft.AspNetCore.Mvc;
using SpaceShipGame;
using SpaceShipGameServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShipGameApi.Controllers
{
    [Route("api/spaceshipgame")]
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

        [HttpPost]
        [Route("receivemessagewithtoken")]
        public object ReceiveMessageWithToken([FromBody] MessageDto messageDto)
        {
            if (messageDto.Token == null)
            {
                throw new Exception("Запрос не авторизован. Jwt-токен не найен.");
            }

            if(!TokenValidator.ValidateToken(messageDto.Token))
            {
                throw new Exception("jwt-токен не валиден.");
            }

            var tokenClaims = TokenValidator.GetClaims(messageDto.Token);

            if (!StartupServer.Games.ContainsKey(messageDto.GameId))
            {
                throw new Exception("Игра с таким id не найдена");
            }

            if (tokenClaims.Item1 != messageDto.GameId)
            {
                throw new Exception("Запрошенная игра не совпадает с номером игры автоизованного пользователя");
            }

            var game = StartupServer.Games[messageDto.GameId];

            if (!game.Users.Any(u => (string)u.GetProperty("name") == tokenClaims.Item2))
            {
                throw new Exception($"Пользователь {tokenClaims.Item2} не авторизован в запрошенной игре {messageDto.GameId}");
            }

            game.Messages.Enqueue(messageDto);

            return Ok();
        }

        [HttpPost]
        [Route("newgame")]
        public string StartNewGame([FromBody] List<string> userNameList)
        {
            IocResolveStrategy.RegisterDependensies();
            string newGameId = $"game_{DateTime.UtcNow.Ticks}";

            List<IUObject> users = new List<IUObject>();

            foreach (string name in userNameList.Distinct())
            {
                var user = new UObject();
                user.SetProperty("name", name);
                users.Add(user);
            }
            
            StartupServer.StartNewGame(newGameId, users);
            return newGameId;
        }
    }
}
