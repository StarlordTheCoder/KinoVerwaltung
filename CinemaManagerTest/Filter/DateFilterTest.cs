// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using CinemaManager.Filter;
using Moq;
using NUnit.Framework;

namespace CinemaManagerTest.Filter
{
	public class DateFilterTest : UnitTestBase<DateFilter<DummyModel>>
	{
		private void Setup(params Func<DummyModel, DateTime?>[] setup)
		{
			UnitUnderTest = new DateFilter<DummyModel>(string.Empty, setup);
		}

		[Test]
		public void TestCheckCallsValue()
		{
			//Arrange
			Setup(d => d.DateTimeProperty);

			var dummyData = new Mock<DummyModel>();

			//Act
			UnitUnderTest.Check(dummyData.Object);

			//Assert
			dummyData.Verify(d => d.DateTimeProperty, Times.Once);
		}

		[Test]
		public void TestGivenCorrectDates()
		{
			TestDateGivesCorrectResult(DateTime.Today, DateTime.Today + TimeSpan.FromDays(1), DateTime.Today, true);
			TestDateGivesCorrectResult(DateTime.Today, DateTime.Today + TimeSpan.FromDays(1), DateTime.Today - TimeSpan.FromDays(1), false);
			TestDateGivesCorrectResult(DateTime.Today, DateTime.Today + TimeSpan.FromDays(1), DateTime.Today + TimeSpan.FromDays(1), true);
			TestDateGivesCorrectResult(DateTime.Today, DateTime.Today + TimeSpan.FromDays(2), DateTime.Today + TimeSpan.FromDays(1), true);
			TestDateGivesCorrectResult(DateTime.Today, DateTime.Today + TimeSpan.FromDays(1), DateTime.Today + TimeSpan.FromDays(2), false);
		}

		public void TestDateGivesCorrectResult(DateTime start, DateTime end, DateTime input, bool expectedResult)
		{
			//Arrange
			Setup(d => d.DateTimeProperty);
			UnitUnderTest.StartDate = start;
			UnitUnderTest.EndDate = end;

			var dummyData = new Mock<DummyModel>();
			dummyData.Setup(d => d.DateTimeProperty).Returns(input);

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);
			//Assert
			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}