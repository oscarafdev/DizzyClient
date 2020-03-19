namespace DizzyHacks.GUIS
{
    using DizzyHacks.Hacks;
    using DizzyHacks.Rendering;
    using DizzyHacks.Settings;
    using System;
    using UnityEngine;

    internal class GUIWallhack : MonoBehaviour
    {
        public static Rect startRect = new Rect(200f, 150f, 235f, 200f);
        public string wallhackstr = "OFF";

        private void DoMyWindow(int windowID)
        {
            if (CVars.WH.whack)
            {
                try
                {
                    this.wallhackstr = "ON";
                    GUI.backgroundColor = Color.green;
                }
                catch
                {
                }
            }
            if (!CVars.WH.whack)
            {
                try
                {
                    this.wallhackstr = "OFF";
                    GUI.backgroundColor = Color.red;
                }
                catch
                {
                }
            }
            if (GUI.Button(new Rect(10f, 20f, 215f, 20f), this.wallhackstr))
            {
                CVars.WH.whack = !CVars.WH.whack;
            }
            GUI.backgroundColor = new UColor(160f, 32f, 240f, 255f).Get();
            CVars.WH.NoWalls = GUI.Toggle(new Rect(10f, 40f, 160f, 20f), CVars.WH.NoWalls, "Walls");
            CVars.WH.NoCeilings = GUI.Toggle(new Rect(10f, 60f, 160f, 20f), CVars.WH.NoCeilings, "Ceilings");
            CVars.WH.NoDoorways = GUI.Toggle(new Rect(10f, 80f, 160f, 20f), CVars.WH.NoDoorways, "Doorways");
            CVars.WH.NoPillars = GUI.Toggle(new Rect(10f, 100f, 160f, 20f), CVars.WH.NoPillars, "Pillars");
            CVars.WH.NoWindowWalls = GUI.Toggle(new Rect(10f, 120f, 160f, 20f), CVars.WH.NoWindowWalls, "Window Walls");
            CVars.WH.NoFoundations = GUI.Toggle(new Rect(10f, 140f, 160f, 20f), CVars.WH.NoFoundations, "Foundations");
            CVars.WH.NoStairs = GUI.Toggle(new Rect(10f, 160f, 160f, 20f), CVars.WH.NoStairs, "Stairs");
            CVars.WH.NoRamps = GUI.Toggle(new Rect(10f, 180f, 160f, 20f), CVars.WH.NoRamps, "Ramps");
            GUI.DragWindow(new Rect(0f, 0f, (float) Screen.width, (float) Screen.height));
        }

        private void OnGUI()
        {
            if (Local.ShowMenu)
            {
                startRect = GUI.Window(1, startRect, new GUI.WindowFunction(this.DoMyWindow), "Wallhack");
            }
        }

        private void Start()
        {
            startRect.y = GUIAimbot.startRect.yMax + 25f;
        }
    }
}

