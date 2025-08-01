using Verse;
using HarmonyLib;
using UnityEngine;

namespace VFEArchitectCellDoorsPatch
{
    public class ModEntry : Mod
    {
        
        public static Settings Settings;
        
        public ModEntry(ModContentPack content) : base(content)
        {
            Settings = GetSettings<Settings>();
            var harmony = new Harmony("vanillafurnitureexpanded.cellDoorsPatch");
            harmony.PatchAll();
        }
        
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.CheckboxLabeled("Enable Debug Logging", ref Settings.enableDebugLogging, "Enable verbose debug logs for door-bashing logic.");
            listing.End();
        }

        public override string SettingsCategory() => "VFE: Cell Doors Patch";
    }
}