// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.User;
using NUnit.Framework;

namespace CinemaManagerTest.Modules
{
	public class UserModuleTest : UnitTestBase<UserModule>
	{
		protected override void DoSetup()
		{
			base.DoSetup();
			UnitUnderTest = new UserModule
			{
				UserFilterConfigurator = CreateTrueFilterConfigurator<UserModel>()
			};
		}

		[Test]
		public void AddCorrectlyAddFilm()
		{
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);
			var user1 = new UserModel();
			var user2 = new UserModel();

			cinema.Users.Add(user1);
			cinema.Users.Add(user2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();
			UnitUnderTest.AddUser();

			//Assert
			Assert.That(UnitUnderTest.Users, Has.Count.EqualTo(3));
			Assert.That(cinema.Users, Has.Count.EqualTo(3));
		}

		[Test]
		public void CorrectlyLoadUserData()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);
			var user1 = new UserModel();
			var user2 = new UserModel();

			cinema.Users.Add(user1);
			cinema.Users.Add(user2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();

			//Assert
			Assert.That(UnitUnderTest.Users, Is.EqualTo(cinema.Users));
		}

		[Test]
		public void RemoveCorrectlyRemovesFilm()
		{
			//Arrange
			var model = new CinemasModel();
			var cinema = new CinemaModel
			{
				IsActive = true
			};
			model.Cinemas.Add(cinema);
			var user1 = new UserModel();
			var user2 = new UserModel();

			cinema.Users.Add(user1);
			cinema.Users.Add(user2);

			Session.Instance.DataModel = CreateData(model);

			//Act
			UnitUnderTest.Refresh();
			UnitUnderTest.SelectedUser = user2;
			UnitUnderTest.RemoveUser();

			//Assert
			Assert.That(UnitUnderTest.Users, Has.Count.EqualTo(1));
			Assert.That(UnitUnderTest.Users, Has.Count.EqualTo(1));
		}
	}
}