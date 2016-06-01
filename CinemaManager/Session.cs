// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Timers;
using CinemaManager.Properties;

namespace CinemaManager
{
	/// <summary>
	///     Session for howl Project
	/// </summary>
	public static class Session
	{
		/// <summary>
		///     Global Ticker for Project
		/// </summary>
		public static Timer Ticker { get; } = new Timer(TimeSpan.FromMinutes(2).Ticks);

		/// <summary>
		///     Expanded <see cref="Settings.Default" /> DataPath
		/// </summary>
		public static string FullDataPath => Environment.ExpandEnvironmentVariables(Settings.Default.DataPath);
	}
}