using HarmonyLib;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Zuris.ExplorationMod.Common.Extensions.System;

namespace Zuris.ExplorationMod.Common.Extensions.DysonSphereProgram
{
	public static class PlanetFactoryExtensions
	{
		private static readonly CodeMatch MatchLoadThis = new CodeMatch(i => i.IsLdarg(0));
		private static readonly CodeMatch MatchLoadPlayer = new CodeMatch(i => i.IsLdarg(1));

		private static readonly CodeMatch MatchCallTakeItems =
			new CodeMatch(i => i.Calls(typeof(PlanetFactory).GetMethod(nameof(PlanetFactory.TakeBackItemsInEntity))));
		
		// ReSharper disable once InconsistentNaming
		[PublicAPI]
		public static void DestructObject(this PlanetFactory __instance, int objId, out int protoId)
		{
			// ReSharper disable once LocalFunctionCanBeMadeStatic
#pragma warning disable 8321
			IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction>? instructions, ILGenerator? generator)
			{
				if (instructions is null)
				{
					return new List<CodeInstruction>();
				}
				
				// IL length of the calls to PlayerAction_Build::NotifyDestruct(int32) and
				// instance void PlayerAction_Inspect::NotifyObjectDestruct(EObjectType, int32), minus NOPs
				const int callNotifyLength = 9;

				// IL length of the call to PlanetFactory::TakeBackItemsInEntity(Player, int32)
				const int callTakeItemsLength = 4;

				var codeMatcher = new CodeMatcher(instructions, generator);

				// Delete calls to the above methods, along with associated stack transforms.
				var output = codeMatcher.MatchForward(false, MatchLoadPlayer) // IL_002a: ldarg.1
				                        .SetOpcodeAndAdvance(OpCodes.Nop) // transform: ldarg.1 -> nop
				                        .SetOpcodeAndAdvance(OpCodes.Nop) // transform: callvirt -> nop
				                        .RemoveInstructions(callNotifyLength) // remove: opcode1 ... opcode9
				                        .With(matcher => Console.WriteLine((string) "Currently at instruction {0}", (object) matcher.Instruction))
				                        .MatchForward(false, MatchCallTakeItems) // IL_0050: call instance void PlanetFactory::TakeBackItemsInEntity(class Player, int32)
				                        .MatchBack(false, MatchLoadThis) // IL_004d: ldarg.0
				                        .RemoveInstructions(callTakeItemsLength) // remove: opcode1 ... opcode4
				                        .MatchForward(false, MatchLoadPlayer) // IL_00a4: ldarg.1
				                        .SetOpcodeAndAdvance(OpCodes.Nop) // transform: ldarg.1 -> nop
				                        .SetOpcodeAndAdvance(OpCodes.Nop) // transform: callvirt -> nop
				                        .RemoveInstructions(callNotifyLength) // remove: opcode1 ... opcode9
				                        .InstructionEnumeration()
				                        .ToList();

				output.ForEach(instruction => {
					if (instruction.IsLdarg(2))
					{
						instruction.opcode = OpCodes.Ldarg_1; // transform: ldarg.2 -> ldarg.1
					}
					else if (instruction.IsLdarg(3))
					{
						instruction.opcode = OpCodes.Ldarg_2; // transform: ldarg.3 -> ldarg.2
					}
				});
				
				return output;
			}

			protoId = 0;
#pragma warning restore 8321
		}
	}
}
