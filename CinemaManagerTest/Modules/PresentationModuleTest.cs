// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Film;
using CinemaManager.Modules.Presentation;
using CinemaManager.Modules.Room;
using Moq;
using NUnit.Framework;

namespace CinemaManagerTest.Modules
{
	public class PresentationModuleTest : UnitTestBase<PresentationModule>
	{
		private FilmModel _selectedFilmModel = new FilmModel();
		private RoomModel _selectedRoomModel = new RoomModel();

		protected override void DoSetup()
		{
			base.DoSetup();

			var filmModule = new Mock<FilmModule>();
			filmModule.Setup(f => f.SelectedFilm).Returns(_selectedFilmModel);

			var roomModule = new Mock<RoomModule>();
			roomModule.Setup(f => f.SelectedRoom).Returns(new RoomViewModel(_selectedRoomModel));

			UnitUnderTest = new PresentationModule(filmModule.Object, roomModule.Object)
			{
				PresentationFilterConfigurator = CreateTrueFilterConfigurator<PresentationModel>()
			};
		}

		[Test]
		public void AddCorrectlyAddPresentation()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);
			var pres1 = new PresentationModel();
			var pres2 = new PresentationModel();

			cinema.Presentations.Add(pres1);
			cinema.Presentations.Add(pres2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();
			UnitUnderTest.AddPresentation();

			//Assert
			Assert.That(UnitUnderTest.Presentations, Has.Count.EqualTo(3));
			Assert.That(cinema.Presentations, Has.Count.EqualTo(3));
		}

		[Test]
		public void CorrectlyLoadPresenationData()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);
			var pres1 = new PresentationModel();
			var pres2 = new PresentationModel();

			cinema.Presentations.Add(pres1);
			cinema.Presentations.Add(pres2);

			Session.Instance.DataModel = CreateData(model);
			//Act
			UnitUnderTest.Refresh();

			//Assert
			Assert.That(UnitUnderTest.Presentations.Select(p => p.Model), Is.EquivalentTo(cinema.Presentations));
		}

		[Test]
		public void RemoveCorrectlyRemovesPresentation()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);
			var pres1 = new PresentationModel();
			var pres2 = new PresentationModel();

			cinema.Presentations.Add(pres1);
			cinema.Presentations.Add(pres2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();
			UnitUnderTest.SelectedPresentation = UnitUnderTest.Presentations.First(p => Equals(p.Model, pres2));
			UnitUnderTest.RemovePresentation();

			//Assert
			Assert.That(UnitUnderTest.Presentations, Has.Count.EqualTo(1));
			Assert.That(cinema.Presentations, Has.Count.EqualTo(1));
		}
	}
}