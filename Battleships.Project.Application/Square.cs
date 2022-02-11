using System.ComponentModel;

namespace Battleships.Project.Application
{
    /*    enum type to determine if the square is occupied, next to occupied, empty,
          or after an attack if it is a hit or a miss*/

    public enum SquareState
    {
        [Description("B")] Battleship,
        [Description("C")] Carrier,
        [Description("D")] Destroyer,
        [Description("S")] Submarine,
        [Description("P")] Patrolship,
        [Description("~")] Empty,
        [Description("X")] Hit,
        [Description("o")] Miss
    }

    public enum ShotResult
    {
        Miss,
        Hit
    }

    public class Square
    {
        public SquareState State { get; set; }

        public GridCellRef GridCellRef { get; set; }

        public bool IsOccupied
        {
            get
            {
                return State == SquareState.Battleship
                    || State == SquareState.Destroyer
                    || State == SquareState.Carrier
                    || State == SquareState.Submarine
                    || State == SquareState.Patrolship;
            }
        }

        public string Status => State.GetAttributeOfType<DescriptionAttribute>().Description;

        public Square(int column, int row)
        {
            State = SquareState.Empty;
            GridCellRef = new GridCellRef(column, row);
        }

        public bool IsRandomAvailable
        {
            get
            {
                return (GridCellRef.Row % 2 == 0 && GridCellRef.Col % 2 == 0)
                    || (GridCellRef.Row % 2 == 1 && GridCellRef.Col % 2 == 1);
            }
        }
    }
}