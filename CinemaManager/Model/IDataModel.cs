using System.Collections.Generic;

namespace CinemaManager.Model
{
	public interface IDataModel
	{
		IEnumerable<CinemaModel> Cinemas { get; }

		void Save();

		void Load();
	}
}
