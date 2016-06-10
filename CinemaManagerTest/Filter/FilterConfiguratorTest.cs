// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Linq;
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
		public void GivenCorrectFilterResultReturned()
		{
			//Arrange
			var data = new List<DummyModel>
			{
				new DummyModel(),
				new DummyModel()
			};

			var stringFilterMock = new Mock<IStringFilter<DummyModel>>();
			var dateFilterMock = new Mock<IDateFilter<DummyModel>>();

			stringFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			stringFilterMock.Setup(f => f.Check(It.IsAny<DummyModel>())).Returns(() => true);

			dateFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			dateFilterMock.Setup(f => f.Check(It.IsAny<DummyModel>())).Returns(() => true);

			UnitUnderTest.StringFilter(stringFilterMock.Object);
			UnitUnderTest.DateFilter(dateFilterMock.Object);

			//Act
			var result = UnitUnderTest.FilterData(data);

			//Assert
			Assert.That(result, Is.EquivalentTo(data));
		}

		[Test]
		public void GivenWrongFilterEmptyNoResultReturned()
		{
			//Arrange
			var data = new List<DummyModel>
			{
				new DummyModel(),
				new DummyModel()
			};

			var stringFilterMock = new Mock<IStringFilter<DummyModel>>();
			var dateFilterMock = new Mock<IDateFilter<DummyModel>>();

			stringFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			stringFilterMock.Setup(f => f.Check(It.IsAny<DummyModel>())).Returns(() => false);

			dateFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			dateFilterMock.Setup(f => f.Check(It.IsAny<DummyModel>())).Returns(() => false);

			UnitUnderTest.StringFilter(stringFilterMock.Object);
			UnitUnderTest.DateFilter(dateFilterMock.Object);

			//Act
			var result = UnitUnderTest.FilterData(data);

			//Assert
			Assert.That(result, Is.Empty);
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

			var stringFilterMock = new Mock<IStringFilter<DummyModel>>();
			var dateFilterMock = new Mock<IDateFilter<DummyModel>>();

			stringFilterMock.Setup(f => f.IsEnabled).Returns(() => false);
			stringFilterMock.Setup(f => f.Check(It.IsAny<DummyModel>())).Returns(() => false);

			dateFilterMock.Setup(f => f.IsEnabled).Returns(() => false);
			dateFilterMock.Setup(f => f.Check(It.IsAny<DummyModel>())).Returns(() => false);

			UnitUnderTest.StringFilter(stringFilterMock.Object);
			UnitUnderTest.DateFilter(dateFilterMock.Object);

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

			var stringFilterMock = new Mock<IStringFilter<DummyModel>>();
			var dateFilterMock = new Mock<IDateFilter<DummyModel>>();

			stringFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			stringFilterMock.Setup(f => f.Check(It.IsAny<DummyModel>())).Returns(() => true);

			dateFilterMock.Setup(f => f.IsEnabled).Returns(() => false);
			dateFilterMock.Setup(f => f.Check(It.IsAny<DummyModel>())).Returns(() => false);

			UnitUnderTest.StringFilter(stringFilterMock.Object);
			UnitUnderTest.DateFilter(dateFilterMock.Object);

			//Act
			var result = UnitUnderTest.FilterData(data);

			//Assert
			Assert.That(result, Is.EquivalentTo(data));

			stringFilterMock.Verify(m => m.Check(It.IsAny<DummyModel>()), Times.Exactly(2));
			dateFilterMock.Verify(m => m.Check(It.IsAny<DummyModel>()), Times.Never);
		}

		[Test]
		public void GivenNoFilterResultReturned()
		{
			//Arrange
			var data = new List<DummyModel>
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

			var stringFilterMock = new Mock<IStringFilter<DummyModel>>();
			var dateFilterMock = new Mock<IDateFilter<DummyModel>>();

			stringFilterMock.Setup(f => f.IsEnabled).Returns(() => true);
			stringFilterMock.Setup(f => f.Check(It.IsAny<DummyModel>())).Returns(() => true);

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
	}
}