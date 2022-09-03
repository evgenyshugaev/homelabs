using SimpleIoc;
using SpaceShipGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShipGameServer
{
    public static class StartupServer
    {
        public static Dictionary<string, GameCommand> Games;

        static StartupServer()
        {
            Games = new Dictionary<string, GameCommand>();
        }

        public static GameCommand StartNewGame(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("id не определен");
            }

            if (Games.ContainsKey(id))
            {
                throw new Exception("Игра с таким id уже существует.");
            }

            var newGame = Ioc.Resolve<GameCommand>("GameCommand", id);
            newGame.Execute();
            Games.Add(id, newGame);

            return newGame;
        }
    }
}
