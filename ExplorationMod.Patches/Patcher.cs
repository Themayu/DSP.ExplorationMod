using BepInEx.Logging;
using ExplorationMod.Patches.DysonSphereProgram.PlanetFactoryPatches;
using HarmonyLib;
using HarmonyLib.Tools;
using System.Reflection;

namespace ExplorationMod.Patches
{
	public static class Patcher
	{
		public static void Patch(string guid, ManualLogSource logger)
		{
#if DEBUG
			HarmonyFileLog.Enabled = true;		
#endif

			var harmony = new Harmony(guid);
			
			PatchPlanetFactory(harmony, logger);
		}

		private static void PatchPlanetFactory(Harmony harmony, ManualLogSource logger)
		{
			DestructFinallyPatch.Apply(harmony, logger);
		}
	}
}
