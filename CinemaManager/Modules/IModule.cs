using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CinemaManager.Modules
{
	public interface IModule : INotifyPropertyChanged
	{
		bool IsVisible { get; set; }
		string Title { get; set; }

		ICommand CloseCommand { get; }
	}
}
