// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Timers;
using CinemaManager.Model;
using CinemaManager.Properties;

namespace CinemaManager
{
	/// <summary>
	///     Session for whole Project
	/// </summary>
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public sealed class Session : INotifyPropertyChanged
	{
		private static string _dataPath;

		private static string _layoutPath;

		private Session()
		{
		}

		public static Session Instance { get; } = new Session();

		public IDataModel DataModel { get; private set; } = new DataModel();

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


		public event PropertyChangedEventHandler PropertyChanged;

		public event EventHandler PrepareForSave;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void OnPrepareForSave()
		{
			PrepareForSave?.Invoke(this, EventArgs.Empty);
		}

		~Session()
		{
			Ticker?.Stop();
			Ticker?.Dispose();
			Ticker = null;
			PrepareForSave = null;
			PropertyChanged = null;
			DataModel = null;
		}
	}
}