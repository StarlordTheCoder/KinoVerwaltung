// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Linq;
using CinemaManager.Filter;
using CinemaManager.Filter.Date;
using CinemaManager.Filter.String;
using Moq;
using NUnit.Framework;

namespace CinemaManagerTest.Filter
{
	public class FilterConfiguratorTest : UnitTestBase<IFilterConfigurator<IDummyModel>>
	{
		private static IDummyModel EmptyDummyModel => new Mock<IDummyModel>().Object;

		protected override void DoSetup()
		{
			UnitUnderTest = new FilterConfigurator<IDummyModel>();

			base.DoSetup();
		}

		[Test]
		public void GivenCorrectFilterResultReturned()
		{
			//Arrange
			var data = new List<IDummyModel>
			{
				EmptyDummyModel,
				EmptyDummyModel
			};

			var stringFilterMock = new Mock<IStringFilter<IDummyModel>>();
			var dateFilterMock = new Mock<IDateFilter<IDummyModel>>();

			stringFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			stringFilterMock.Setup(f => f.Check(It.IsAny<IDummyModel>())).Returns(() => true);

			dateFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			dateFilterMock.Setup(f => f.Check(It.IsAny<IDummyModel>())).Returns(() => true);

			UnitUnderTest.StringFilter(stringFilterMock.Object);
			UnitUnderTest.DateFilter(dateFilterMock.Object);

			//Act
			var result = UnitUnderTest.FilterData(data);

			//Assert
			Assert.That(result, Is.EquivalentTo(data));
		}

		[Test]
		public void GivenDisabledFilterEmptyNoResultReturned()
		{
			//Arrange
			var data = new List<IDummyModel>
			{
				EmptyDummyModel,
				EmptyDummyModel
			};

			var filterMock = new Mock<IStringFilter<IDummyModel>>();

			filterMock.Setup(f => f.IsEnabled).Returns(() => false);
			filterMock.Setup(f => f.Check(It.IsAny<IDummyModel>())).Returns(() => false);

			UnitUnderTest.StringFilter(filterMock.Object);

			//Act
			var result = UnitUnderTest.FilterData(data);

			//Assert
			Assert.That(result, Is.EquivalentTo(data));
		}

		[Test]
		public void GivenFilterCorrectResultReturned()
		{
			//Arrange

			var leer = EmptyDummyModel;
			var wichtig = new Mock<IDummyModel>();
			wichtig.Setup(d => d.StringProperty).Returns("Wichtiges Dummymodel!");

			var data = new List<IDummyModel>
			{
				leer,
				wichtig.Object
			};

			var filterMock = new Mock<IStringFilter<IDummyModel>>();

			filterMock.Setup(f => f.IsEnabled).Returns(() => true);
			filterMock.Setup(f => f.Check(wichtig.Object)).Returns(() => true);
			filterMock.Setup(f => f.Check(leer)).Returns(() => false);

			UnitUnderTest.StringFilter(filterMock.Object);

			//Act
			var result = UnitUnderTest.FilterData(data).ToList();

			//Assert
			Assert.That(result, Does.Contain(wichtig.Object));
			Assert.That(result, !Does.Contain(leer));
		}

		[Test]
		public void GivenNoFilterResultReturned()
		{
			//Arrange
			var data = new List<IDummyModel>
			{
				EmptyDummyModel,
				EmptyDummyModel
			};

			//Act
			var result = UnitUnderTest.FilterData(data);

			//Assert
			Assert.That(result, Is.EquivalentTo(data));
		}

		[Test]
		public void GivenPartiallyDisabledFilterDisabledFiltersIgnored()
		{
			//Arrange
			var data = new List<IDummyModel>
			{
				EmptyDummyModel,
				EmptyDummyModel
			};

			var stringFilterMock = new Mock<IStringFilter<IDummyModel>>();
			var dateFilterMock = new Mock<IDateFilter<IDummyModel>>();

			stringFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			stringFilterMock.Setup(f => f.Check(It.IsAny<IDummyModel>())).Returns(() => true);

			dateFilterMock.Setup(f => f.IsEnabled).Returns(() => false);
			dateFilterMock.Setup(f => f.Check(It.IsAny<IDummyModel>())).Returns(() => false);

			UnitUnderTest.StringFilter(stringFilterMock.Object);
			UnitUnderTest.DateFilter(dateFilterMock.Object);

			//Act
			var result = UnitUnderTest.FilterData(data);

			//Assert
			Assert.That(result, Is.EquivalentTo(data));

			stringFilterMock.Verify(m => m.Check(It.IsAny<IDummyModel>()), Times.Exactly(2));
			dateFilterMock.Verify(m => m.Check(It.IsAny<IDummyModel>()), Times.Never);
		}

		[Test]
		public void GivenWrongFilterEmptyNoResultReturned()
		{
			//Arrange
			var data = new List<IDummyModel>
			{
				EmptyDummyModel,
				EmptyDummyModel
			};

			var stringFilterMock = new Mock<IStringFilter<IDummyModel>>();
			var dateFilterMock = new Mock<IDateFilter<IDummyModel>>();

			stringFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			stringFilterMock.Setup(f => f.Check(It.IsAny<IDummyModel>())).Returns(() => false);

			dateFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			dateFilterMock.Setup(f => f.Check(It.IsAny<IDummyModel>())).Returns(() => false);

			UnitUnderTest.StringFilter(stringFilterMock.Object);
			UnitUnderTest.DateFilter(dateFilterMock.Object);

			//Act
			var result = UnitUnderTest.FilterData(data);

			//Assert
			Assert.That(result, Is.Empty);
		}
	}
}