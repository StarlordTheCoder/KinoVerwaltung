// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using CinemaManager.Filter.Number;
using Moq;
using NUnit.Framework;

namespace CinemaManagerTest.Filter
{
	public class NumberFilterTest : UnitTestBase<NumberFilter<IDummyModel>>
	{
		private void Setup(params Func<IDummyModel, int>[] setup)
		{
			UnitUnderTest = new NumberFilter<IDummyModel>(string.Empty, setup);
		}

		[Test]
		public void TestChangeValueCallsFilterChanged()
		{
			//Arrange
			Setup();
			var eventCalled = false;

			UnitUnderTest.FilterChanged += (sender, e) => eventCalled = true;

			//Act
			UnitUnderTest.Number = 12;

			//Assert
			Assert.That(eventCalled);
		}

		[Test]
		public void TestCheckCallsValue()
		{
			//Arrange
			Setup(d => d.NumberProperty);
			UnitUnderTest.Number = 12;

			var dummyData = new Mock<IDummyModel>();

			//Act
			UnitUnderTest.Check(dummyData.Object);

			//Assert
			dummyData.Verify(d => d.NumberProperty, Times.Once);
		}

		[Test]
		public void TestCheckIgnoresCase()
		{
			//Arrange
			Setup(d => d.NumberProperty);
			UnitUnderTest.Number = 12;

			var dummyData = new Mock<IDummyModel>();
			dummyData.Setup(v => v.NumberProperty).Returns(12);

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);

			//Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void TestCheckReturnsFalse()
		{
			//Arrange
			Setup(d => d.NumberProperty);
			UnitUnderTest.Number = 12;

			var dummyData = new Mock<IDummyModel>();
			dummyData.Setup(v => v.NumberProperty).Returns(42);

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);

			//Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void TestCheckWithEmptyTextReturnTrue()
		{
			//Arrange
			Setup(d => d.NumberProperty);
			UnitUnderTest.Number = null;

			var dummyData = new Mock<IDummyModel>();
			dummyData.Setup(v => v.NumberProperty).Returns(12);

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);

			//Assert
			Assert.That(result, Is.True);
		}
	}
}