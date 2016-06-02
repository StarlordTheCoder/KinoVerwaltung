using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CinemaManager.Properties;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace CinemaManager.MainView
{
	public class LayoutService
	{
		private DockingManager _dockingManager;
		private static Session Session => Session.Instance;

		public LayoutService()
		{
			OpenLayoutCommand = new RoutedUICommand("Open...", "Open...", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.L, ModifierKeys.Control | ModifierKeys.Shift)
			});
			SaveAsLayoutCommand = new RoutedUICommand("Save as...", "Save as...", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt)
			});

			CommandBindings = new List<CommandBinding>
			{
				new CommandBinding(OpenLayoutCommand, (sender, e) => LoadLayoutFile()),
				new CommandBinding(SaveAsLayoutCommand, (sender, e) => SaveLayoutFile())
			};
		}

		public void Initialize(DockingManager dockingManager)
		{
			_dockingManager = dockingManager;

			LoadLayout(Session.LayoutPath);
		}

		public void LoadLayout(string path)
		{
			Session.LayoutPath = path;

			var folder = Path.GetDirectoryName(Session.LayoutPath);
			if (folder != null && !Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			if (File.Exists(path))
			{
				try
				{
					using (var stream = File.Open(Session.LayoutPath, FileMode.OpenOrCreate))
					{
						new XmlLayoutSerializer(_dockingManager).Deserialize(stream);
					}
				}
				catch (Exception)
				{
					MessageBox.Show("Error loading layout!");
				}
			}
			else
			{
				SaveLayout(path);
			}
		}

		private void LoadLayoutFile()
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's layout (*.satan)|*.satan",
				InitialDirectory = Path.GetDirectoryName(Session.LayoutPath)
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
				InitialDirectory = Path.GetDirectoryName(Session.LayoutPath)
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				SaveLayout(dialog.FileName);
			}
		}

		private void SaveLayout(string path)
		{
			new XmlLayoutSerializer(_dockingManager).Serialize(path);
		}

		public RoutedUICommand OpenLayoutCommand { get; }
		public RoutedUICommand SaveAsLayoutCommand { get; }
		public IList<CommandBinding> CommandBindings { get; }
	}
}
