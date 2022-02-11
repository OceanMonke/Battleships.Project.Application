namespace Battleships.Project.Application
{
    public class GridCellRef
    {
        /*Properties for columns and rows of the grid*/
        public int Col { get; set; }
        public int Row { get; set; }

        public GridCellRef(int column, int row)
        {
            Col = column;
            Row = row;
        }
    }
}