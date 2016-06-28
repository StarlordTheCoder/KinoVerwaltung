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
		private readonly IAutoSaveService _autosaver;

		/// <summary>
		///     Used in XAML
		/// </summary>
		public Session Session => Session.Instance;

		/// <summary>
		///     Starts all tasks so the application can procceed normally
		/// </summary>
		/// <param name="startupFile">Used to load a different layout or file at startup</param>
		public MainWindowViewModel(string startupFile)
		{
			AboutCommand = new DelegateCommand(() => MessageBox.Show(AboutMessage));

			if (!string.IsNullOrEmpty(startupFile))
			{
				var extension = Path.GetExtension(startupFile);

				if (Equals(extension, ".satan"))
				{
					Session.Instance.LayoutPath = startupFile;
				}
				else if (Equals(extension, ".satanData"))
				{
					Session.Instance.DataPath = startupFile;
				}
				else
				{
					MessageBox.Show($"{extension} isn't a valid file extension!");
				}
			}

			DataSourceService = new DataSourceService(RefreshModules);
			LayoutService = new LayoutService();
			_autosaver = new AutoSaveService();

			InitialiseModules();
			CheckAutoSave();


		}

		/// <summary>
		///     Wird serialisiert. Falls true, speichert alle Daten regelmässig ab
		/// </summary>
		public bool AutoSaveEnabled
		{
			get { return Settings.Default.AutoSaveEnabled; }
			set
			{
				if (Settings.Default.AutoSaveEnabled == value) return;
				Settings.Default.AutoSaveEnabled = value;
				Settings.Default.Save();
				CheckAutoSave();
			}
		}

		private void CheckAutoSave()
		{
			if (AutoSaveEnabled)
			{
				_autosaver.EnableAutoSave();
			}
			else
			{
				_autosaver.DisableAutoSave();
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


		/// <summary>
		///     Beim Schliessen wird ein Popup angezeigt welches nach dem Speichern fragt
		/// </summary>
		public void Exit(System.ComponentModel.CancelEventArgs e)
		{
			MessageBoxButton buttons = MessageBoxButton.YesNoCancel;
			string message = "Wollen Sie speichern";
			string caption = "Speichern";
			MessageBoxResult result = MessageBox.Show(message, caption, buttons, MessageBoxImage.Stop); 

			if(result == MessageBoxResult.Yes)
			{
				Session.DataModel.Save();
			} else if(result == MessageBoxResult.Cancel)
			{
				e.Cancel = true;
			}

		}

		private void RefreshModules(IModule except)
		{
			foreach (var module in Modules.Where(m => !Equals(m, except)))
			{
				module.Refresh();
			}
		}

		#region Properties

		/// <summary>
		///     Service zum Verwalten der Datenquelle
		/// </summary>
		public IDataSourceService DataSourceService { get; set; }

		/// <summary>
		///     Service zum Verwalten des Layouts
		/// </summary>
		public ILayoutService LayoutService { get; set; }

		#endregion

		#region Modules

		/// <summary>
		///     Liste aller Module für das Docking-Framework
		/// </summary>
		public ObservableCollection<IModule> Modules { get; private set; }


		/// <summary>
		///     Kino-Modul
		/// </summary>
		public CinemaModule CinemaModule { get; private set; }

		/// <summary>
		///     Film-Modul
		/// </summary>
		public FilmModule FilmModule { get; private set; }

		/// <summary>
		///     Vorstellungs-Modul
		/// </summary>
		public PresentationModule PresentationModule { get; private set; }

		/// <summary>
		///     Reservierungs-Modul
		/// </summary>
		public ReservationModule ReservationModule { get; private set; }

		/// <summary>
		///     Benutzer-Modul
		/// </summary>
		public UserModule UserModule { get; private set; }

		/// <summary>
		///     Saal-Modul
		/// </summary>
		public RoomModule RoomModule { get; private set; }

		#endregion

		#region About

		/// <summary>
		///     Command for displaying the <see cref="AboutMessage" />
		/// </summary>
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

		/// <summary>
		///     Die Command-Binding. Verbindet alle Command mit den dazugehörigen Aktionen
		/// </summary>
		public ICollection CommandBindings
			=> LayoutService.CommandBindings.Concat(DataSourceService.CommandBindings).ToArray();

		#endregion
	}
}