// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using CinemaManager.Infrastructure;
using CinemaManager.Modules;
using CinemaManager.Modules.Cinema;
using CinemaManager.Modules.Film;
using CinemaManager.Modules.Presentation;
using CinemaManager.Modules.Reservation;
using CinemaManager.Modules.Room;
using CinemaManager.Modules.User;
using CinemaManager.Properties;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.MainView
{
	/// <summary>
	///     Bereitet die Daten für das Hauptfenster vor
	/// </summary>
	public class MainWindowViewModel
	{
		public AutoSaver autosaver;

		public MainWindowViewModel(string startupFile)
		{
			AboutCommand = new DelegateCommand(() => MessageBox.Show(AboutMessage));

			if (!string.IsNullOrEmpty(startupFile))
			{
				var extension = Path.GetExtension(startupFile);

				if (Equals(extension, ".satan"))
				{
					Session.LayoutPath = startupFile;
				}
				else if (Equals(extension, ".satanData"))
				{
					Session.DataPath = startupFile;
				}
				else
				{
					MessageBox.Show($"{extension} isn't a valid file extension!");
				}
			}

			DataSourceService = new DataSourceService();
			LayoutService = new LayoutService();
			autosaver = new AutoSaver();

			InitialiseModules();
			CheckAutoSave();
		}

		public static Session Session => Session.Instance;

		public bool AutoSaveEnabled
		{
			get { return Settings.Default.AutoSaveEnabled; }
			set
			{
				Settings.Default.AutoSaveEnabled = value;
				Settings.Default.Save();
				CheckAutoSave();
			}
		}

		private void CheckAutoSave()
		{
			if (AutoSaveEnabled)
			{
				autosaver.StartSave();
			}
			else
			{
				autosaver.StopSave();
			}
		}

		private void InitialiseModules()
		{
			//Create Module instances
			CinemaModule = new CinemaModule(RefreshModules);
			FilmModule = new FilmModule();
			RoomModule = new RoomModule();
			PresentationModule = new PresentationModule(FilmModule, RoomModule);
			UserModule = new UserModule();
			ReservationModule = new ReservationModule(PresentationModule, UserModule);

			//Add to list
			Modules = new ObservableCollection<IModule>
			{
				CinemaModule,
				FilmModule,
				PresentationModule,
				UserModule,
				ReservationModule,
				RoomModule
			};

			//Load data
			RefreshModules(null);
		}

		private void RefreshModules(IModule except)
		{
			foreach (var module in Modules.Where(m => !Equals(m, except)))
			{
				module.Refresh();
			}
		}

		#region Properties

		public IDataSourceService DataSourceService { get; set; }
		public ILayoutService LayoutService { get; set; }

		#endregion

		#region Modules

		public ObservableCollection<IModule> Modules { get; private set; }

		public CinemaModule CinemaModule { get; private set; }
		public FilmModule FilmModule { get; private set; }
		public PresentationModule PresentationModule { get; private set; }
		public IModule ReservationModule { get; private set; }
		public UserModule UserModule { get; private set; }
		public RoomModule RoomModule { get; private set; }

		#endregion

		#region About

		public ICommand AboutCommand { get; }

		private static readonly string AboutMessage =
			"Created by Seraphin Rihm, Pascal Honegger & Alain Keller" + Environment.NewLine
			+ "Version " + ApplicationVersion;

		private static string ApplicationVersion
		{
			get
			{
				var assembly = Assembly.GetExecutingAssembly();
				var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
				return fvi.FileVersion;
			}
		}

		public ICollection CommandBindings
			=> LayoutService.CommandBindings.Concat(DataSourceService.CommandBindings).ToArray();

		#endregion
	}
}