using System.Drawing;

namespace SudokuSolver.Models
{
    public class FillIn
    {
        public Point Subgrid;
        public Point InsideSubgrid;

        public int? Number { get; set; }

        public FillIn(int subgridX, int subgridY, int x, int y, int? number)
        {
            Subgrid.X = subgridX;
            Subgrid.Y = subgridY;

            InsideSubgrid.X = x;
            InsideSubgrid.Y = y;

            Number = number;
        }
    }
}