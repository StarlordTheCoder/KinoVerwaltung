using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager.Model
{
	[Serializable]
	public class CinemaModel
	{
		public string Name { get; set; }
		public RoomModel[] Rooms { get; set; }
		public string Address { get; set; }
	}
}
