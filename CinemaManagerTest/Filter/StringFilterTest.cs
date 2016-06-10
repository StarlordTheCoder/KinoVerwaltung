// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using CinemaManager.Filter;
using Moq;
using NUnit.Framework;

namespace CinemaManagerTest.Filter
{
	public class StringFilterTest : UnitTestBase<StringFilter<DummyModel>>
	{
		private void Setup(params Func<DummyModel, string>[] setup)
		{
			UnitUnderTest = new StringFilter<DummyModel>(string.Empty, setup);
		}

		[Test]
		public void TestCheckCallsValue()
		{
			//Arrange
			Setup(d => d.StringProperty);

			var dummyData = new Mock<DummyModel>();

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

			var dummyData = new Mock<DummyModel>();
			dummyData.Setup(v => v.StringProperty).Returns("suchtext");

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);

			//Assert
			dummyData.Verify(d => d.StringProperty, Times.Once);
			Assert.That(result, Is.True);
		}

		[Test]
		public void TestCheckReturnsFalse()
		{
			//Arrange
			Setup(d => d.StringProperty);
			UnitUnderTest.Text = "Nicht gefundener Text";

			var dummyData = new Mock<DummyModel>();
			dummyData.Setup(v => v.StringProperty).Returns("Zu filternder Text");

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);

			//Assert
			dummyData.Verify(d => d.StringProperty, Times.Once);
			Assert.That(result, Is.False);
		}

		[Test]
		public void TestCheckWithEmptyTextReturnTrue()
		{
			//Arrange
			Setup(d => d.StringProperty);
			UnitUnderTest.Text = string.Empty;

			var dummyData = new Mock<DummyModel>();
			dummyData.Setup(v => v.StringProperty).Returns("Zu filternder Text");

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);

			//Assert
			dummyData.Verify(d => d.StringProperty, Times.Once);
			Assert.That(result, Is.True);
		}
	}
}