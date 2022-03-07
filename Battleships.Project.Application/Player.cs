using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Project.Application
{
    /// Player class, which consists of a name, list of ships, boards (grids) used to play and a prop "Lost"

    public class Player
    {
        public string Name { get; set; }
        public List<Ship> Ships { get; set; }
        public Grid Grid { get; set; }
        public OperationGrid OperationGrid { get; set; }

        public bool Lost
        {
            get
            {
                return Ships.All(x => x.Sunk);
            }
        }

        public Player(string name)
        {
            Name = name;
            Grid = new Grid();
            OperationGrid = new OperationGrid();

            Ships = new List<Ship>()
            {
                new Submarine(),
                new Carrier(),
                new PatrolShip(),
                new Battleship(),
                new Destroyer(),
            };
        }

        ///Method that shows the Player his Grid and Firing Grid  

        public void ShowGrid()
        {
            Console.WriteLine(Name);
            Console.WriteLine("Own Board:                          Firing Board:");

            for (int row = 1; row <= 10; row++)
            {
                for (int ownColumn = 1; ownColumn <= 10; ownColumn++)
                {
                    Console.Write(Grid.Squares.At(row, ownColumn).Status + " ");
                }
                Console.Write("                ");

                for (int firingColumn = 1; firingColumn <= 10; firingColumn++)
                {
                    Console.Write(OperationGrid.Squares.At(firingColumn, row).Status + " ");
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }

        /*Method that places the ships using rng. I have found numerous ways to deal with that but I think that generating a random number and trying to place the ship is not the most effective way but it's enough)*/

        public void ShipsPlacing()
        {
            /*Random number generator for choosing a random square on the grid as a starting point of placing a ship*/
            Random rand = new();

            /*foreach loop which looks for a random non-occupied square to place a ship*/

            foreach (var ship in Ships)
            {
                bool leftShips = true;
                const int gridBounds = 11;


                while (leftShips)
                {
                    /*Starting position of the ship/the first cell of the ship*/

                    var startx = rand.Next(1, gridBounds);
                    var starty = rand.Next(1, gridBounds);

                    /*The last cell of the ship*/

                    var endx = startx;
                    var endy = starty;

                    var orientation = rand.Next(1, 101) % 2;

                    List<int> squareNumber = new List<int>();

                    /*Orientation condition; condition = 1 means vertical, 0 means horizontal*/
                    if (orientation == 1)
                    {
                        for (int i = 1; i < ship.ShipSize; i++)
                        {
                            endy++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.ShipSize; i++)
                        {
                            endx++;
                        }
                    }

                    /*Conditions for placing a ship; ship cannot be put out of bounds; any of the square on which the ship is being placed cannot be already taken. Setting occupation status for every square taken by currently placed ship. */
                    if (endx > 10 || endy > 10)
                    {
                        leftShips = true;
                        continue;
                    }

                    var chosenSquares = Grid.Squares.Range(startx, starty, endx, endy);

                    /*Ship cannot be placed on an occupied square*/
                    if (chosenSquares.Any(x => x.IsOccupied))
                    {
                        leftShips = true;
                        continue;
                    }

                    /*Placing the ship on the chosen squares*/
                    foreach (var square in chosenSquares)
                    {
                        square.State = ship.State;
                    }

                    leftShips = false;
                }
            }
        }

        /*Method for showing the grid*/

        public void GridOutput()
        {
            Console.WriteLine(Name);
            Console.WriteLine("Your Grid: \t\t\t    Opponent's Grid:");
            for (int row = 1; row <= 10; row++)
            {
                for (int ownColumn = 1; ownColumn <= 10; ownColumn++)
                {
                    Console.Write(Grid.Squares.At(row, ownColumn).Status + " ");
                }
                Console.Write("                ");
                for (int firingColumn = 1; firingColumn <= 10; firingColumn++)
                {
                    Console.Write(OperationGrid.Squares.At(row, firingColumn).Status + " ");
                }
                Console.WriteLine(Environment.NewLine);
            }
        }

        /*Methods for shooting*/

        public GridCellRef Shoot()
        {
            /*A good strategy for playing battleships is shooting squares with neighbours that weren't shot yet*/

            var hitNeighbours = OperationGrid.GetHitNeighbours();
            GridCellRef gridCellRef;
            if (hitNeighbours.Any())
            {
                gridCellRef = ShootNearby();
            }
            else
            {
                gridCellRef = ShootRandom();
            }
            Console.WriteLine(Name + " says: \"Firing shot at "
                      + gridCellRef.Row.ToString()
                      + ", " + gridCellRef.Col.ToString()
                      + "\"");
            return gridCellRef;
        }

        /*This method chooses a random blank sqaure (one which was not yet shot at)*/

        private GridCellRef ShootRandom()
        {
            var blankSquares = OperationGrid.GetOpenRandomSquares();
            Random rand = new Random();
            var squareID = rand.Next(blankSquares.Count);
            return blankSquares[squareID];
        }

        /*After a succesful hit this method is used to check the neighbouring squares; it uses the GetHitNeighbours method.*/

        private GridCellRef ShootNearby()
        {
            Random rand = new Random();
            var hitNeighbours = OperationGrid.GetHitNeighbours();
            var neighbourID = rand.Next(hitNeighbours.Count);
            return hitNeighbours[neighbourID];
        }

        /*This method checks if the square was occupied or not and prints a message. If the shot was a hit and it was the last hitpoint of a ship then it prints "sunk"*/

        public ShotResult ProcessShot(GridCellRef gridCellRef)
        {
            var square = Grid.Squares.At(gridCellRef.Row, gridCellRef.Col);
            if (!square.IsOccupied)
            {
                Console.WriteLine(Name + " says: \"Miss!\"");
                return ShotResult.Miss;
            }

            var ship = Ships.First(x => x.State == square.State);
            ship.Hits++;
            Console.WriteLine(Name + " says: \"Hit!\"");

            if (ship.Sunk)
            {
                Console.WriteLine(Name + " says: \"You sunk a ship!\"");
            }
            return ShotResult.Hit;
        }

        /*This method changes the state of a square after a shot*/

        public void ProcessShotResult(GridCellRef gridCellRef, ShotResult result)
        {
            var square = OperationGrid.Squares.At(gridCellRef.Row, gridCellRef.Col);
            switch (result)
            {
                case ShotResult.Hit:
                    square.State = SquareState.Hit;
                    break;

                default:
                    square.State = SquareState.Miss;
                    break;
            }
        }
    }
}