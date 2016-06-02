// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using CinemaManager.Model;
using CinemaManager.Modules;
using CinemaManager.Modules.Cinema;
using CinemaManager.Modules.User;
using CinemaManager.Properties;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace CinemaManager.MainView
{
	public class MainWindowViewModel
	{
		private readonly DockingManager _dockingManager;

		public MainWindowViewModel(DockingManager dockingManager)
		{
			LoadLayoutCommand = new DelegateCommand(LoadLayoutFile);
			SaveLayoutCommand = new DelegateCommand(SaveLayoutFile);
			AboutCommand = new DelegateCommand(() => MessageBox.Show(AboutMessage));

			DataSourceServce = new DataSourceService(new DataModel());

			_dockingManager = dockingManager;
			LoadLayout();

			Modules = new ObservableCollection<IModule> {CinemaModule, UserModule};
		}

		#region Modules

		public IModule CinemaModule { get; } = new CinemaModule();
		public IModule MovieModule { get; }
		public IModule PresentationModule { get; }
		public IModule ReservationModule { get; }
		public IModule UserModule { get; } = new UserModule();

		#endregion

		#region Layout

		private void LoadLayout(string path)
		{
			if (Session.FullLayoutPath != path)
			{
				Settings.Default.LayoutPath = path;
				Settings.Default.Save();
			}

			var folder = Path.GetDirectoryName(Session.FullDataPath);
			if (folder != null && !Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			try
			{
				using (var stream = File.Open(Session.FullDataPath, FileMode.OpenOrCreate))
				{
					new XmlLayoutSerializer(_dockingManager).Serialize(stream);
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Error loading layout!");
			}
		}

		private void LoadLayout()
		{
			LoadLayout(Session.FullLayoutPath);
		}

		private void LoadLayoutFile()
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's layout (*.satan)|*.satan",
				InitialDirectory = Path.GetDirectoryName(Session.FullLayoutPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				LoadLayout(dialog.FileName);
			}
		}

		private void SaveLayoutFile()
		{
			var dialog = new SaveFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's layout (*.satan)|*.satan",
				InitialDirectory = Path.GetDirectoryName(Session.FullLayoutPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				new XmlLayoutSerializer(_dockingManager).Serialize(dialog.FileName);
			}
		}

		public ICommand LoadLayoutCommand { get; }
		public ICommand SaveLayoutCommand { get; }

		public ObservableCollection<IModule> Modules { get; }

		#endregion


		#region About

		public ICommand AboutCommand { get; }

		private static readonly string AboutMessage =
			"Created by Seraphin Rihm, Pascal Honegger & Alain Keller" + Environment.NewLine
			+ "Version " + ApplicationVersion;

		public DataSourceService DataSourceServce { get; set; }

		private static string ApplicationVersion
		{
			get
			{
				var assembly = Assembly.GetExecutingAssembly();
				var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
				return fvi.FileVersion;
			}
		}

		#endregion

		public void LoadFile(string startupFile)
		{
			if (!string.IsNullOrEmpty(startupFile))
			{
				var extension = Path.GetExtension(startupFile);

				if (Equals(extension, ".satan"))
				{
					LoadLayout(startupFile);
				}
				else if (Equals(extension, ".satanData"))
				{
					DataSourceServce.LoadData(startupFile);
				}
				else
				{
					MessageBox.Show($"{extension} isn't a valid file extension!");
				}
			}
		}
	}
}