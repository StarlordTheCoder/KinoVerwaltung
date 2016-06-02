using System.IO;
using System.Windows;
using System.Windows.Input;
using CinemaManager.Model;
using CinemaManager.Properties;
using Microsoft.Win32;

namespace CinemaManager.MainView
{
	public class DataSourceService
	{
		private readonly IDataModel _data;

		public DataSourceService(IDataModel data)
		{
			_data = data;
			LoadData();


			OpenFileCommand = new RoutedUICommand("Open...","Open...", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.O, ModifierKeys.Control)
			});
			RefreshCommand = new RoutedUICommand("Synchronize", "Synchronize", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.S, ModifierKeys.Control)
			});
			SaveFileCommand = new RoutedUICommand("Save as...", "Save as...", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Alt)
			});
		}

		public void SaveDataFile()
		{
			var dialog = new SaveFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's children (*.satanData)|*.satanData",
				InitialDirectory = Path.GetDirectoryName(Session.FullDataPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				if (Settings.Default.DataPath != dialog.FileName)
				{
					Settings.Default.DataPath = dialog.FileName;
					Settings.Default.Save();
				}
				_data.Save();
			}
		}

		public void LoadData()
		{
			LoadData(Session.FullDataPath);
		}

		public void LoadData(string fileName)
		{
			if (Session.FullDataPath != fileName)
			{
				Settings.Default.DataPath = fileName;
				Settings.Default.Save();
			}

			try
			{
				_data.Load();
			}
			catch
			{
				MessageBox.Show("Cannot interpret data!");
			}
		}

		private void OpenDataFile()
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's children (*.satanData)|*.satanData",
				InitialDirectory = Path.GetDirectoryName(Session.FullDataPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				LoadData(dialog.FileName);
			}
		}

		public RoutedUICommand OpenFileCommand { get; }
		public RoutedUICommand RefreshCommand { get; }
		public RoutedUICommand SaveFileCommand { get; }
	}
}
