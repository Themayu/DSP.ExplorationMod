using System;

namespace Zuris.ExplorationMod.Common.Extensions.System
{
	public static class ArrayExtensions
	{
		public static void ForEach<T>(this T[] array, Action<T> action)
		{
			Array.ForEach(array, action);
		}
	}
}
