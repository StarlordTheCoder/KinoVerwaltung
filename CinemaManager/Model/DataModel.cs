using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager.Model
{
	[Serializable]
	public class DataModel
	{
		[NonSerialized]
		private readonly BinaryFormatter _binaryFormatter;

		[NonSerialized]
		private static string _path = "TODO";

		public CinemaModel[] Cinemas { get; set; }

		public DataModel()
		{
			_binaryFormatter = new BinaryFormatter();
		}

		public void Save()
		{
			//TODO
			_binaryFormatter.Serialize(File.OpenWrite(_path), this);
		}

		public void Load()
		{
			//TODO
			var result = (DataModel) _binaryFormatter.Deserialize(File.OpenWrite(_path));
			Cinemas = result.Cinemas;
		}
	}
}
