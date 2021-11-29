using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe {
	public static class Player {
		public static double Weight(string piece) {
			double score;
			switch (piece) {
				case P.Turn:
					score = 1;
					break;
				case "Opponent":
					score = 2;
				default:
					break;
			}

		}

	}
}
