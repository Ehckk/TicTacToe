using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe {
	class PlayerOld {
		public PlayerOld(string turn) {
			Turn = turn;
		}

		public static string Turn { get; set; }

		public static void TakeTurn() {
			Game.PrintBoard();
			Console.WriteLine("It is your turn");
			Game.squares[Move()-1] = Turn; // did you even write the thing in the squares vruh
			// Check if someone has won in AI script
			AI.TakeTurn();
		}

		private static int Move() {
			int x;
			bool validMove = false;
			do {
				Console.WriteLine("Input move: ");
				bool check = int.TryParse(Console.ReadLine(), out x);
				if (!check) { // Did they input an integer? If not, yell at them
					Console.Write("Invalid move: input should be a whole number!\n");
				} else { // If they did, see if the box exists and is empty
					validMove = IsMoveValid(x);
				}
			} while (!validMove);
			if (!Game.squares.Contains(Turn)) { // no player letters on board, Player's first turn
				AI.playerFirstMove = x;
			}
			return x;
		} 

		private static bool IsMoveValid(int move) {
			bool inRange = move > 0 && move < 10;
			if (!inRange) { // puts in 10 on a board with 9 squares
				Console.WriteLine($"Invalid move: {move} is not a whole number from 1 to 9!");
				return false;
			} else if (Game.squares[move - 1] != " ") { // input is valid, but box is not empty
				Console.WriteLine($"Invalid move: The number you enter should correspond to an empty box, and the {move} box is not empty!");
				return false;
			} else return true;
		}
	}
}
