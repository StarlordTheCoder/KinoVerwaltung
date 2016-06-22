// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using CinemaManager.Filter;
using CinemaManager.Model;
using Moq;
using NUnit.Framework;

namespace CinemaManagerTest
{
	[TestFixture, Category("UnitTest")]
	public abstract class UnitTestBase<T>
	{
		[SetUp]
		public void SetUp()
		{
			DoSetup();
		}

		[TearDown]
		public void TearDown()
		{
			DoTearDown();
		}

		protected T UnitUnderTest;

		protected IFilterConfigurator<TDto> CreateTrueFilterConfigurator<TDto>()
		{
			var mock = new Mock<IFilterConfigurator<TDto>>();
			mock.Setup(m => m.FilterData(It.IsAny<IEnumerable<TDto>>())).Returns<IEnumerable<TDto>>(d => d);
			return mock.Object;
		}
		protected IDataModel CreateData(CinemasModel data)
		{
			var mock = new Mock<IDataModel>();
			mock.Setup(m => m.CinemasModel).Returns(data);
			return mock.Object;
		}

		protected virtual void DoSetup()
		{
			//Optional
		}

		protected virtual void DoTearDown()
		{
			//Optional
		}
	}
}