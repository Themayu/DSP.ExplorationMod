using System.Linq;
using UnityEngine;
using Zuris.ExplorationMod.Common.Extensions.System.Collections;

namespace Zuris.ExplorationMod.Common.Extensions.DysonSphereProgram
{
	public static class StarDataExtensions
	{
		public static void DeletePlanet(this StarData star, int planetId, bool updateChildren)
		{
			var target = star.planets.FirstOrDefault(data => data.id == planetId);

			if (target is { })
			{
				DeletePlanet(star, target, updateChildren);
				return;
			}

			Debug.LogWarningFormat("No planet with ID {0} found in system {1}", planetId, star.displayName);
		}
		
		/// <summary>
		/// Removes a planet from the solar system, either updating child orbits or deleting them as well.
		/// </summary>
		/// <param name="star">The solar system.</param>
		/// <param name="planetToDelete">The planet to remove.</param>
		/// <param name="updateChildren">Make children orbit the closest new parent instead of deleting them.</param>
		/// <returns>The planet that was removed.</returns>
		public static void DeletePlanet(this StarData star, PlanetData planetToDelete, bool updateChildren)
		{
			var children = from planet in star.planets
			               where planet.orbitAround == planetToDelete.index
			               select planet;

			if (updateChildren)
			{
				
			}
			else
			{
				children.ForEach(child => DeletePlanet(star, (PlanetData) child, false));
			}
		}

		/// <summary>
		/// Removes all planets from a star.
		/// </summary>
		/// <param name="star">The star to remove all planets from.</param>
		public static void DeleteAllPlanets(this StarData star)
		{
			star.planets
			    .Where(planet => planet.orbitAround == 0)
			    .ForEach(planet => star.DeletePlanet(planet, false));
		}
	}
}
