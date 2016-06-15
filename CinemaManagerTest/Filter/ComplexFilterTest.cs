// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using CinemaManager.Filter;
using CinemaManager.Filter.Complex;
using Moq;
using NUnit.Framework;

namespace CinemaManagerTest.Filter
{
	public class ComplexFilterTest : UnitTestBase<ComplexFilter<IDummyModel, IDummyModule>>
	{
		private void Setup(IDummyModule moduleMock, Func<IDummyModule, IEnumerable<IDummyModel>> setup)
		{
			UnitUnderTest = new ComplexFilter<IDummyModel, IDummyModule>(string.Empty, moduleMock, setup);
		}

		[Test]
		public void TestCheckCallsValue()
		{
			//Arrange
			var moduleMock = new Mock<IDummyModule>();

			Setup(moduleMock.Object, d => d.ExampleList);

			var dummyData = new Mock<IDummyModel>();

			//Act
			UnitUnderTest.Check(dummyData.Object);

			//Assert
			moduleMock.Verify(d => d.ExampleList, Times.Once);
		}

		[Test]
		public void TestCheckReturnsFalse()
		{
			//Arrange
			var moduleMock = new Mock<IDummyModule>();

			var dummyData = new Mock<IDummyModel>();
			var otherDummyData = new Mock<IDummyModel>();

			moduleMock.Setup(m => m.ExampleList).Returns(new List<IDummyModel>
			{
				otherDummyData.Object
			});

			Setup(moduleMock.Object, d => d.ExampleList);

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);

			//Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void TestCheckReturnsTrue()
		{
			//Arrange
			var moduleMock = new Mock<IDummyModule>();

			var dummyData = new Mock<IDummyModel>();

			moduleMock.Setup(m => m.ExampleList).Returns(new List<IDummyModel>
			{
				dummyData.Object
			});

			Setup(moduleMock.Object, d => d.ExampleList);

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);

			//Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void TestCheckWithEmptyTextReturnTrue()
		{
			//Arrange
			var moduleMock = new Mock<IDummyModule>();

			var dummyData = new Mock<IDummyModel>();

			moduleMock.Setup(m => m.ExampleList).Returns(new List<IDummyModel>());

			Setup(moduleMock.Object, d => d.ExampleList);

			//Act
			var result = UnitUnderTest.Check(dummyData.Object);

			//Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void TestModuleChangedThrowsFilterChanged()
		{
			//Arrange
			var moduleMock = new Mock<IDummyModule>();

			var dummyData = new Mock<IDummyModel>();

			Setup(moduleMock.Object, d => d.ExampleList);
			var eventCalled = false;

			UnitUnderTest.FilterChanged += (sender, e) => eventCalled = true;

			//Act
			moduleMock.Raise(m => m.ModuleDataChanged += null, EventArgs.Empty);

			//Assert
			Assert.That(eventCalled);
		}
	}
}