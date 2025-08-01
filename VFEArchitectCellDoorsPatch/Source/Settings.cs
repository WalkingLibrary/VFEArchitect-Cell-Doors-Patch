using Verse;

namespace VFEArchitectCellDoorsPatch
{
    public class Settings : ModSettings
    {
        public bool enableDebugLogging = false;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref enableDebugLogging, "enableDebugLogging", false);
        }
    }
}