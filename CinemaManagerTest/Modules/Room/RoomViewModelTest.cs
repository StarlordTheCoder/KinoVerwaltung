using System.Collections.Generic;
using System.Linq;
using CinemaManager.Model;
using CinemaManager.Modules.Room;
using NUnit.Framework;

namespace CinemaManagerTest.Modules.Room
{
	public class RoomViewModelTest : UnitTestBase<RoomViewModel>
	{
		[Test]
		public void AddRowCorrectlyAddsRow()
		{
			//Arrange
			var room = new RoomModel();

			UnitUnderTest = new RoomViewModel(room);

			//Act
			UnitUnderTest.AddRow();
			UnitUnderTest.AddRow();

			//Assert
			Assert.That(UnitUnderTest.Rows.First().RowNumber, Is.EqualTo(1));
			Assert.That(UnitUnderTest.Rows.Last().RowNumber, Is.EqualTo(2));
		}

		[Test]
		public void AddRowWithSelectedUpdatesRownumbers()
		{
			//Arrange
			var room = new RoomModel();

			UnitUnderTest = new RoomViewModel(room);

			var row1 = new RowViewModel(1, new List<SeatModel>());
			var row2 = new RowViewModel(2, new List<SeatModel>());

			UnitUnderTest.Rows.Add(row1);
			UnitUnderTest.Rows.Add(row2);

			UnitUnderTest.SelectedRow = row1;

			//Act
			UnitUnderTest.AddRow();

			//Assert
			Assert.That(row1.RowNumber, Is.EqualTo(1));
			Assert.That(UnitUnderTest.Rows.First(r => !Equals(r, row1) && !Equals(r, row2)).RowNumber, Is.EqualTo(2));
			Assert.That(row2.RowNumber, Is.EqualTo(3));
		}

		[Test]
		public void AddRowWithoutSelectionAddsRowAtTheBottom()
		{
			//Arrange
			var room = new RoomModel();

			UnitUnderTest = new RoomViewModel(room);

			var row1 = new RowViewModel(1, new List<SeatModel>());
			var row2 = new RowViewModel(2, new List<SeatModel>());

			UnitUnderTest.Rows.Add(row1);
			UnitUnderTest.Rows.Add(row2);

			//Act
			UnitUnderTest.AddRow();

			//Assert
			Assert.That(row1.RowNumber, Is.EqualTo(1));
			Assert.That(row2.RowNumber, Is.EqualTo(2));
			Assert.That(UnitUnderTest.Rows.Last().RowNumber, Is.EqualTo(3));
		}

		[Test]
		public void AddSeatWithoutSelectionAddsSeatAtTheEnd()
		{
			//Arrange
			var room = new RoomModel();

			UnitUnderTest = new RoomViewModel(room);

			var row1 = new RowViewModel(1, new List<SeatModel>());

			UnitUnderTest.Rows.Add(row1);

			UnitUnderTest.SelectedRow = row1;

			//Act
			UnitUnderTest.AddSeat();
			UnitUnderTest.AddSeat();

			//Assert
			Assert.That(row1.Seats, Has.Count.EqualTo(2));
			Assert.That(row1.Seats.First().Model.Place.Number, Is.EqualTo(1));
			Assert.That(row1.Seats.Last().Model.Place.Number, Is.EqualTo(2));
		}

		[Test]
		public void RemoveRowUpdatesRowNumbers()
		{
			//Arrange
			var room = new RoomModel();

			UnitUnderTest = new RoomViewModel(room);

			var row1 = new RowViewModel(1, new List<SeatModel>());
			var row2 = new RowViewModel(2, new List<SeatModel>());
			var row3 = new RowViewModel(3, new List<SeatModel>());

			UnitUnderTest.Rows.Add(row1);
			UnitUnderTest.Rows.Add(row2);
			UnitUnderTest.Rows.Add(row3);

			UnitUnderTest.SelectedRow = row1;

			//Act
			UnitUnderTest.RemoveRow();

			//Assert
			Assert.That(UnitUnderTest.Rows.Contains(row1), Is.False);
			Assert.That(row2.RowNumber, Is.EqualTo(1));
			Assert.That(row3.RowNumber, Is.EqualTo(2));
		}

		[Test]
		public void CorrectlyLoadRoomData()
		{
			//Arrange
			var seat1 = new SeatModel
			{
				Place = new SeatIdentifier
				{
					Number = 1,
					Row = 1
				}
			};
			var seat2 = new SeatModel
			{
				Place = new SeatIdentifier
				{
					Number = 2,
					Row = 1
				}
			};
			var seat3 = new SeatModel
			{
				Place = new SeatIdentifier
				{
					Number = 1,
					Row = 2
				}
			};

			var room = new RoomModel
			{
				Seats = new List<SeatModel>
				{
					seat1, seat2, seat3
				}
			};

			//Act
			UnitUnderTest = new RoomViewModel(room);

			//Assert
			Assert.That(UnitUnderTest.Rows, Has.Count.EqualTo(2));
			Assert.That(UnitUnderTest.Rows.First().Seats.Select(s => s.Model), Contains.Item(seat1));
			Assert.That(UnitUnderTest.Rows.First().Seats.Select(s => s.Model), Contains.Item(seat2));
			Assert.That(UnitUnderTest.Rows.Last().Seats.Select(s => s.Model), Contains.Item(seat3));
		}

		[Test]
		public void RemoveSeatUpdatesSeatNumbers()
		{
			//Arrange
			var seat1 = new SeatModel
			{
				Place = new SeatIdentifier
				{
					Number = 1,
					Row = 1
				}
			};
			var seat2 = new SeatModel
			{
				Place = new SeatIdentifier
				{
					Number = 2,
					Row = 1
				}
			};
			var seat3 = new SeatModel
			{
				Place = new SeatIdentifier
				{
					Number = 3,
					Row = 1
				}
			};

			var room = new RoomModel
			{
				Seats = new List<SeatModel>
				{
					seat1, seat2, seat3
				}
			};

			UnitUnderTest = new RoomViewModel(room);

			UnitUnderTest.SelectedSeats.Add(UnitUnderTest.Rows.First().Seats.First(s => s.Model.Place.Number == 2));

			//Act
			UnitUnderTest.RemoveSeat();

			//Assert
			Assert.That(seat1.Place.Number, Is.EqualTo(1));
			Assert.That(!room.Seats.Contains(seat2));
			Assert.That(seat3.Place.Number, Is.EqualTo(2));
		}
	}
}
