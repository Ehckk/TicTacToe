using System.Windows.Media;
using System.Drawing;

namespace WpfTicTacToe {
	public class Player {
		public char Number { get; set; }
		public char Piece { get; set; }
		public Brush Color { get; set; }
		public int winCount = 0;

		public Player(char number, char piece, System.Drawing.Color color) {
			Number = number;
			Piece = piece;
			Color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
		}
	}
}
