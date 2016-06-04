// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace CinemaManager.MainView
{
	public class LayoutService : ILayoutService
	{
		private DockingManager _dockingManager;

		public LayoutService()
		{
			OpenLayoutCommand = new RoutedUICommand("Open...", "Open...", typeof(MainWindow), new InputGestureCollection
			{
				new KeyGesture(Key.O, ModifierKeys.Alt)
			});
			SaveAsLayoutCommand = new RoutedUICommand("Save as...", "Save as...", typeof(MainWindow),
				new InputGestureCollection
				{
					new KeyGesture(Key.S, ModifierKeys.Alt | ModifierKeys.Shift)
				});
			SaveLayoutCommand = new RoutedUICommand("Save", "Save", typeof(MainWindow),
				new InputGestureCollection
				{
					new KeyGesture(Key.S, ModifierKeys.Alt)
				});

			CommandBindings = new List<CommandBinding>
			{
				new CommandBinding(OpenLayoutCommand, (sender, e) => LoadLayoutFile()),
				new CommandBinding(SaveAsLayoutCommand, (sender, e) => SaveLayoutFile()),
				new CommandBinding(SaveLayoutCommand, (sender, e) => SaveLayout(Session.LayoutPath)),
			};
		}

		private static Session Session => Session.Instance;

		public RoutedUICommand OpenLayoutCommand { get; }
		public RoutedUICommand SaveAsLayoutCommand { get; }
		public RoutedUICommand SaveLayoutCommand { get; }
		public IList<CommandBinding> CommandBindings { get; }

		public void Initialize(DockingManager dockingManager)
		{
			_dockingManager = dockingManager;

			LoadLayout(Session.LayoutPath);
		}

		private void LoadLayout(string path)
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
	}
}