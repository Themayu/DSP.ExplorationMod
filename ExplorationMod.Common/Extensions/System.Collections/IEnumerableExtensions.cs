using System;
using System.Collections.Generic;

namespace Zuris.ExplorationMod.Common.Extensions.System.Collections
{
	public static class IEnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (var item in enumerable)
			{
				action(item);
			}
		}
	}
}
