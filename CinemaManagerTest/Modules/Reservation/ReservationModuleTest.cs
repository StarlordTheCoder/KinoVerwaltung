// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Presentation;
using CinemaManager.Modules.Reservation;
using CinemaManager.Modules.User;
using Moq;
using NUnit.Framework;

namespace CinemaManagerTest.Modules.Reservation
{
	public class ReservationModuleTest : UnitTestBase<ReservationModule>
	{
		private PresentationModel _selectedPresentationModel;
		private UserModel _selectedUserModel;

		protected override void DoSetup()
		{
			base.DoSetup();

			_selectedPresentationModel = new PresentationModel();
			_selectedUserModel = new UserModel();

			var presentationModule = new Mock<IPresentationModule>();
			presentationModule.Setup(f => f.SelectedPresentation).Returns(new PresentationViewModel(_selectedPresentationModel));

			var userModule = new Mock<IUserModule>();
			userModule.Setup(f => f.SelectedUser).Returns(_selectedUserModel);

			UnitUnderTest = new ReservationModule(presentationModule.Object, userModule.Object)
			{
				ReservationFilterConfigurator = CreateTrueFilterConfigurator<ReservationModel>()
			};
		}

		[Test]
		public void AddCorrectlyAddsReservation()
		{
			//Arrange
			var film = new FilmModel
			{
				FilmId = 1
			};

			var seats = new List<SeatModel>
			{
				new SeatModel
				{
					Place = new SeatIdentifier {Number = 1, Row = 1}
				},
				new SeatModel
				{
					Place = new SeatIdentifier {Number = 2, Row = 1}
				},
				new SeatModel
				{
					Place = new SeatIdentifier {Number = 3, Row = 1}
				},
				new SeatModel
				{
					Place = new SeatIdentifier {Number = 4, Row = 1}
				}
			};

			var room = new RoomModel
			{
				RoomNumber = 1,
				Seats = seats
			};

			_selectedUserModel.UserId = 1;

			_selectedPresentationModel.FilmId = film.FilmId;
			_selectedPresentationModel.RoomNumber = room.RoomNumber;


			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};

			cinema.Films.Add(film);
			cinema.Rooms.Add(room);
			cinema.Presentations.Add(_selectedPresentationModel);
			cinema.Users.Add(_selectedUserModel);

			model.Cinemas.Add(cinema);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();
			UnitUnderTest.AddReservation();

			//Assert
			Assert.That(UnitUnderTest.Reservations, Has.Count.EqualTo(1));
			Assert.That(_selectedPresentationModel.Reservations, Has.Count.EqualTo(1));
			Assert.That(_selectedPresentationModel.Reservations.First().ReservatorId, Is.EqualTo(_selectedUserModel.UserId));
		}
		
		[Test]
		public void CorrectlyLoadPresentationData()
		{
			//Arrange
			var film = new FilmModel
			{
				FilmId = 1
			};

			var seats = new List<SeatModel>
			{
				new SeatModel
				{
					Place = new SeatIdentifier {Number = 1, Row = 1}
				},
				new SeatModel
				{
					Place = new SeatIdentifier {Number = 2, Row = 1}
				},
				new SeatModel
				{
					Place = new SeatIdentifier {Number = 3, Row = 1}
				},
				new SeatModel
				{
					Place = new SeatIdentifier {Number = 4, Row = 1}
				}
			};

			var room = new RoomModel
			{
				RoomNumber = 1,
				Seats = seats
			};

			_selectedUserModel.UserId = 1;

			_selectedPresentationModel.FilmId = film.FilmId;
			_selectedPresentationModel.Reservations.Add(new ReservationModel
			{
				ReservatorId = _selectedUserModel.UserId,
				Seats = seats.GetRange(1, 2).Select(s => s.Place).ToList()
			});
			_selectedPresentationModel.RoomNumber = room.RoomNumber;


			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};

			cinema.Films.Add(film);
			cinema.Rooms.Add(room);
			cinema.Presentations.Add(_selectedPresentationModel);
			cinema.Users.Add(_selectedUserModel);

			model.Cinemas.Add(cinema);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();

			//Assert
			Assert.That(UnitUnderTest.Reservations, Has.Count.EqualTo(1));
		}

		[Test]
		public void RemoveCorrectlyRemovesPresentation()
		{
			//Arrange
			var film = new FilmModel
			{
				FilmId = 1
			};

			var seats = new List<SeatModel>
			{
				new SeatModel
				{
					Place = new SeatIdentifier {Number = 1, Row = 1}
				},
				new SeatModel
				{
					Place = new SeatIdentifier {Number = 2, Row = 1}
				},
				new SeatModel
				{
					Place = new SeatIdentifier {Number = 3, Row = 1}
				},
				new SeatModel
				{
					Place = new SeatIdentifier {Number = 4, Row = 1}
				}
			};

			var room = new RoomModel
			{
				RoomNumber = 1,
				Seats = seats
			};

			_selectedUserModel.UserId = 1;

			_selectedPresentationModel.FilmId = film.FilmId;
			_selectedPresentationModel.Reservations.Add(new ReservationModel
			{
				ReservatorId = _selectedUserModel.UserId,
				Seats = seats.GetRange(1, 2).Select(s => s.Place).ToList()
			});
			_selectedPresentationModel.RoomNumber = room.RoomNumber;


			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};

			cinema.Films.Add(film);
			cinema.Rooms.Add(room);
			cinema.Presentations.Add(_selectedPresentationModel);
			cinema.Users.Add(_selectedUserModel);

			model.Cinemas.Add(cinema);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();
			UnitUnderTest.SelectedReservation = UnitUnderTest.Reservations.First();
			UnitUnderTest.RemoveReservation();

			//Assert
			Assert.That(UnitUnderTest.Reservations, Has.Count.EqualTo(0));
			Assert.That(_selectedPresentationModel.Reservations, Has.Count.EqualTo(0));
		}
	}
}