// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Windows;
using CinemaManager.Converter;
using NUnit.Framework;

namespace CinemaManagerTest.Converter
{
	public class BooltoVisConverterTest : UnitTestBase<BoolToVisConverter>
	{
		protected override void DoSetup()
		{
			UnitUnderTest = new BoolToVisConverter();
		}

		[TestCase(true)]
		[TestCase(false)]
		public void TestConvert(bool input)
		{
			//Arrange
			UnitUnderTest.True = Visibility.Visible;
			UnitUnderTest.False = Visibility.Hidden;

			//Act
			var result = UnitUnderTest.Convert(input, typeof(Visibility), null, null);

			//Assert
			Assert.That(result, Is.EqualTo(input ? UnitUnderTest.True : UnitUnderTest.False));
		}

		[TestCase(Visibility.Visible, true)]
		[TestCase(Visibility.Hidden, false)]
		[TestCase(Visibility.Collapsed, false)]
		public void TestConvertBack(Visibility input, bool expectedResult)
		{
			//Arrange
			UnitUnderTest.True = Visibility.Visible;
			UnitUnderTest.False = Visibility.Hidden;

			//Act
			var result = UnitUnderTest.ConvertBack(input, typeof(bool), null, null);

			//Assert
			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}