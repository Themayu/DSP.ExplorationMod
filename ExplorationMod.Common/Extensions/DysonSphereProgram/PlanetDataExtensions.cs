using Zuris.ExplorationMod.Common.Extensions.System;

namespace Zuris.ExplorationMod.Common.Extensions.DysonSphereProgram
{
	public static class PlanetDataExtensions
	{
		/// <summary>
		/// Deletes all factory items from the planet.
		/// </summary>
		/// <param name="planet">The planet.</param>
		public static void DeleteFactory(this PlanetData planet)
		{
			var factory = planet.factory;
			
			factory.entityPool.ForEach(entity => {
				factory.DestructObject(entity.id, out _);
			});
		}
	}
}
