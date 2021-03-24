using BepInEx.Logging;
using HarmonyLib;
using HarmonyLib.Tools;
using Zuris.ExplorationMod.Patches.DysonSphereProgram.PlanetFactoryPatches;

namespace Zuris.ExplorationMod.Patches
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
