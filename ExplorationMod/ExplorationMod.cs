using BepInEx;
using ExplorationMod.Patches;
using JetBrains.Annotations;
using System;

namespace ExplorationMod
{
	[PublicAPI]
	[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
	[BepInProcess(PROCESS_NAME_DSP)]
	public class ExplorationMod: BaseUnityPlugin
	{
		public const string PLUGIN_GUID = "zuris.dysonsphereprogram.explorationmod";
		public const string PLUGIN_NAME = "";
		public const string PLUGIN_VERSION = "0.0.0";

		public const string PROCESS_NAME_DSP = "DSPGame.exe";
		
		private void Awake()
		{
			Logger.LogInfo($"Loading mod {PLUGIN_GUID} ${PLUGIN_VERSION}");
			
			Patcher.Patch(PLUGIN_GUID, Logger);
			
			Logger.LogInfo("Mod loaded.");
		}
	}
}
