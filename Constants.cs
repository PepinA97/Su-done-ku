namespace SudokuSolver
{
    class Constants
    {
        public static readonly bool NotPossible = true;
        public static readonly bool Possible = false;

        public class Dimensions
        {
            public static readonly int SubgridX = 0;
            public static readonly int SubgridY = 1;
            public static readonly int InsideSubgridX = 2;
            public static readonly int InsideSubgridY = 3;
        }

        public class DefaultSize
        {
            public static readonly int NumSubgridRows = 3;
            public static readonly int NumSubgridColumns = 3;

            public static readonly int NumInsideSubgridRows = 3;
            public static readonly int NumInsideSubgridColumns = 3;
        }
    }
}