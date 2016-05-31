using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using CinemaManager.Properties;

namespace CinemaManager.Model
{
	[Serializable]
	public class DataModel : IDataModel
	{
		[NonSerialized]
		private readonly XmlSerializer _serializer;

		public IEnumerable<CinemaModel> Cinemas { get; private set; }

		public DataModel()
		{
			_serializer = new XmlSerializer(typeof(DataModel));
			Load();
		}

		public void Save()
		{
			_serializer.Serialize(File.OpenWrite(Settings.Default.DataPath), this);
		}

		public void Load()
		{
			var result = (DataModel) _serializer.Deserialize(File.OpenWrite(Settings.Default.DataPath));
			Cinemas = result.Cinemas;
		}
	}
}
