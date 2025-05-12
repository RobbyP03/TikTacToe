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
        private PlayerEnum[,] playerMoves = new PlayerEnum[3, 3]; // Stores moves
        private PlayerEnum currentPlayer = PlayerEnum.X; // Alternates turns

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
                    _boardRenderer.AddMove(row, column, currentPlayer, true); // Draw move
                    totalMoves++;
                }
                else
                {
                    Console.SetCursorPosition(2, 24);
                    Console.Write("Invalid move! Try again.");
                    Console.ReadKey();
                    continue;
                }

                // Check win condition
                if (totalMoves >= 5 && CheckWin(currentPlayer))
                {
                    Console.Clear();
                    _boardRenderer.Render();
                    Console.SetCursorPosition(2, 26);
                    Console.WriteLine($"Player {currentPlayer} Wins!");
                    break;
                }

                // Check draw condition
                if (totalMoves == 9)
                {
                    Console.Clear();
                    _boardRenderer.Render();
                    Console.SetCursorPosition(2, 26);
                    Console.WriteLine("It's a draw!");
                    break;
                }

                // Alternate player turn
                currentPlayer = currentPlayer == PlayerEnum.X ? PlayerEnum.O : PlayerEnum.X;
            }
        }

        private bool CheckWin(PlayerEnum player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (playerMoves[i, 0] == player && playerMoves[i, 1] == player && playerMoves[i, 2] == player) return true; // Row
                if (playerMoves[0, i] == player && playerMoves[1, i] == player && playerMoves[2, i] == player) return true; // Column
            }
            if (playerMoves[0, 0] == player && playerMoves[1, 1] == player && playerMoves[2, 2] == player) return true; // Diagonal
            if (playerMoves[0, 2] == player && playerMoves[1, 1] == player && playerMoves[2, 0] == player) return true; // Reverse diagonal

            return false;
        }
    }
}
