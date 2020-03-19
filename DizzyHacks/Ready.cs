namespace DizzyHacks
{
    using DizzyHacks.GUIS;
    using DizzyHacks.Hacks;
    using DizzyHacks.Settings;
    using System;
    using UnityEngine;

    public class Ready
    {
        public static GameObject Load;
        public static GameObject Load2;

        public static void Init()
        {
            CVars.Initialize();
            Load = new GameObject();
            Load.AddComponent<Bypass>();
            Load.AddComponent<Local>();
            Load.AddComponent<GUIAimbot>();
            Load.AddComponent<GUIWallhack>();
            Load.AddComponent<GUIMisc>();
            Load.AddComponent<GUICrosshair>();
            Load.AddComponent<GUIEsp>();
            Load.AddComponent<Misc>();
            Load.AddComponent<Aimbot>();
            Load.AddComponent<Wallhack>();
            Load.AddComponent<ESP_Player>();
            Load.AddComponent<ESP_Loot>();
            Load.AddComponent<ESP_RaidHelper>();
            Load.AddComponent<ESP_Resource>();
            Load.AddComponent<ESP_Animal>();
            Load.AddComponent<ESP_UpdateOBJs>();
            UnityEngine.Object.DontDestroyOnLoad(Load);

            /*Load2 = new GameObject();
            Load2.AddComponent<Bypass>();
            Load2.AddComponent<Local>();
            UnityEngine.Object.DontDestroyOnLoad(Load);*/
        }
    }
}

