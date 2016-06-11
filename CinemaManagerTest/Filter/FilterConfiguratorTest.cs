// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Linq;
using CinemaManager.Filter;
using Moq;
using NUnit.Framework;

namespace CinemaManagerTest.Filter
{
	public class FilterConfiguratorTest : UnitTestBase<FilterConfigurator<IDummyModel>>
	{
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
				new DummyModel(),
				new DummyModel()
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
			var data = new List<DummyModel>
			{
				new DummyModel(),
				new DummyModel()
			};

			var stringFilterMock = new Mock<IStringFilter<IDummyModel>>();
			var dateFilterMock = new Mock<IDateFilter<IDummyModel>>();

			stringFilterMock.Setup(f => f.IsEnabled).Returns(() => false);
			stringFilterMock.Setup(f => f.Check(It.IsAny<IDummyModel>())).Returns(() => false);

			dateFilterMock.Setup(f => f.IsEnabled).Returns(() => false);
			dateFilterMock.Setup(f => f.Check(It.IsAny<IDummyModel>())).Returns(() => false);

			UnitUnderTest.StringFilter(stringFilterMock.Object);
			UnitUnderTest.DateFilter(dateFilterMock.Object);

			//Act
			var result = UnitUnderTest.FilterData(data);

			//Assert
			Assert.That(result, Is.EquivalentTo(data));
		}

		[Test]
		public void GivenFilterCorrectResultReturned()
		{
			//Arrange

			var leer = new DummyModel();
			var wichtig = new DummyModel
			{
				StringProperty = "Wichtiges Dummymodel!"
			};

			var data = new List<DummyModel>
			{
				leer,
				wichtig
			};

			var stringFilterMock = new Mock<IStringFilter<IDummyModel>>();
			var dateFilterMock = new Mock<IDateFilter<IDummyModel>>();

			stringFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			stringFilterMock.Setup(f => f.Check(It.IsAny<IDummyModel>())).Returns(() => true);

			dateFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			dateFilterMock.Setup(f => f.Check(wichtig)).Returns(() => true);
			dateFilterMock.Setup(f => f.Check(leer)).Returns(() => false);

			UnitUnderTest.StringFilter(stringFilterMock.Object);
			UnitUnderTest.DateFilter(dateFilterMock.Object);

			//Act
			var result = UnitUnderTest.FilterData(data).ToList();

			//Assert
			Assert.That(result, Does.Contain(wichtig));
			Assert.That(result, !Does.Contain(leer));
		}

		[Test]
		public void GivenNoFilterResultReturned()
		{
			//Arrange
			var data = new List<IDummyModel>
			{
				new DummyModel(),
				new DummyModel()
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
			var data = new List<DummyModel>
			{
				new DummyModel(),
				new DummyModel()
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
				new DummyModel(),
				new DummyModel()
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