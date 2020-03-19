namespace DizzyHacks.GUIS
{
    using DizzyHacks.Hacks;
    using DizzyHacks.Rendering;
    using DizzyHacks.Settings;
    using Rust;
    using System;
    using UnityEngine;

    internal class GUIMisc : MonoBehaviour
    {
        public static Rect startRect = new Rect(200f, 150f, 220f, 230f);

        private void DoMyWindow(int windowID)
        {
            GUI.backgroundColor = new UColor(160f, 32f, 240f, 255f).Get();
            GUI.Label(new Rect(10f, 20f, 120f, 20f), string.Format("Jump = ({0:0.#})", CVars.Misc.JumpModifer));
            CVars.Misc.JumpModifer = GUI.HorizontalSlider(new Rect(130f, 25f, 80f, 12f), CVars.Misc.JumpModifer, 1f, 30f);
            GUI.Label(new Rect(10f, 40f, 110f, 20f), string.Format("Speed = ({0:0.#})", (CVars.Misc.SpeedModifer * 4f) / 10f));
            CVars.Misc.SpeedModifer = GUI.HorizontalSlider(new Rect(130f, 45f, 80f, 12f), CVars.Misc.SpeedModifer, 10f, 20f);
            if (GUI.Button(new Rect(10f, 60f, 200f, 20f), "Unlock Blueprints"))
            {
                DizzyHacks.Hacks.Misc.AllBlueprints();
                Notice.Inventory("", "All Blueprints Unlocked!");
            }
            CVars.Misc.NoFallDamage = GUI.Toggle(new Rect(10f, 80f, 160f, 20f), CVars.Misc.NoFallDamage, "No Fall Damage");
            CVars.Misc.FlyHack = GUI.Toggle(new Rect(10f, 100f, 160f, 20f), CVars.Misc.FlyHack, "Fly Hack");
            CVars.Misc.LightHack = GUI.Toggle(new Rect(10f, 120f, 160f, 20f), CVars.Misc.LightHack, "Light Hack");
            CVars.Misc.NoRecoil = GUI.Toggle(new Rect(10f, 140f, 160f, 20f), CVars.Misc.NoRecoil, "No Recoil/Spread");
            CVars.Misc.NoReload = GUI.Toggle(new Rect(10f, 160f, 160f, 20f), CVars.Misc.NoReload, "No Reload");
            CVars.Misc.AutoWood = GUI.Toggle(new Rect(10f, 180f, 160f, 20f), CVars.Misc.AutoWood, "Auto Wood (BROKEN!)");
            CVars.Misc.ShowWatermark = GUI.Toggle(new Rect(10f, 200f, 160f, 20f), CVars.Misc.ShowWatermark, "Show Watermark");
            GUI.DragWindow(new Rect(0f, 0f, (float) Screen.width, (float) Screen.height));
        }

        private void OnGUI()
        {
            if (Local.ShowMenu)
            {
                startRect = GUI.Window(2, startRect, new GUI.WindowFunction(this.DoMyWindow), "Misc");
            }
        }

        private void Start()
        {
            startRect.x = GUIAimbot.startRect.xMax + 25f;
        }
    }
}

