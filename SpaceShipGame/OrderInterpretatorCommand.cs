using Interfaces;
using SimpleIoc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShipGame
{
    /// <summary>
    /// Интерпретатор приказов.
    /// </summary>
    public class OrderInterpretatorCommand : ICommand
    {
        IUObject UObject = new UObject();

        public ICommand Command { get; private set; }
        private GameCommand Game;
        
        public OrderInterpretatorCommand(IUObject uObject, GameCommand game)
        {
            UObject = uObject;
            Game = game;
        }

        public void Execute()
        {
            if (!IsUserCanOrder())
            {
                throw new Exception("Пользователь не может выполнить приказ над чужим объектом");
            }
            
            var id = (string)UObject.GetProperty("id");
            IUObject spaceship = Game.GameObjects.First(g => (string)g.GetProperty("id") == id);

            var action = (string)UObject.GetProperty("action");

            Type t = Type.GetType($"SpaceShipGame.{action}");
            if (t == null)
            {
                throw new Exception("Данный тип не найден");
            }

            var constructors = t.GetConstructors();

            List<object> prms = new List<object>();

            if (constructors.Length > 0)
            {
                var constructor = constructors[0];

                foreach (var constructorParam in constructor.GetParameters())
                {
                    // UObject передаем в каждую команду.
                    if (constructorParam.ParameterType.Name.ToLower() == "iuobject" || constructorParam.ParameterType.Name.ToLower() == "uobject")
                    {
                        prms.Add(spaceship);
                        continue;
                    }
                    prms.Add(UObject.GetProperty(constructorParam.Name));
                }
            }

            Command = Ioc.Resolve<ICommand>(action, prms.ToArray());
        }

        private bool IsUserCanOrder()
        {
            var userName = (string)UObject.GetProperty("userName");

            return Game.GameObjects.Any(u => (string)u.GetProperty("userName") == userName);
        }
    }
}
