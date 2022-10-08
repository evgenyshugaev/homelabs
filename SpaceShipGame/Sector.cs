using Interfaces;
using SimpleIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShipGame
{
    /// <summary>
    /// Окрестность.
    /// </summary>
    public class Sector
    {
        public int Id;
        public Sector Neighbor;
        public List<IUObject> Spaceships;
        public double[] Coordinates;
        public ICommand CollisionsCheckMacroCommand;

        public Sector(int id, Sector neighbor, double[] coordinates)
        {
            Id = id;
            Neighbor = neighbor;
            Coordinates = coordinates;
            Spaceships = new List<IUObject>();
            CollisionsCheckMacroCommand = null;
        }

        /// <summary>
        /// Обновить списки космических кораблей по секторам
        /// </summary>
        /// <param name="spaceship"></param>
        public void UpdateSpaceshipList(IUObject spaceship)
        {
            string id = (string)spaceship.GetProperty("id");
            Vector position = (Vector)spaceship.GetProperty("Positiion");
            bool spaceshipInSector = SpaceShipInSector(position);

            if (spaceshipInSector)
            {
                // Если космический корабль попал в новую окрестность
                if (!Spaceships.Any(s => (string)s.GetProperty("id") == id))
                {
                    Spaceships.Add(spaceship);
                    CollisionsCheckMacroCommand = PrepareCollisionsCheckMacroCommand(spaceship);
                }
            }
            else
            {
                if (Spaceships.Any(s => (string)s.GetProperty("id") == id))
                {
                    Spaceships.Remove(spaceship);
                }

                // Проверим в соседней окресности.
                if (Neighbor != null)
                {
                    Neighbor.UpdateSpaceshipList(spaceship);
                }
            }
            
        }

        public bool SpaceShipInSector(Vector position)
        {
            return Coordinates.Contains(position.x);
        }

        public ICommand PrepareCollisionsCheckMacroCommand(IUObject spaceship)
        {
            if (Spaceships.Count == 0)
            {
                return new MacroCommand(new List<ICommand>());
            }

            List<ICommand> commands = new List<ICommand>();

            foreach (IUObject sectorSpaceship in Spaceships)
            {
                ObjectCollisionCheckCommand objectCollisionCheckCommand = Ioc.Resolve<ObjectCollisionCheckCommand>("ObjectCollisionCheckCommand", sectorSpaceship, spaceship);
                commands.Add(objectCollisionCheckCommand);
            }

            return new MacroCommand(commands);
        }
    }
}
