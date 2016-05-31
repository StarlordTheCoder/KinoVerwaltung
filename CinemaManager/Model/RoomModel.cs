using System;

namespace CinemaManager.Model
{
	[Serializable]
	public class RoomModel
	{
		public int RoomNumber { get; set; }
		public SeatModel[] Seats { get; set; }
	}
}