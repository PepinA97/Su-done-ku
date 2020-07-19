using SudokuSolver.Models;

namespace SudokuSolver
{
    static class BoardFactory
    {
        public static Cell[,,,] CreateBoard(int numSubgridColumns, int numSubgridRows, int numInsideSubgridColumns, int numInsideSubgridRows)
        {
            Cell[,,,] board = new Cell[numSubgridColumns, numSubgridRows,
                                       numInsideSubgridColumns, numInsideSubgridRows];

            for (int subgridX = 0; subgridX < numSubgridColumns; subgridX++)
            {
                for (int subgridY = 0; subgridY < numSubgridRows; subgridY++)
                {
                    for (int X = 0; X < numInsideSubgridColumns; X++)
                    {
                        for (int Y = 0; Y < numInsideSubgridRows; Y++)
                        {
                            board[subgridX, subgridY, X, Y] = new Cell();
                        }
                    }
                }
            }

            return board;
        }
    }
}
