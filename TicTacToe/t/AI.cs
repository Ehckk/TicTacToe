using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe {
	class AI {
		public AI(string turn) {
			Turn = turn;
		}

		public static string Turn { get; set; }
		private static int[] corners = { 1, 3, 7, 9 };
		private static readonly int[] sides = { 2, 4, 6, 8 };
		private static readonly int center = 5;
		public static bool isComGoingFirst;
		public static int playerFirstMove;
		static readonly Random random = new();

		public static void TakeTurn() {
			Game.PrintBoard();
			Console.WriteLine("It is the computer's turn");
			MakeMove(MoveManager()); 
			if (Game.EndCheck()) {
				Program.EndGame(Game.scenario);
			} else {
				PlayerOld.TakeTurn();
			}
		}

		public static void MakeMove(int move) {
			Game.squares[move-1] = Turn;
			Game.possibleMoves.RemoveAt(move); // the move is no longer possible
		}

		public static int MoveManager() {
			if (!Game.squares.Contains(Turn)) { // if the board does not contain any AI moves, then it is currently the COMs first turn
				return FirstMove(isComGoingFirst);
			} else {
				return GetBestMove(isComGoingFirst);
			}
		}

		private static int FirstMove(bool goingFirst) {
			int move;
			if (goingFirst) { // always start by moving in a random corner
				move = GetRandomCorner();
			} else {
				if (corners.Contains(playerFirstMove)) { // player first moved in corner
					move = center;
				} else if (sides.Contains(playerFirstMove)) { // player first moved in a side
					move = center;
				} else { // player first moved in center
					move = GetRandomCorner();
				}
			} 
			return move;
		}

		private static int GetBestMove(bool goingFirst) {
			string turn;
			if (goingFirst) {
				turn = Turn; // play offensviely
			} else {
				turn = PlayerOld.Turn; // play defensively
			}
			int[] possibleMoveWeights = EvaluatePossibleMoves(turn);
			int bestMove = Game.possibleMoves[Array.IndexOf(possibleMoveWeights, possibleMoveWeights.Max())];
			// EvaluateBestMove(Player.Turn); // get list off best moves to block with
			return bestMove;
		}

		private static int[] EvaluatePossibleMoves(string piece) { // this function takes all the possible moves and weighs them in how good of a move they are
			// order possible moves in bestMoves list in order of how many winning combinations they set up with a priority to corners
			List<int> bestMoves = new();
			foreach (int i in Game.possibleMoves) { // checks every possible move
				int totalMoveWeight = 0; // weight of how good the move is with how many possibilities it sets up
				foreach (int[] win in Game.winBoxes) {
					if (!win.Contains(i)) { // if win string does not include the possible move in question, skip it
						continue;
					} else {
						foreach (int w in win) { // for each box in a winning string with the target move;
							int weight = 0;
							if (Game.possibleMoves.Contains(w)) {
								weight += 2;
							} else if (Game.squares[w-1] == piece) { // it is the piece of the player in question
								weight += 3; // more weight if the move is next to boxes with their piece
							} else {
								weight += 1;
							}
							if (corners.Contains(w)) {
								weight *= 3;
							} else if (sides.Contains(w)) {
								weight *= 1;
							} else { // center
								weight *= 2;
							}
							totalMoveWeight += weight;
						}
					}
				}
				bestMoves.Add(totalMoveWeight); // position in this array corresponds to the move in possiblemoves array of the same position
				// have the AI do the same calculations with respect to player moves
				// play offensively when going first
				// play defensively when going second
			}
			return bestMoves.ToArray();
		}

		private static bool IsValidMove(int m) {
			if (Game.possibleMoves.Contains(m)) {
				return true;
			} else {
				return false;
			}
		}

		private static int GetRandomCorner() {
			int r = random.Next(corners.Length);
			return corners[r];
		}
	}
}
