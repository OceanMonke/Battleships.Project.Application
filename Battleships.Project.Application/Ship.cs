namespace Battleships.Project.Application
{
    public class Ship
    {
        /*A set of properties to define the Ship*/

        public string ShipName { get; set; }
        public int ShipSize { get; set; }
        public int Hits { get; set; }

        public bool Sunk
        {
            get
            {
                return Hits >= ShipSize;
            }
        }

        public SquareState State { get; set; }
    }

    /*    Five ship classes which inherit from class Ship */

    public class Carrier : Ship
    {
        public Carrier()
        {
            ShipName = "Carrier";
            ShipSize = 5;
            State = SquareState.Carrier;
        }
    }

    public class Battleship : Ship
    {
        public Battleship()
        {
            ShipName = "Battleship";
            ShipSize = 4;
            State = SquareState.Battleship;
        }
    }

    public class Destroyer : Ship
    {
        public Destroyer()
        {
            ShipName = "Destroyer";
            ShipSize = 3;
            State = SquareState.Destroyer;
        }
    }

    public class Submarine : Ship
    {
        public Submarine()
        {
            ShipName = "Submarine";
            ShipSize = 3;
            State = SquareState.Submarine;
        }
    }

    public class PatrolShip : Ship
    {
        public PatrolShip()
        {
            ShipName = "Patrol Ship";
            ShipSize = 2;
            State = SquareState.Patrolship;
        }
    }
}