// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.IO;
using System.Linq;
using System.Windows.Shell;
using System.Xml.Serialization;
using CinemaManager.Infrastructure;

namespace CinemaManager.Model
{
	/// <summary>
	///     Implementation von <see cref="IDataModel" /> mithilfe eines <see cref="XmlSerializer" />
	/// </summary>
	public class DataModel : IDataModel
	{
		//XmlSerializer only serializes public properties & fields
		private readonly XmlSerializer _serializer;

		/// <summary>
		///     Initialisiert <see cref="_serializer" />
		/// </summary>
		public DataModel()
		{
			_serializer = new XmlSerializer(typeof(CinemasModel));
		}

		private static Session Session => Session.Instance;

		/// <summary>
		///     The main instance, containing all data from the models.
		/// </summary>
		public CinemasModel CinemasModel { get; set; } = new CinemasModel();

		/// <summary>
		///     Speichert die Daten (<see cref="IDataModel.CinemasModel" />) ab
		/// </summary>
		public void Save()
		{
			var folder = Path.GetDirectoryName(Session.DataPath);
			if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			using (var stream = File.Open(Session.DataPath, FileMode.OpenOrCreate))
			{
				stream.SetLength(0);
				_serializer.Serialize(stream, CinemasModel);
			}
		}

		/// <summary>
		///     Lädt die Daten (<see cref="IDataModel.CinemasModel" />)
		/// </summary>
		public void Load()
		{
			if (File.Exists(Session.DataPath))
			{
				JumpList.AddToRecentCategory(Session.DataPath);

				//Read file
				using (var stream = File.OpenRead(Session.DataPath))
				{
					CinemasModel = (CinemasModel) _serializer.Deserialize(stream);

					ValidateData();
				}
			}
			else
			{
				//Create new file
				Save();
			}
		}

		private void ValidateData()
		{
			if (CinemasModel.Cinemas.Any() && CinemasModel.Cinemas.Count(c => c.IsActive) != 1)
			{
				CinemasModel.Cinemas.ForEach(c => c.IsActive = false);
				CinemasModel.Cinemas.First().IsActive = true;
			}
		}
	}
}