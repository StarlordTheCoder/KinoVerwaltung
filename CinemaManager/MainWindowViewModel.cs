using System.Collections.ObjectModel;
using System.Linq;
using CinemaManager.Modules;
using CinemaManager.Modules.Cinema;
using CinemaManager.Modules.User;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace CinemaManager
{
	public class MainWindowViewModel
	{
		private readonly XmlLayoutSerializer _layoutSerializer;

		public MainWindowViewModel(DockingManager dockManager)
		{
			LoadLayoutCommand = new DelegateCommand(LoadLayout);
			SaveLayoutCommand = new DelegateCommand(SaveLayout);

			_layoutSerializer = new XmlLayoutSerializer(dockManager);

			Modules = new ObservableCollection<IModule> {CinemaModule, UserModule};
		}

		#region Modules

		public IModule UserModule { get; } = new UserModule();
		public IModule CinemaModule { get; } = new CinemaModule();

		#endregion

		#region Layout

		private void LoadLayout()
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's children (*.satan)|*.satan"
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				_layoutSerializer.LayoutSerializationCallback += (sender, e) =>
				{
					var module = Modules.First(m => m.Title == e.Model.Title);

					//TODO

					e.Cancel = true;
				};

				_layoutSerializer.Deserialize(dialog.FileName);
			}
		}

		private void SaveLayout()
		{
			var dialog = new SaveFileDialog
			{
				DefaultExt = ".satan",
				Filter = "Satan's children (*.satan)|*.satan"
			};

			var result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				_layoutSerializer.Serialize(dialog.FileName);
			}
		}

		public DelegateCommand LoadLayoutCommand { get; }

		public DelegateCommand SaveLayoutCommand { get; }

		public ObservableCollection<IModule> Modules { get; }

		#endregion
	}
}