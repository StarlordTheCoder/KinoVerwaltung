using System;

namespace CinemaManager.Model
{
	[Serializable]
	public class SeatModel
	{
		public int Row { get; set; }
		public int Number { get; set; }
		public SeatType SeatType { get; set; }
	}
}