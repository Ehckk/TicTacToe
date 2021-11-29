using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe {
	public class Game {
		public static string[] squares = new string[9];
		public static List<int[]> winBoxes = new();
		public static readonly string[] winStrings = { "123", "456", "789", "147", "258", "369", "159", "357" };
		public static List<int> possibleMoves = new();
		// make an int list of all possible moves
		// have it start with all numbers 1 through 9 and when a side plays that move number is removed from the list
		// initialize it each time the game starts
		public static string playerTurn;
		public static int? scenario = null;

		public static void PrintBoard() {
			string board = "|";
			for (int c = 0; c < 9; c++) {
				if (c % 3 == 0 && c != 0) { // every 3rd box, add a line break
					board += "\n";
					board += "|";

				}
				board += $"{squares[c]}|";
			}
			Console.WriteLine(board);
		} 

		public static void InitializeBoxes(bool isTutorial) { // emptys all boxes, or fills them with numbers for the tutorial
			if (isTutorial) {
				foreach (string win in winStrings) {
					char[] winString = win.ToCharArray();
					int[] winParse = new int[winString.Length];
					for (int i = 0; i < winString.Length; i++) {
						winParse[i] = int.Parse(winString[i].ToString());
					}
					winBoxes.Add(winParse);
				}
				for (int i = 0; i < 9; i++) {
					squares[i] = (i+1).ToString();
				}
				winBoxes.ToArray();
			} else {
				for (int i = 0; i < 9; i++) {
					possibleMoves.Add(i); // { 0, 1, 2.. 7, 8 }
					squares[i] = " ";
				}
			}
		}

		public static void PlayGame() {
			// 0 = COM won
			// 1 = tie
			// 2 = Player won
			InitializeBoxes(false);
			AI.playerFirstMove = 0;
			SetTurn();
		}
		
		private static void SetTurn() {
			if (string.IsNullOrEmpty(AI.Turn)) {
				PlayerGoesFirst();
				//Random Rng = new();
				//int r = Rng.Next(0, 100);
				//if (r < 50) { // move to it's own object method
				//	PlayerGoesFirst();
				//} else {
				//	ComGoesFirst();
				//}
			} else {
				TurnSwap();
			}
		}

		private static void PlayerGoesFirst() {
			AI.Turn = "O";
			PlayerOld.Turn = "X";
			Console.WriteLine("Player goes first");
			AI.isComGoingFirst = false;
			PlayerOld.TakeTurn();
		}

		private static void ComGoesFirst() {
			AI.Turn = "X";
			PlayerOld.Turn = "O";
			Console.WriteLine("COM goes first");
			AI.isComGoingFirst = true;
			AI.TakeTurn();
		}

		private static void TurnSwap() {
			if (AI.Turn == "X") { // AI went first last round
				PlayerGoesFirst();
			} else { // AI went second last round
				ComGoesFirst();
			}
		}
		public static bool EndCheck() {
			if (WinCheck(AI.Turn, PlayerOld.Turn)) { // check if computer has matched
				scenario = 0; // computer wins
				return true;
			} else if (WinCheck(PlayerOld.Turn, AI.Turn)) { // check if player has matched
				scenario = 2; // player wins
				return true;
			}
			if (!squares.Contains(" ")) { // if none are blank, nobody has won
				scenario = 1;
				return true; // tie
			}
			return false;
		}

		public static bool WinCheck(string turn, string opponent) { // returns a win for the first param, by checking the input string for the 'turn' of the second param
			bool winner = false;
			foreach (int[] win in winBoxes) { // check possible way to win a tic tac toe game
				string check = string.Empty; // initialize check string
				bool isBlank = false; // initialize blank check bool
				for (int i = 0; i < 3; i++) { // for each box in each combo
					int w = win[i];
					if (squares[w-1].Contains(" ")) { // if box is blank
					isBlank = true;
					break;
					}
					check += squares[w-1];
				}
				if (isBlank) { // blank square exists in one of the selected boxes, meaning there will be no matches
					continue; // check the next one.
				}
				if (!check.Contains(opponent)) { // if opponent's symbol is not in the string, that means the side in question has matched 3 of their symbol
					winner = true; // the player has won
					break;
				}
			}
			return winner;
		}
	}
}
