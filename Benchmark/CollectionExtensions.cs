using System;
using System.Collections.Generic;
using System.Linq;

namespace Benchmark
{
	public static class CollectionExtensions
	{
		public static double Median(this IEnumerable<double> source)
		{
			if (source == null || !source.Any())
				throw new ArgumentException("Cannot calculate median unless source contains at least 1 element");

			var sortedList = source.OrderBy(x => x);
			int itemIndex = sortedList.Count() / 2;

			if (sortedList.Count() % 2 == 0)
				return (sortedList.ElementAt(itemIndex) + sortedList.ElementAt(itemIndex - 1)) / 2;

			return sortedList.ElementAt(itemIndex);
		}		
	}
}
