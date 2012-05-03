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

			var sorted = source.OrderBy(x => x).ToArray();
			int itemIndex = sorted.Length / 2;

			if (sorted.Length % 2 == 0)
				return (sorted[itemIndex] + sorted[itemIndex - 1]) / 2;

			return sorted[itemIndex];
		}		
	}
}
