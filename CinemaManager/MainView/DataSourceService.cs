// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using Microsoft.Win32;

namespace CinemaManager.MainView
{
	/// <summary>
	///     Managed die momentane Dateiquelle.
	/// </summary>
	public class DataSourceService : IDataSourceService
	{
		/// <summary>
		///     Loads the default data
		///     Creates the Command and CommandBindings
		/// </summary>
		public DataSourceService()
		{
			LoadData(Session.DataPath);

			OpenFileCommand = new RoutedUICommand("Open...", "Open...", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.O, ModifierKeys.Control)
			});
			SynchronizeCommand = new RoutedUICommand("Synchronize", "Synchronize", typeof(MainWindow),
				new InputGestureCollection
				{
					new KeyGesture(Key.S, ModifierKeys.Control)
				});
			SaveFileCommand = new RoutedUICommand("Save as...", "Save as...", typeof(MainWindow),
				new InputGestureCollection
				{
					new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift)
				});

			CommandBindings = new List<CommandBinding>
			{
				new CommandBinding(OpenFileCommand, (sender, e) => OpenDataFile()),
				new CommandBinding(SynchronizeCommand, (sender, e) => SynchronizeData()),
				new CommandBinding(SaveFileCommand, (sender, e) => SaveDataFile())
			};
		}

		private static Session Session => Session.Instance;

		/// <summary>
		///     List of the used <see cref="IDataSourceService.CommandBindings" />
		///     Binds the commands to the associated actions
		/// </summary>
		public IList<CommandBinding> CommandBindings { get; }

		/// <summary>
		///     Command to open a file
		/// </summary>
		public RoutedUICommand OpenFileCommand { get; }

		/// <summary>
		///     Command to save the current file and reload it
		/// </summary>
		public RoutedUICommand SynchronizeCommand { get; }

		/// <summary>
		///     Command to save a file (as)
		/// </summary>
		public RoutedUICommand SaveFileCommand { get; }

		/// <summary>
		///     Try to load the Datafile at the specified <paramref name="path" />/>
		/// </summary>
		/// <param name="path">Path to load data from</param>
		private static void LoadData(string path)
		{
			Session.DataPath = path;

			try
			{
				Session.DataModel.Load();
			}
			catch
			{
				MessageBox.Show("Cannot interpret data!");
			}
		}

		/// <summary>
		///     Opens a save dialog
		/// </summary>
		private static void SaveDataFile()
		{
			var dialog = new SaveFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's children (*.satanData)|*.satanData",
				InitialDirectory = Path.GetDirectoryName(Session.DataPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				Session.DataPath = dialog.FileName;
				Session.DataModel.Save();
			}
		}

		/// <summary>
		///     Calls Load and Save on the <see cref="IDataModel" />
		/// </summary>
		private static void SynchronizeData()
		{
			Session.DataModel.Save();
			Session.DataModel.Load();
		}

		/// <summary>
		///     Opens a open file dialog
		/// </summary>
		private static void OpenDataFile()
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's children (*.satanData)|*.satanData",
				InitialDirectory = Path.GetDirectoryName(Session.DataPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				LoadData(dialog.FileName);
			}
		}
	}
}