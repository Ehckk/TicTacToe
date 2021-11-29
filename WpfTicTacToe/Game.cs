using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Drawing;

namespace WpfTicTacToe {
	class Game {
		public static Player Player1 { get; set; }
		public static Player Player2 { get; set; }
		public static Player CurrentPlayer;
		public static List<Button> board = new();
		private const char X = 'X';
		private const char O = 'O';
		private static readonly System.Drawing.Color blue = System.Drawing.Color.DeepSkyBlue;
		private static readonly System.Drawing.Color gold = System.Drawing.Color.DarkGoldenrod;
		//private Random random = new();

		public static void InitializePlayers() {
			Player1 = new Player('1', X, blue);
			Player2 = new Player('2', O, gold);
			SwapCurrentPlayer();
		}

		public static void SwapCurrentPlayer() {
			if (CurrentPlayer == Player1) {
				CurrentPlayer = Player2;
			} else { // CurrentPlayer = Player2
				CurrentPlayer = Player1;
			}
		}

		public static void SwapPlayerPieces() {
			if (Player1.Piece == X) {
				Player1.Piece = O;
				Player2.Piece = X;
				CurrentPlayer = Player2;
			} else { // Player1.Piece = O
				Player1.Piece = X;
				Player2.Piece = O;
				CurrentPlayer = Player1;
			}
		}

		// add another function for AI
	}
}
