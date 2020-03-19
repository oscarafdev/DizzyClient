namespace DizzyHacks.Settings
{
    using System;
    using UnityEngine;

    internal class CVars
    {
        public static void Initialize()
        {
            Aimbot.AimKey = KeyCode.LeftAlt;
            Aimbot.AimAngle = 360f;
            Aimbot.AimAtHead = false;
            Aimbot.AimAtAnimals = false;
            Aimbot.VisibleCheck = false;
            Aimbot.SilentAim = false;
            Aimbot.AutoAim = false;
            Aimbot.AutoShoot = false;
            Crosshair.Enable = true;
            Crosshair.Style = 0;
            Crosshair.Color = 0;
            Crosshair.Opacity = 0xff;
            Crosshair.Size = 10;
            ESP.DrawPlayers = false;
            ESP.DrawLoot = false;
            ESP.DrawRaid = false;
            ESP.DrawAnimals = false;
            ESP.DrawSleepers = false;
            ESP.DrawResources = false;
            WH.whack = false;
            WH.NoWalls = false;
            WH.NoCeilings = false;
            WH.NoDoorways = false;
            WH.NoPillars = false;
            WH.NoWindowWalls = false;
            WH.NoFoundations = false;
            WH.NoStairs = false;
            WH.NoRamps = false;
            Misc.JumpModifer = 1f;
            Misc.SpeedModifer = 10f;
            Misc.NoFallDamage = false;
            Misc.FlyHack = false;
            Misc.LightHack = false;
            Misc.NoRecoil = false;
            Misc.NoReload = false;
            Misc.ShowWatermark = true;
            Misc.Blueprints = false;
            Misc.AutoWood = false;
            Misc.ClientVersion = "v1.0  - Puto el que lee";
        }

        internal class Aimbot
        {
            public static float AimAngle;
            public static bool AimAtAnimals;
            public static bool AimAtHead;
            public static KeyCode AimKey;
            public static bool AutoAim;
            public static bool AutoShoot;
            public static bool SilentAim;
            public static bool VisibleCheck;
        }

        internal class Crosshair
        {
            public static int Color;
            public static bool Enable;
            public static int Opacity;
            public static int Size;
            public static int Style;
        }

        internal class ESP
        {
            public static bool DrawAnimals;
            public static bool DrawLoot;
            public static bool DrawPlayers;
            public static bool DrawRaid;
            public static bool DrawResources;
            public static bool DrawSleepers;
        }

        internal class Misc
        {
            public static bool FlyHack;
            public static float JumpModifer;
            public static bool LightHack;
            public static bool NoFallDamage;
            public static bool NoRecoil;
            public static bool NoReload;
            public static bool ShowWatermark;
            public static bool Blueprints;
            public static bool AutoWood;
            public static float SpeedModifer;
            public static string ClientVersion;
        }

        internal class WH
        {
            public static bool whack;
            public static bool NoWalls;
            public static bool NoCeilings;
            public static bool NoDoorways;
            public static bool NoPillars;
            public static bool NoWindowWalls;
            public static bool NoFoundations;
            public static bool NoStairs;
            public static bool NoRamps;
        }
    }
}

