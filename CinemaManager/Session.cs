using System;
using System.Timers;

namespace CinemaManager
{
	/// <summary>
	/// Session for howl Project
	/// </summary>
	public static class Session
	{
		 /// <summary>
		 /// Global Ticker for Project
		 /// </summary>
		 public static Timer Ticker { get; } = new Timer(TimeSpan.FromMinutes(2).Ticks);

	}
}
