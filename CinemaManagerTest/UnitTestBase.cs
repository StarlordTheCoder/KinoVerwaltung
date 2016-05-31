﻿// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

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