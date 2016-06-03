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
using CinemaManager.Model;
using CinemaManager.Modules;
using CinemaManager.Modules.Cinema;
using CinemaManager.Modules.User;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.MainView
{
	public class MainWindowViewModel
	{
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

			Modules = new ObservableCollection<IModule>
			{
				CinemaModule,
				MovieModule,
				PresentationModule,
				ReservationModule,
				UserModule
			};
		}

		public static Session Session => Session.Instance;

		#region Properties

		public IDataSourceService DataSourceService { get; set; }
		public ILayoutService LayoutService { get; set; }

		#endregion

		#region Modules

		public ObservableCollection<IModule> Modules { get; }

		public IModule CinemaModule { get; } = new CinemaModule();
		public IModule MovieModule { get; }
		public IModule PresentationModule { get; }
		public IModule ReservationModule { get; }
		public IModule UserModule { get; } = new UserModule();

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