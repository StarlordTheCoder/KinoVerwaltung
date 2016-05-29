using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CinemaManager.Annotations;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules
{
	public class ExampleModule : IModule
	{
		private string _title;
		private bool _isVisible;

		public ExampleModule(bool isVisible, string title)
		{
			IsVisible = isVisible;
			Title = title;
			CloseCommand = new DelegateCommand(() => IsVisible = false);
		}

		public bool IsVisible
		{
			get { return _isVisible; }
			set
			{
				if (value == _isVisible) return;

				_isVisible = value;
				OnPropertyChanged();
			}
		}

		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				OnPropertyChanged();
			}
		}

		public ICommand CloseCommand { get; }


		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
