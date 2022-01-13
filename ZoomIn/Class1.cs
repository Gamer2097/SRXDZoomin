using BepInEx;
using HarmonyLib;
using BepInEx.Configuration;

namespace ZoomIn
{
    [BepInPlugin("Zoomin", "Zoomin", "1.0.1")]
    public class Zoomin : BaseUnityPlugin
    {
        public static ConfigEntry<int> Fieldofview { get; private set; }
        private static BepInEx.Logging.ManualLogSource logger { get; set; }
        private void Awake()
        {
            
            Fieldofview = Config.Bind("Field of View value",
                                      "FoV",
                                      80,
                                      "Sets the FoV when you play a track");
            Harmony.CreateAndPatchAll(typeof(cameraPatches));
        }
        
        public class cameraPatches
        {
            [HarmonyPatch(typeof(Track), nameof(Track.PlayTrack)), HarmonyPostfix]
            private static void PlayTrack_Postfix()
            {
                GameplayVariables.Instance.camFieldOfView = Fieldofview.Value;
            }
        }
    }
}
