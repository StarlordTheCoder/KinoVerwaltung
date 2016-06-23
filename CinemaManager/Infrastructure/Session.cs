// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Timers;
using CinemaManager.Model;
using CinemaManager.Properties;

namespace CinemaManager.Infrastructure
{
	/// <summary>
	///     Session for whole Project
	/// </summary>
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public sealed class Session : NotifyPropertyChangedBase
	{
		private static string _dataPath;

		private static string _layoutPath;

		private Session()
		{
			DataModel = new DataModel();
		}

		/// <summary>
		///     Instanz. Singleton!
		/// </summary>
		public static Session Instance { get; } = new Session();

		/// <summary>
		///     Die Daten dieser Session
		/// </summary>
		public IDataModel DataModel { get; set; }

		/// <summary>
		///     Das ausgewählte Kino. Shortcut für <see cref="CinemasModel.Cinemas"/>, welches <see cref="CinemaModel.IsActive"/> ist.
		/// </summary>
		public CinemaModel SelectedCinemaModel => DataModel.CinemasModel.Cinemas.FirstOrDefault(c => c.IsActive);

		/// <summary>
		///     Global Ticker for Project
		/// </summary>
		public Timer Ticker { get; private set; } = new Timer(TimeSpan.FromSeconds(30).Ticks);

		/// <summary>
		///     Expanded <see cref="Settings.Default" /> DefaultDataPath
		/// </summary>
		public string DataPath
		{
			get { return _dataPath ?? Environment.ExpandEnvironmentVariables(Settings.Default.DefaultDataPath); }
			set
			{
				if (Equals(_dataPath, value)) return;
				_dataPath = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		///     Expanded <see cref="Settings.Default" /> DefaultLayoutPath
		/// </summary>
		public string LayoutPath
		{
			get { return _layoutPath ?? Environment.ExpandEnvironmentVariables(Settings.Default.DefaultLayoutPath); }
			set
			{
				if (Equals(_layoutPath, value)) return;
				_layoutPath = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		///     Gibt einem Objekt Gelegenheit zu dem Versuch, Ressourcen freizugeben und andere Bereinigungen durchzuführen,
		///     bevor es von der Garbage Collection freigegeben wird.
		/// </summary>
		~Session()
		{
			Ticker?.Stop();
			Ticker?.Dispose();
			Ticker = null;
			DataModel = null;
		}
	}
}