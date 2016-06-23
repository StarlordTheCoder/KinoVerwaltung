// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Room;
using NUnit.Framework;

namespace CinemaManagerTest.Modules
{
	public class RoomModuleTest : UnitTestBase<RoomModule>
	{
		protected override void DoSetup()
		{
			base.DoSetup();
			UnitUnderTest = new RoomModule
			{
				RoomFilterConfigurator = CreateTrueFilterConfigurator<RoomModel>()
			};
		}

		[Test]
		public void AddCorrectlyAddRoom()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);

			var room1 = new RoomModel();
			var room2 = new RoomModel();

			cinema.Rooms.Add(room1);
			cinema.Rooms.Add(room2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();
			UnitUnderTest.AddRoom();

			//Assert
			Assert.That(UnitUnderTest.Rooms, Has.Count.EqualTo(3));
			Assert.That(cinema.Rooms, Has.Count.EqualTo(3));
			Assert.That(UnitUnderTest.Rooms.Select(vm => vm.Model), Is.EquivalentTo(cinema.Rooms));
		}

		[Test]
		public void AddCorrectlyAddSeat()
		{
			//Arrange

			//Act

			//Assert
		}

		[Test]
		public void AddRowCorrectlyAddsRow()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);

			var room = new RoomModel();

			cinema.Rooms.Add(room);

			Session.Instance.DataModel = CreateData(model);

			UnitUnderTest.Refresh();

			UnitUnderTest.SelectedRoom = UnitUnderTest.Rooms.First();

			//Act
			UnitUnderTest.AddRow();
			UnitUnderTest.AddRow();

			//Assert
			Assert.That(UnitUnderTest.SelectedRoom.Rows.First().RowNumber, Is.EqualTo(1));
			Assert.That(UnitUnderTest.SelectedRoom.Rows.Last().RowNumber, Is.EqualTo(2));
		}

		[Test]
		public void AddRowWithSelectedUpdatesRownumbers()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);

			var room = new RoomModel();

			cinema.Rooms.Add(room);

			Session.Instance.DataModel = CreateData(model);

			UnitUnderTest.Refresh();
			UnitUnderTest.SelectedRoom = UnitUnderTest.Rooms.First();

			var row1 = new RowViewModel(1, new List<SeatModel>());
			var row2 = new RowViewModel(2, new List<SeatModel>());

			UnitUnderTest.SelectedRoom.Rows.Add(row1);
			UnitUnderTest.SelectedRoom.Rows.Add(row2);

			UnitUnderTest.SelectedRoom.SelectedRow = row1;

			//Act
			UnitUnderTest.AddRow();

			//Assert
			Assert.That(row1.RowNumber, Is.EqualTo(1));
			Assert.That(UnitUnderTest.SelectedRoom.Rows.First(r => !Equals(r, row1) && !Equals(r, row2)).RowNumber, Is.EqualTo(2));
			Assert.That(row2.RowNumber, Is.EqualTo(3));
		}

		[Test]
		public void CorrectlyLoadRoomData()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);

			var room1 = new RoomModel();
			var room2 = new RoomModel();

			cinema.Rooms.Add(room1);
			cinema.Rooms.Add(room2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();

			//Assert
			Assert.That(UnitUnderTest.Rooms.Select(vm => vm.Model), Is.EquivalentTo(cinema.Rooms));
		}

		[Test]
		public void CorrectlyLoadRowData()
		{
			//Arrange

			//Act

			//Assert
		}

		[Test]
		public void CorrectlyLoadSeatData()
		{
			//Arrange

			//Act

			//Assert
		}

		[Test]
		public void RemoveCorrectlyRemovesRoom()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);

			var room1 = new RoomModel();
			var room2 = new RoomModel();

			cinema.Rooms.Add(room1);
			cinema.Rooms.Add(room2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();
			UnitUnderTest.SelectedRoom = UnitUnderTest.Rooms.First(r => Equals(r.Model, room1));
			UnitUnderTest.RemoveRoom();

			//Assert
			Assert.That(UnitUnderTest.Rooms, Has.Count.EqualTo(1));
			Assert.That(cinema.Rooms, Has.Count.EqualTo(1));
			Assert.That(cinema.Rooms.Contains(room2));
		}

		[Test]
		public void RemoveCorrectlyRemovesRow()
		{
			//Arrange

			//Act

			//Assert
		}

		[Test]
		public void RemoveCorrectlyRemovesSeat()
		{
			//Arrange

			//Act

			//Assert
		}
	}
}