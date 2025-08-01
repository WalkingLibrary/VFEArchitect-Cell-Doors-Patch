using HarmonyLib;
using RimWorld;
using Verse;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace VanillaFurnitureExpandedCellDoorsPatch
{
    [StaticConstructorOnStartup]
    public static class PatchInit
    {
        static PatchInit()
        {
            var harmony = new Harmony("walkinglibrary.vfe.celldoorspatch");
            harmony.Patch(
                AccessTools.Method(typeof(Region), "Allows"),
                transpiler: new HarmonyMethod(typeof(Patch_Region_Allows), nameof(Patch_Region_Allows.Transpiler))
            );
        }
    }

    public static class Patch_Region_Allows
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var list = new List<CodeInstruction>(instructions);
            var field = AccessTools.Field(typeof(TraverseParms), "canBashDoors");

            for (int i = 0; i < list.Count; i++)
            {
                yield return list[i];

                if (list[i].LoadsField(field))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_1); // TraverseParms
                    yield return CodeInstruction.LoadField(typeof(TraverseParms), "pawn");
                    yield return new CodeInstruction(OpCodes.Ldarg_0); // Region
                    yield return CodeInstruction.LoadField(typeof(Region), "door");
                    yield return CodeInstruction.Call(typeof(Patch_Region_Allows), nameof(DontBashCellDoors));
                }
            }
        }

        public static bool DontBashCellDoors(bool canBash, Pawn pawn, Building_Door door)
        {
            Log.Message($"Checking bash: canBash={canBash}, isPrisoner={pawn?.IsPrisoner}, door={door?.def?.defName}");

            if (!canBash)
                return false;

            if (pawn != null && pawn.IsPrisoner && door.def.defName == "VFEArch_CellDoor")
            {
                Log.Message("Blocking cell door bash attempt.");
                return false;
            }

            return true;
        }

    }
}