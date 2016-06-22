using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Cinema;
using NUnit.Framework;

namespace CinemaManagerTest.Modules
{
	public class CinemaModuleTest : UnitTestBase<CinemaModule>
	{
		protected override void DoSetup()
		{
			base.DoSetup();
			UnitUnderTest = new CinemaModule(m => { })
			{
				CinemaFilterConfigurator = CreateTrueFilterConfigurator<CinemaModel>()
			};
		}

		[Test]
		public void CorrectlyLoadsModelData()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema1 = new CinemaModel
			{
				IsActive = true
			};

			var cinema2 = new CinemaModel();

			model.Cinemas.Add(cinema1);
			model.Cinemas.Add(cinema2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();

			//Assert
			Assert.That(UnitUnderTest.Cinemas, Is.EquivalentTo(model.Cinemas));
		}

		[Test]
		public void AddCorrectlyAddsCinema()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema1 = new CinemaModel
			{
				IsActive = true
			};

			var cinema2 = new CinemaModel();

			model.Cinemas.Add(cinema1);
			model.Cinemas.Add(cinema2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();
			UnitUnderTest.AddCinema();

			//Assert
			Assert.That(UnitUnderTest.Cinemas, Has.Count.EqualTo(3));
			Assert.That(model.Cinemas, Has.Count.EqualTo(3));
			Assert.That(cinema1.IsActive, Is.False);
		}

		[Test]
		public void RemoveCorrectlyRemovesCinema()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema1 = new CinemaModel
			{
				IsActive = true
			};

			var cinema2 = new CinemaModel();

			model.Cinemas.Add(cinema1);
			model.Cinemas.Add(cinema2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();
			UnitUnderTest.RemoveCinema();

			//Assert
			Assert.That(UnitUnderTest.Cinemas, Has.Count.EqualTo(1));
			Assert.That(model.Cinemas, Has.Count.EqualTo(1));
			Assert.That(model.Cinemas.First(), Is.EqualTo(cinema2));
			Assert.That(UnitUnderTest.Cinemas.First(), Is.EqualTo(cinema2));
		}
	}
}
