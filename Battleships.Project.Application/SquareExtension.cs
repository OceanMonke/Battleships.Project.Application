using System.Collections.Generic;
using System.Linq;

namespace Battleships.Project.Application
{
    public static class SquareExtension
    {
        public static List<Square> Range(this List<Square> panels, int startRow, int startColumn, int endRow, int endColumn)
        {
            return panels.Where(x => x.GridCellRef.Row >= startRow && x.GridCellRef.Col >= startColumn && x.GridCellRef.Row <= endRow && x.GridCellRef.Col <= endColumn).ToList();
        }

        public static Square At(this List<Square> panels, int row, int column)
        {
            return panels.Where(x => x.GridCellRef.Row == row && x.GridCellRef.Col == column).First();
        }
    }
}