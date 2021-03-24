using System;
using UnityEngine;

namespace Zuris.ExplorationMod.Common.Extensions.DysonSphereProgram
{
	public static class GalaxyDataExtensions
	{
		public static void DeletePlanet(this GalaxyData galaxy, int planetId, bool updateChildren)
		{
			var target = galaxy.PlanetById(planetId);

			if (target != null)
			{
				target.star.DeletePlanet(target, updateChildren);
				return;
			}

			Debug.LogWarningFormat("No planet with ID {0} found in galaxy", planetId);
		}

		public static EntityData[] EntitiesByPlanetId(this GalaxyData galaxy, int planetId)
		{
			var target = galaxy.PlanetById(planetId);

			EntityData[] entities = { };
			if (target != null)
			{
				Array.Copy(target.factory.entityPool, entities, target.factory.entityCount);
			}
			else
			{
				Debug.LogWarningFormat("No planet with ID {0} found in galaxy", planetId);
			}

			return entities;
		}
	}
}
