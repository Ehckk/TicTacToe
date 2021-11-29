using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTicTacToe {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}
		
		private void Button_Space_Loaded(object sender, RoutedEventArgs e) {
			Game.board.Add((Button)sender);
		}

		private void Button_Space_Click(object sender, RoutedEventArgs e) {
			Button space = (Button)sender;
			if (!String.IsNullOrEmpty(space.Content?.ToString())) return;
			space.Content = Game.CurrentPlayer.Piece;
			space.Foreground = Game.CurrentPlayer.Color;
			space.IsEnabled = false;

			if (BoardCheck.CheckForWin(Game.CurrentPlayer.Piece.ToString())) {
				_ = MessageBox.Show($"Player {Game.CurrentPlayer.Number} ({Game.CurrentPlayer.Piece}) wins!", "Game Over!");
				Game.CurrentPlayer.winCount += 1;
				// maybe draw a line object across grids or highlight the winning boxes
				Button_NewGame_Click(new object(), new RoutedEventArgs());
			} else { // the player who has just moved has not won
				if (BoardCheck.CheckForDraw()) { // if there are no more spaces to play in
					_ = MessageBox.Show($"The game ends in a draw!", "Game Over!");
					Button_NewGame_Click(new object(), new RoutedEventArgs());
				} else { // there are spaces still open
						 // if opponent == player 
					Game.SwapCurrentPlayer(); // change turns
					// else
					// AI takes turn
				}
			}
			//string test = string.Empty;
			//foreach (Button b in board) {
			//	test += $"{b.Name} ";
			//}
			//_ = MessageBox.Show($"{test}", "Hello World!");
		}

		private void Button_NewGame_Click(object sender, RoutedEventArgs e) {
			Game.SwapPlayerPieces();
			foreach (Button space in Game.board) {
				space.Content = string.Empty;
				space.IsEnabled = true;
			}
			Update_Labels();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			BoardCheck.InitializeWinningCombos();
			Game.InitializePlayers();
			Update_Labels();
		}

		private void Update_Labels() {
			Update_Label(Label_Player1_Score, Game.Player1);
			Update_Label(Label_Player2_Score, Game.Player2);
		}

		private void Update_Label(object sender, Player player) {
			Label label = (Label)sender;
			label.Content = $"Player {player.Number} [{player.Piece}]: {player.winCount}";
		}

		private void Button_Close_Click(object sender, RoutedEventArgs e) {
			Close();
		}

		private void Button_Minimize_Click(object sender, RoutedEventArgs e) {
			WindowState = WindowState.Minimized;
		}

		private void Window_KeyDown(object sender, KeyEventArgs e) {
			switch (e.Key) {
				case Key.D1:
					Button_Space_Click(Button_Space_1, new RoutedEventArgs());
					break;
				case Key.D2:
					Button_Space_Click(Button_Space_2, new RoutedEventArgs());
					break;
				case Key.D3:
					Button_Space_Click(Button_Space_3, new RoutedEventArgs());
					break;
				case Key.D4:
					Button_Space_Click(Button_Space_4, new RoutedEventArgs());
					break;
				case Key.D5:
					Button_Space_Click(Button_Space_5, new RoutedEventArgs());
					break;
				case Key.D6:
					Button_Space_Click(Button_Space_6, new RoutedEventArgs());
					break;
				case Key.D7:
					Button_Space_Click(Button_Space_7, new RoutedEventArgs());
					break;
				case Key.D8:
					Button_Space_Click(Button_Space_8, new RoutedEventArgs());
					break;
				case Key.D9:
					Button_Space_Click(Button_Space_9, new RoutedEventArgs());
					break;
				default:
					// add keyboard shortcuts to choose opponent/reset board
					break;
			}
		}
	}
}
