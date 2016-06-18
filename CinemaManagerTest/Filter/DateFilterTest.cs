// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using CinemaManager.Filter.Date;
using Moq;
using NUnit.Framework;

namespace CinemaManagerTest.Filter
{
	public class DateFilterTest : UnitTestBase<DateFilter<IDummyModel>>
	{
		private readonly DateTime _startDate = DateTime.Today;
		private readonly DateTime _endDate = DateTime.Today + TimeSpan.FromDays(1);

		private void Setup(params Func<IDummyModel, DateTime?>[] setup)
		{
			UnitUnderTest = new DateFilter<IDummyModel>(string.Empty, setup);
		}

		private void TestDateGivesCorrectResult(DateTime start, DateTime end, DateTime input, bool expectedResult)
		{
			//Arrange
			Setup(d => d.DateTimeProperty);
			UnitUnderTest.StartDate = start;
			UnitUnderTest.EndDate = end;

			var dummyData = new Mock<IDummyModel>();
			dummyData.Setup(d => d.DateTimeProperty).Returns(input);

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);
			//Assert
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void TestChangeValueCallsFilterChanged()
		{
			//Arrange
			Setup();
			var eventCalled = 0;

			UnitUnderTest.FilterChanged += (sender, e) => eventCalled++;

			//Act
			UnitUnderTest.StartDate = UnitUnderTest.EndDate = DateTime.Now;

			//Assert
			Assert.That(eventCalled, Is.EqualTo(2));
		}

		[Test]
		public void TestCheckCallsValue()
		{
			//Arrange
			Setup(d => d.DateTimeProperty);

			var dummyData = new Mock<IDummyModel>();

			//Act
			UnitUnderTest.Check(dummyData.Object);

			//Assert
			dummyData.Verify(d => d.DateTimeProperty, Times.Once);
		}

		[Test]
		public void TestGivenCorrectDatesTrueIsReturned()
		{
			TestDateGivesCorrectResult(_startDate, _endDate, _startDate, true);
			TestDateGivesCorrectResult(_startDate, _endDate, _endDate, true);
		}

		[Test]
		public void TestGivenWrongDatesFalseIsReturned()
		{
			TestDateGivesCorrectResult(_startDate, _endDate, _startDate - TimeSpan.FromDays(1), false);
			TestDateGivesCorrectResult(_startDate, _endDate, _endDate + TimeSpan.FromDays(1), false);
		}
	}
}