// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using CinemaManager.Filter.String;
using Moq;
using NUnit.Framework;

namespace CinemaManagerTest.Filter
{
	public class StringFilterTest : UnitTestBase<StringFilter<IDummyModel>>
	{
		private void Setup(params Func<IDummyModel, string>[] setup)
		{
			UnitUnderTest = new StringFilter<IDummyModel>(string.Empty, setup);
		}

		[Test]
		public void TestChangeValueCallsFilterChanged()
		{
			//Arrange
			Setup();
			var eventCalled = false;

			UnitUnderTest.FilterChanged += (sender, e) => eventCalled = true;

			//Act
			UnitUnderTest.Text = "Neuer Wert";

			//Assert
			Assert.That(eventCalled);
		}

		[Test]
		public void TestCheckCallsValue()
		{
			//Arrange
			Setup(d => d.StringProperty);

			var dummyData = new Mock<IDummyModel>();

			//Act
			UnitUnderTest.Check(dummyData.Object);

			//Assert
			dummyData.Verify(d => d.StringProperty, Times.Once);
		}

		[Test]
		public void TestCheckIgnoresCase()
		{
			//Arrange
			Setup(d => d.StringProperty);
			UnitUnderTest.Text = "UchTeX";

			var dummyData = new Mock<IDummyModel>();
			dummyData.Setup(v => v.StringProperty).Returns("suchtext");

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);

			//Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void TestCheckReturnsFalse()
		{
			//Arrange
			Setup(d => d.StringProperty);
			UnitUnderTest.Text = "Nicht gefundener Text";

			var dummyData = new Mock<IDummyModel>();
			dummyData.Setup(v => v.StringProperty).Returns("Zu filternder Text");

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);

			//Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void TestCheckWithEmptyTextReturnTrue()
		{
			//Arrange
			Setup(d => d.StringProperty);
			UnitUnderTest.Text = string.Empty;

			var dummyData = new Mock<IDummyModel>();
			dummyData.Setup(v => v.StringProperty).Returns("Zu filternder Text");

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);

			//Assert
			Assert.That(result, Is.True);
		}
	}
}