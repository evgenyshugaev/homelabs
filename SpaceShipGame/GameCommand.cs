using CommandQueue;
using Interfaces;
using SimpleIoc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SpaceShipGame
{
    public class GameCommand : ICommand
    {
        public string Id { get; set; }

        public bool IsStarted { get; private set; }

        public List<IUObject> GameObjects { get; private set; }

        public List<IUObject> Users { get; private set; }

        public IQueue Commands { get; private set; }

        public ConcurrentQueue<MessageDto> Messages { get; private set; }
        
        public GameCommand(string id, List<IUObject> users)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("Id не задан");
            }
            
            Id = id;
            Commands = new CommandQueue.CommandQueue();
            Messages = new ConcurrentQueue<MessageDto>();
            GameObjects = new List<IUObject>();
            Users = users;

            // При старте игры создаем несколько игровых объектов
            var spaceship_1 = new UObject();
            spaceship_1.SetProperty("userName", "Evgeny");
            spaceship_1.SetProperty("id", "x-ray");
            spaceship_1.SetProperty("fuel", (decimal)5);

            GameObjects.Add(spaceship_1);

            var spaceship_2 = new UObject();
            spaceship_2.SetProperty("userName", "Oleg");
            spaceship_2.SetProperty("id", "destroyer");
            spaceship_2.SetProperty("fuel", (decimal)10);
            GameObjects.Add(spaceship_2);

            var spaceship_3 = new UObject();
            spaceship_3.SetProperty("userName", "Vladimir");
            spaceship_3.SetProperty("id", "death star");
            spaceship_3.SetProperty("fuel", (decimal)150);
            GameObjects.Add(spaceship_3);
        }

        public void Execute()
        {
            if (IsStarted)
            {
                return;
            }

            CommandQueueHandler commandQueueHandler = Ioc.Resolve<CommandQueueHandler>("CommandQueueHandler", Commands);
            StartQueueCommand startQueueCommand = Ioc.Resolve<StartQueueCommand>("StartQueueCommand", commandQueueHandler);
            startQueueCommand.Execute();

            InterpretCommand inetrpretCommand = Ioc.Resolve<InterpretCommand>("InetrpretCommand", this);
            Commands.Put(inetrpretCommand);

            IsStarted = true;
        }
    }
}
