// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Filter
{
	/// <summary>
	/// Interface for every Filter
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IFilter<in T>
	{
		bool IsEnabled { get; set; }

		string Label { get; }

		bool Check(T data);

		event EventHandler FilterChangedEvent;
	}
}