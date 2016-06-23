// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Windows;

namespace CinemaManager.Infrastructure
{
	/// <summary>
	///     Erlaubt das Ausblenden von bestimmten Funktionen im RESERVATIONS-Modus
	/// </summary>
	public static class DebugReleaseStylePicker
	{
		static DebugReleaseStylePicker()
		{
#if RESERVATIONS
			DebugBuildStarElseHidden = new GridLength(0, GridUnitType.Star);
			DebugBuildAutoElseHidden = new GridLength(0, GridUnitType.Star);
#else
			DebugBuildStarElseHidden = new GridLength(1, GridUnitType.Star);
			DebugBuildAutoElseHidden = GridLength.Auto;
#endif
		}

		/// <summary>
		///     Im Debug ist die Breite ein Stern, ansonsten null
		/// </summary>
		public static GridLength DebugBuildStarElseHidden { get; }

		/// <summary>
		///     Im Debug ist die Breite AUTO, ansonsten null
		/// </summary>
		public static GridLength DebugBuildAutoElseHidden { get; }
	}
}