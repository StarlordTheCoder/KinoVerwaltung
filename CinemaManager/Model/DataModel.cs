// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
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
		    try
		    {
		        using (var stream = File.Open(Session.DataPath, FileMode.OpenOrCreate))
		        {
		            stream.SetLength(0);
		            _serializer.Serialize(stream, CinemasModel);
		        }
		    }
		    catch (IOException)
		    {
		        //TODO AUTASVE FAILED (We will never do this)
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
            // Laden der Sitze
            foreach (var cinema in CinemasModel.Cinemas.Where(c => c.SeatTypes.Count == 0))
		    {
                cinema.SeatTypes.Add(new SeatType()
                {
                    Capacity = 1,
                    DisplayName = "Einzelitz",
                    PriceMultiplicator = 1,
                    Id = 1
                });
                cinema.SeatTypes.Add(new SeatType()
                {
                    Capacity = 2,
                    DisplayName = "Sofa",
                    PriceMultiplicator = 1.5,
                    Id = 2
                });
                cinema.SeatTypes.Add(new SeatType()
                {
                    Capacity = 2,
                    DisplayName = "Vip-Sofa",
                    PriceMultiplicator = 2,
                    Id = 3
                });
            }
		}
	}
}