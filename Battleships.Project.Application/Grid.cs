using System.Collections.Generic;
using System.Linq;

namespace Battleships.Project.Application
{
    public class Grid
    {
        /*The Grid is basically a list which contains squares defined earlier*/

        public List<Square> Squares { get; set; }

        /*To create a 10x10 board a simple nested for loop is used*/

        public Grid()
        {
            Squares = new List<Square>();
            for (int x = 1; x <= 10; x++)
            {
                for (int y = 1; y <= 10; y++)
                {
                    Squares.Add(new Square(x, y));
                }
            }
        }
    }

    /*A grid for attacking is needed and it inherits from the Grid class*/

    public class OperationGrid : Grid
    {
        /*This method select panels which weren't fired at and which coordinates are both odd/even*/

        public List<GridCellRef> GetOpenRandomSquares()
        {
            return Squares.Where(x => x.State == SquareState.Empty && x.IsRandomAvailable).Select(x => x.GridCellRef).ToList();
        }

        /*This method is used after a succesful shot; it's used by the ShootNearby method to check the surroundings. */

        public List<GridCellRef> GetHitNeighbours()
        {
            List<Square> squares = new();
            var hits = Squares.Where(x => x.State == SquareState.Hit);

            foreach (var hit in hits)
            {
                squares.AddRange(GetNeighbours(hit.GridCellRef).ToList());
            }

            return squares.Distinct().Where(x => x.State == SquareState.Empty).Select(x => x.GridCellRef).ToList();
        }

        /*This method is used to find the neighbouring squares after a shot*/

        public List<Square> GetNeighbours(GridCellRef gridCellRef)
        {
            int row = gridCellRef.Row;
            int col = gridCellRef.Col;

            List<Square> squares = new();

            if (col > 1)
            {
                squares.Add(Squares.At(row, col - 1));
            }

            if (row > 1)
            {
                squares.Add(Squares.At(row - 1, col));
            }

            if (row < 10)
            {
                squares.Add(Squares.At(row + 1, col));
            }

            if (col < 10)
            {
                squares.Add(Squares.At(row, col + 1));
            }

            return squares;
        }
    }
}