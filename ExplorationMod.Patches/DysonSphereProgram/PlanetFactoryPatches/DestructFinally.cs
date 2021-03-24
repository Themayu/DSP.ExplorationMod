using BepInEx.Logging;
using HarmonyLib;
using JetBrains.Annotations;
using Zuris.ExplorationMod.Common.Extensions.DysonSphereProgram;

namespace Zuris.ExplorationMod.Patches.DysonSphereProgram.PlanetFactoryPatches
{
	[PublicAPI]
	[HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.DestructFinally))]
	public static class DestructFinallyPatch
	{
		private static ManualLogSource Logger { get; set; } = null!;

		public static void Apply(Harmony harmony, ManualLogSource logger)
		{
			const string originalMethodName = nameof(PlanetFactory.DestructFinally);
			const string outputMethodName = nameof(PlanetFactoryExtensions.DestructObject);
			
			Logger = logger;
			
			harmony.PatchAll(typeof(DestructFinallyPatch));
			
			harmony.CreateReversePatcher(
				       typeof(PlanetFactory).GetMethod(originalMethodName),
				       new HarmonyMethod(typeof(PlanetFactoryExtensions).GetMethod(outputMethodName))
				   ).Patch();
			
			Logger.LogDebug($"Applied patch class {typeof(DestructFinallyPatch).FullName}.");
		}
	}
}
