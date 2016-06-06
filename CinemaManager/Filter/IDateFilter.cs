// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;

namespace CinemaManager.Filter
{
	public interface IDateFilter<T> : IFilter<T>
	{
		DateTime? Date { get; set; }

		bool Check(T data);
	}
}