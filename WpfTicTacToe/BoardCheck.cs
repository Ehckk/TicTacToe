using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfTicTacToe {
	class BoardCheck {
		public static List<int[]> winSpaces = new(); // list of winning move lists
		public static readonly string[] winStrings = { "123", "456", "789", "147", "258", "369", "159", "357" };

		public static void InitializeWinningCombos() {
			foreach (string win in winStrings) {
				char[] winString = win.ToCharArray();
				int[] winParse = new int[winString.Length];
				for (int i = 0; i < winString.Length; i++) {
					winParse[i] = int.Parse(winString[i].ToString());
				}
				winSpaces.Add(winParse.ToArray());
			}
		}

		public static bool CheckForDraw() {
			foreach (Button space in Game.board) {
				if (string.IsNullOrEmpty(space.Content?.ToString())) return false;
			}
			return true;
		}

		public static bool CheckForWin(string piece) {
			if (EvaluateSpaces(GetPlayerSpaces(piece)) != null) { // if winning space numbers are returned
				// someone has won
				// do something with said numbers
				return true;
			} else { // no winning spaces returned
				return false;
				//for (int x = 0; x < 3; x++) {	
				// check columns
				// if column has player piece 
				// else continue
				// switch case to check adjacent spaces maybe?
				//for (int y = 0; y < 3; y++) {
				//}
				//}
			}
		}

		private static int[] GetPlayerSpaces(string piece) { // gathers all spaces that have the player's piece. Ignores all others.
			List<int> playerSpaces = new(); 
			foreach (Button space in Game.board) {
				if (string.IsNullOrEmpty(space.Content?.ToString())) continue;
				if (space.Content?.ToString() != piece) continue;
				playerSpaces.Add(int.Parse(space.Name[^1].ToString()));
			}
			return playerSpaces.ToArray();
		}

		private static int?[] EvaluateSpaces(int[] playerSpaces) {
			List<int?> playerWin = new();
			foreach (int[] win in winSpaces) {
				bool hasWin = true;
				foreach (int w in win) {
					hasWin = playerSpaces.Contains(w);
					if (!hasWin) {
						break;
					} else continue;
				}
				if (!hasWin) continue;
				else {
					foreach (int w in win) {
						playerWin.Add(w);
					}
				}
				return playerWin.ToArray();
			}
			return null;
		}
	}
}
