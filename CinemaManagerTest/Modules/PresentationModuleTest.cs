// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Presentation;
using NUnit.Framework;

namespace CinemaManagerTest.Modules
{
	public class PresentationModuleTest : UnitTestBase<PresentationModule>
	{
		protected override void DoSetup()
		{
			base.DoSetup();
			UnitUnderTest = new PresentationModule
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
			Assert.That(UnitUnderTest.Presentations, Is.EqualTo(cinema.Presentations));
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
			UnitUnderTest.SelectedPresentation = pres2;
			UnitUnderTest.RemovePresentation();

			//Assert
			Assert.That(UnitUnderTest.Presentations, Has.Count.EqualTo(1));
			Assert.That(cinema.Presentations, Has.Count.EqualTo(1));
		}
	}
}