using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityToken.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        [Route("newgame")]
        public string CreateGame([FromBody] NewGameDto newGameDto)
        {
            return NewGameService.StartNewGame(newGameDto.UserNameList); ;
        }

        [HttpPost]
        [Route("gettoken")]
        public string GetToken([FromBody] GetTokenDto getTokenDto)
        {
            if (!NewGameService.GameUsers.ContainsKey(getTokenDto.GameId))
            {
                throw new Exception($"Игра с id {getTokenDto.GameId} не найдена");
            }

            if (!NewGameService.GameUsers[getTokenDto.GameId].Contains(getTokenDto.UserName))
            {
                throw new Exception($"Пользователь {getTokenDto.UserName} не найден");
            }

            string token = TokenService.GetToken(getTokenDto.UserName, getTokenDto.GameId);
            return token;
        }
    }
}
