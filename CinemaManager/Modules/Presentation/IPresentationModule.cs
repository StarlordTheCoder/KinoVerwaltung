// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.ObjectModel;

namespace CinemaManager.Modules.Presentation
{
	/// <summary>
	///     Interface for <see cref="PresentationModule" />
	/// </summary>
	public interface IPresentationModule : IModule
	{
		/// <summary>
		///     Alle gefilterten Präsenatationen
		/// </summary>
		ObservableCollection<PresentationViewModel> Presentations { get; }

		/// <summary>
		///     Ausgewählte Präsentation
		/// </summary>
		PresentationViewModel SelectedPresentation { get; set; }

		/// <summary>
		///     Fügt eine neue Präsentation hinzu
		/// </summary>
		void AddPresentation();

		/// <summary>
		///     Entfernt die ausgewählte Präsentation
		/// </summary>
		void RemovePresentation();
	}
}