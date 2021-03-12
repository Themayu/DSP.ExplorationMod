using System;
using System.Reflection;

namespace ExplorationMod.Common.Extensions.System
{
	public static class ObjectExtensions
	{
		public static Action<object[]> GetDelegate(this object source, string eventName)
		{
			var eventDelegate =
				(MulticastDelegate?) source.GetType()
				                           .GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic)?
				                           .GetValue(source);

			return args => {
				eventDelegate?.GetInvocationList().ForEach(handler => {
					handler.Method.Invoke(handler.Target, args);
				});
			};
		}
		
		public static T With<T>(this T context, Action<T> action)
		{
			action(context);

			return context;
		}
	}
}
