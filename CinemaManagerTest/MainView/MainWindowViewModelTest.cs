// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using CinemaManager.Infrastructure;
using CinemaManager.MainView;
using NUnit.Framework;

namespace CinemaManagerTest.MainView
{
	public class MainWindowViewModelTest : UnitTestBase<MainWindowViewModel>
	{
		[Test]
		public void TestSatanDataStartupFileIsUsed()
		{
			const string startupFile = "example.satanData";

			UnitUnderTest = new MainWindowViewModel(startupFile);

			Assert.That(Session.Instance.DataPath, Is.EqualTo(startupFile));
			Assert.That(Session.Instance.LayoutPath, !Is.EqualTo(startupFile));
		}

		[Test]
		public void TestSatanStartupFileIsUsed()
		{
			const string startupFile = "example.satan";

			UnitUnderTest = new MainWindowViewModel(startupFile);

			Assert.That(Session.Instance.LayoutPath, Is.EqualTo(startupFile));
			Assert.That(Session.Instance.DataPath, !Is.EqualTo(startupFile));
		}
	}
}