using System;

namespace TicTacToe {
	class Program {
		static int winCount;
		static int tieCount;
		static int lossCount;

		static void Main(string[] args) {
			StartGame();
			return;
		}

		static void StartGame() {
			Console.WriteLine("How to play: ");
			Game.InitializeBoxes(true);
			Console.WriteLine("You will be assigned as either the 'X' player, or the 'O' player. The 'X' player will always go first.");
			Console.WriteLine("When it is your turn, write the number that corresponds to the space on the board where you would like to place your symbol, as shown here: ");
			Game.PrintBoard();
			Console.WriteLine("The first player to match 3 of their respective symbol wins.");
			Console.WriteLine("If no squares are left on the board and no player has matched three of their symbol, the game ends in a tie.");
			Console.WriteLine("If you are ready to play, type 'ready': ");
			while (Console.ReadLine().ToLower() != "ready") {
					Console.WriteLine("Please type 'ready' to begin!");
					Console.ReadLine();
					break;
			}
			Game.PlayGame();
		}

		public static void EndGame(int? result) {
			switch (result) {
				case 1:
					tieCount += 1;
					Console.WriteLine("This game ends in a tie!");
					break;
				case 2:
					winCount += 1;
					Console.WriteLine("You have won the game! Congratulations!");
					break;
				default: // 0
					lossCount += 1;
					Console.WriteLine("The computer has won the game! Better luck next time!");
					break;
			}
			PlayAgain();
		}

		static void PlayAgain() {
			Console.WriteLine("Type 'y' to play again, anything else to exit");
			if (Console.ReadLine().ToLower() == "y") {
				Game.PlayGame();
			} else {
				string msgExit = string.Format("You have won {0} times, tied {1} times, and lost {2} times", winCount, tieCount, lossCount);
				Console.WriteLine(msgExit);
				// print how many times you tied, and how many times you lost (win count will always be zero)
				// exit the program
				return;
			}
		}
	}
}
