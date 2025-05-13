using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TicTacToeRendererLib.Enums;
using TicTacToeRendererLib.Renderer;

namespace TicTacToeSubmissionConole
{
   public class TicTacToe
    {
        private TicTacToeConsoleRenderer _boardRenderer;
        private PlayerEnum[,] playerMoves = new PlayerEnum[3, 3];
        bool[,] isOccupied = new bool[3, 3];
        private PlayerEnum currentPlayer = PlayerEnum.X;
        int xMoves = 0;
        int oMoves = 0;

        public TicTacToe()
        {
            _boardRenderer = new TicTacToeConsoleRenderer(10, 6);
            _boardRenderer.Render();
        }

      public void Run()
        {
        int totalMoves = 0;
        

        while (true)
        {
            Console.SetCursorPosition(2, 19);
            Console.Write($"Player {currentPlayer}     ");

            Console.SetCursorPosition(2, 20);
            Console.Write("Please Enter Row (0-2): ");
            int row = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(2, 22);
            Console.Write("Please Enter Column (0-2): ");
            int column = int.Parse(Console.ReadLine());

            
            if (!isOccupied[row, column])
            {
                playerMoves[row, column] = currentPlayer;
                isOccupied[row, column] = true;
                _boardRenderer.AddMove(row, column, currentPlayer, true);
                totalMoves++;

                if (currentPlayer == PlayerEnum.X)
                    xMoves++;
                else
                    oMoves++;

                
                if ((currentPlayer == PlayerEnum.X && xMoves >= 3) ||
                    (currentPlayer == PlayerEnum.O && oMoves >= 3))
                {
                    if (CheckWin(currentPlayer))
                    {
                        Console.SetCursorPosition(2, 26);
                        Console.WriteLine($"Player {currentPlayer} Wins!");
                        break;
                    }
                }

            
                if(currentPlayer==PlayerEnum.X)
                {
                currentPlayer = PlayerEnum.O;
                }
                else
                {
                    currentPlayer = PlayerEnum.X;
                }
            

                if (totalMoves == 9)
                {
                    Console.SetCursorPosition(2, 26);
                    Console.WriteLine("It's a draw!");
                    break;
                }
            }
        }
     }
    private bool CheckWin(PlayerEnum player)
    {
        // Check rows
        for (int i = 0; i < 3; i++)
        {
            if (isOccupied[i, 0] && isOccupied[i, 1] && isOccupied[i, 2] &&
                playerMoves[i, 0] == player &&
                playerMoves[i, 1] == player &&
                playerMoves[i, 2] == player)
                return true;
        }

        // Check columns
        for (int i = 0; i < 3; i++)
        {
            if (isOccupied[0, i] && isOccupied[1, i] && isOccupied[2, i] &&
                playerMoves[0, i] == player &&
                playerMoves[1, i] == player &&
                playerMoves[2, i] == player)
                return true;
        }

        // Check diagonals
        if (isOccupied[0, 0] && isOccupied[1, 1] && isOccupied[2, 2] &&
            playerMoves[0, 0] == player &&
            playerMoves[1, 1] == player &&
            playerMoves[2, 2] == player)
            return true;

        if (isOccupied[0, 2] && isOccupied[1, 1] && isOccupied[2, 0] &&
            playerMoves[0, 2] == player &&
            playerMoves[1, 1] == player &&
            playerMoves[2, 0] == player)
            return true;

        return false;
        }
    }
}