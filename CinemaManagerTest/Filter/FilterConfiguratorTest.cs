// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using CinemaManager.Filter;
using Moq;
using NUnit.Framework;

namespace CinemaManagerTest.Filter
{
	public class FilterConfiguratorTest : UnitTestBase<FilterConfigurator<DummyModel>>
	{
		protected override void DoSetup()
		{
			UnitUnderTest = new FilterConfigurator<DummyModel>();

			base.DoSetup();
		}

		[Test]
		public void WhenGivenCorrectStringResultReturned()
		{
			//Arrange
			var data = new List<DummyModel>
			{
				new DummyModel(string.Empty, null)
			};

			var filterMock = new Mock<IStringFilter<DummyModel>>();

			filterMock.Setup(f => f.IsEnabled).Returns(() => true);
			filterMock.As<IFilter<DummyModel>>().Setup(f => f.Check(It.IsAny<DummyModel>())).Returns(() => true);

			UnitUnderTest.StringFilter(filterMock.Object);

			//Act
			var result = UnitUnderTest.FilterData(data);

			//Assert
			Assert.That(result, Is.EquivalentTo(data));
		}

		[Test]
		public void WhenGivenWrongStringEmptyNoResultReturned()
		{
			//Arrange
			var data = new List<DummyModel>
			{
				new DummyModel(string.Empty, null)
			};

			var filterMock = new Mock<IStringFilter<DummyModel>>();

			filterMock.Setup(f => f.IsEnabled).Returns(() => true);
			filterMock.As<IFilter<DummyModel>>().Setup(f => f.Check(It.IsAny<DummyModel>())).Returns(() => false);

			UnitUnderTest.StringFilter(filterMock.Object);

			//Act
			var result = UnitUnderTest.FilterData(data);

			//Assert
			Assert.That(result, Is.Empty);
		}
	}

	public class DummyModel
	{
		public DummyModel(string stringProperty, DateTime? dateTimeProperty)
		{
			StringProperty = stringProperty;
			DateTimeProperty = dateTimeProperty;
		}

		public string StringProperty { get; set; }
		public DateTime? DateTimeProperty { get; set; }
	}
}