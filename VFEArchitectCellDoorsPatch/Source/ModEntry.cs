using Verse;
using HarmonyLib;

namespace VanillaFurnitureExpandedCellDoorsPatch
{
    public class ModEntry : Mod
    {
        public ModEntry(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("vanillafurnitureexpanded.cellDoorsPatch");
            harmony.PatchAll();
        }
    }
}