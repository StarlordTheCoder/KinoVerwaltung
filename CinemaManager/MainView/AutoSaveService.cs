// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using CinemaManager.Infrastructure;

namespace CinemaManager.MainView
{
	/// <summary>
	///     Klasse zum Verwalten des automatischen Speicherns
	/// </summary>
	public class AutoSaveService : IAutoSaveService
	{
		/// <summary>
		///     Starts the ticker if it's not already running
		/// </summary>
		public AutoSaveService()
		{
			Session.Instance.Ticker.Start();
		}

		/// <summary>
		///     Enables the AutoSave
		/// </summary>
		public void EnableAutoSave()
		{
			Session.Instance.Ticker.Elapsed += OnTimerElapsed;
		}

		/// <summary>
		///     Disables the AutoSave
		/// </summary>
		public void DisableAutoSave()
		{
			Session.Instance.Ticker.Elapsed -= OnTimerElapsed;
		}

		private static void OnTimerElapsed(object s, EventArgs e)
		{
			Session.Instance.DataModel.Save();
		}
	}
}