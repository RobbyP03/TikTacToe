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
        private PlayerEnum currentPlayer = PlayerEnum.X; 

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
                Console.Write($"Player {currentPlayer}");

                Console.SetCursorPosition(2, 20);
                Console.Write("Please Enter Row (0-2): ");
                var row = int.Parse(Console.ReadLine());

                Console.SetCursorPosition(2, 22);
                Console.Write("Please Enter Column (0-2): ");
                var column = int.Parse(Console.ReadLine());

                
                // Validate move
                if (playerMoves[row, column] != PlayerEnum.X || playerMoves[row, column] != PlayerEnum.O)
                {
                    playerMoves[row, column] = currentPlayer; // Store move
                    _boardRenderer.AddMove(row, column, currentPlayer, true); 
                    totalMoves++;
                }


                if (totalMoves >=6 && CheckWin(currentPlayer))
                {
                    Console.SetCursorPosition(2, 26);
                    Console.WriteLine($"Player {currentPlayer} Wins!");
                    break;
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

        private bool CheckWin(PlayerEnum player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (playerMoves[i, 0] == player && playerMoves[i, 1] == player && playerMoves[i, 2] == player) return true; // Row
                if (playerMoves[0, i] == player && playerMoves[1, i] == player && playerMoves[2, i] == player) return true; // Column
            }
            // Diagonal
            if (playerMoves[0, 0] == player && playerMoves[1, 1] == player && playerMoves[2, 2] == player) return true; 
            if (playerMoves[0, 2] == player && playerMoves[1, 1] == player && playerMoves[2, 0] == player) return true; 

            return false;
        }
    }
}
