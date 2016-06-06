// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Windows.Media;
using CinemaManager.Converter;
using NUnit.Framework;

namespace CinemaManagerTest.Converter
{
	public class BoolToSolidColorBrushConverterTest : UnitTestBase<BoolToSolidColorBrushConverter>
	{
		protected override void DoSetup()
		{
			UnitUnderTest = new BoolToSolidColorBrushConverter();
			base.DoSetup();
		}

		[TestCase(true)]
		[TestCase(false)]
		public void TestConvert(bool input)
		{
			//Arrange
			UnitUnderTest.True = Colors.Green;
			UnitUnderTest.False = Colors.Red;

			//Act
			var result = (SolidColorBrush) UnitUnderTest.Convert(input, typeof(Color), null, null);

			//Assert
			Assert.That(result.Color, Is.EqualTo(input ? UnitUnderTest.True : UnitUnderTest.False));
		}
	}
}