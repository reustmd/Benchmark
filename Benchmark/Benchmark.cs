using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Benchmark
{
	public class Benchmark
	{
		public static TimeSpan MeasureExecutionTime(Func<int> func)
		{
			var stopwatch = Stopwatch.StartNew();
			func();
			return stopwatch.Elapsed;
		}

		public static void ExecuteAndPrintExecutionTimeToConsole(Action action)
		{
			var executionTime = ExecuteAndReturnExecutionTime(action);
			Console.WriteLine(string.Format("Action took {0} seconds", executionTime.TotalSeconds));
		}

		public static TimeSpan ExecuteAndReturnExecutionTime(Action action)
		{
			var timer = new Stopwatch();
			timer.Start();

			action.Invoke();

			timer.Stop();
			return timer.Elapsed;
		}

		public static IEnumerable<TimeSpan> ExecuteMultipleTimesAndReturnTimeSpans(Action action, int numRuns, Action actionToPerformEachIteration = null)
		{
			var timeSpans = new List<TimeSpan>();

			for (int i = 0; i < numRuns; i++)
			{
				timeSpans.Add(ExecuteAndReturnExecutionTime(action));
				if (actionToPerformEachIteration != null) actionToPerformEachIteration.Invoke();
			}

			return timeSpans;
		}

		public static void ExecuteMultipleTimesAndPrintSummaryToConsole(Action action, int numRuns)
		{
			int runNumber = 0;
			var runTimes = ExecuteMultipleTimesAndReturnTimeSpans(action, numRuns, () => Console.WriteLine(string.Format("Run {0} completed", ++runNumber)))
								.Select(x => x.TotalSeconds);

			var summary = new MultipleRunSummary { AverageRuntimeInSeconds = runTimes.Average(), MedianRuntimeInSeconds = runTimes.Median() };
			Console.WriteLine("Action executed {0} times, average runtime in seconds: {1}, median runtime in seconds {2}", numRuns, summary.AverageRuntimeInSeconds, summary.MedianRuntimeInSeconds);
		}

		private class MultipleRunSummary
		{
			public double AverageRuntimeInSeconds { get; set; }
			public double MedianRuntimeInSeconds { get; set; }
		}
	}
}
