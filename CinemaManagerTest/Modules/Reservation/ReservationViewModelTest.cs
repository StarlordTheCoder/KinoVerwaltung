// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Presentation;
using CinemaManager.Modules.Reservation;
using NUnit.Framework;

namespace CinemaManagerTest.Modules.Reservation
{
	public class ReservationViewModelTest : UnitTestBase<ReservationViewModel>
	{
		/*[Test]
		public void CorrectlyLoadsModelData()
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

			var reservator = new UserModel
			{
				UserId = 1
			};

			var model = new PresentationModel
			{
				FilmId = film.FilmId,
				Reservations = new List<ReservationModel>
				{
					new ReservationModel
					{
						ReservatorId = reservator.UserId,
						Seats = seats.GetRange(1, 2).Select(s => s.Place).ToList()
					}
				},
				RoomNumber = room.RoomNumber
			};

			var cinema = new CinemaModel();
			cinema.Films.Add(film);
			cinema.Presentations.Add(model);
			cinema.Users.Add(reservator);
			cinema.Rooms.Add(room);
			cinema.IsActive = true;

			var cinemas = new CinemasModel();
			cinemas.Cinemas.Add(cinema);

			Session.Instance.DataModel = CreateData(cinemas);

			//Act
			UnitUnderTest = new PresentationViewModel(model);

			//Assert
			Assert.That(UnitUnderTest.Model, Is.EqualTo(model));
			Assert.That(UnitUnderTest.Film, Is.EqualTo(film));
			Assert.That(UnitUnderTest.RoomViewModel.Model, Is.EqualTo(room));

			//4 Seats total, 2 available
			Assert.That(UnitUnderTest.RoomViewModel.Rows.SelectMany(r => r.Seats).Count(), Is.EqualTo(4));
			Assert.That(UnitUnderTest.RoomViewModel.AvailableSeats, Is.EqualTo(2));

		}*/
	}
}