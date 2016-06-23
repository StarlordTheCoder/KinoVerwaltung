// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Film;
using NUnit.Framework;

namespace CinemaManagerTest.Modules
{
	public class FilmModuleTest : UnitTestBase<FilmModule>
	{
		protected override void DoSetup()
		{
			base.DoSetup();
			UnitUnderTest = new FilmModule
			{
				FilmFilterConfigurator = CreateTrueFilterConfigurator<FilmModel>()
			};
		}

		[Test]
		public void AddCorrectlyAddFilm()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);
			var film1 = new FilmModel();
			var film2 = new FilmModel();
			cinema.Films.Add(film1);
			cinema.Films.Add(film2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();
			UnitUnderTest.AddFilm();

			//Assert
			Assert.That(UnitUnderTest.Films, Has.Count.EqualTo(3));
			Assert.That(cinema.Films, Has.Count.EqualTo(3));
		}

		[Test]
		public void CorrectlyLoadsModelData()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);
			var film1 = new FilmModel();
			var film2 = new FilmModel();
			cinema.Films.Add(film1);
			cinema.Films.Add(film2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();

			//Assert
			Assert.That(UnitUnderTest.Films, Is.EqualTo(cinema.Films));
		}

		[Test]
		public void RemovesCorrectlyRemoveFilm()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);
			var film1 = new FilmModel();
			var film2 = new FilmModel();
			cinema.Films.Add(film1);
			cinema.Films.Add(film2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();
			UnitUnderTest.SelectedFilm = film1;
			UnitUnderTest.RemoveFilm();

			//Assert
			Assert.That(UnitUnderTest.Films, Has.Count.EqualTo(1));
			Assert.That(cinema.Films, Has.Count.EqualTo(1));
		}
	}
}