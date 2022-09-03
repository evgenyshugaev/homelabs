using Interfaces;
using SimpleIoc;

namespace SpaceShipGame
{
    /// <summary>
    /// Забирает новые сообщения из очереди и складывает в очередь команд.
    /// После выполнения, заново ставит саму себя в очередь.
    /// </summary>
    public class InterpretCommand : ICommand
    {
        private GameCommand Game;

        public InterpretCommand(GameCommand game)
        {
            Game = game;
        }

        public void Execute()
        {
            if (Game.Messages.TryDequeue(out MessageDto message))
            {
                foreach (var gameObject in Game.GameObjects)
                {
                    if ((string)gameObject.GetProperty("id") == message.ObjectId)
                    {
                        var command = Ioc.Resolve<ICommand>(message.Command, gameObject, message.Parameters);
                        Game.Commands.Put(command);
                       

                        break;
                    }
                }
            }

            Game.Commands.Put(this);
        }
    }
}
