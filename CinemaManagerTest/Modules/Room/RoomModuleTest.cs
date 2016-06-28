// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Room;
using NUnit.Framework;

namespace CinemaManagerTest.Modules.Room
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
	}
}