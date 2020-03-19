namespace DizzyHacks.GUIS
{
    using DizzyHacks.Hacks;
    using DizzyHacks.Rendering;
    using DizzyHacks.Settings;
    using System;
    using UnityEngine;

    internal class GUICrosshair : MonoBehaviour
    {
        public static Rect startRect = new Rect(200f, 150f, 220f, 140f);

        private void DoMyWindow(int windowID)
        {
            GUI.backgroundColor = new UColor(160f, 32f, 240f, 255f).Get();
            CVars.Crosshair.Enable = GUI.Toggle(new Rect(10f, 20f, 160f, 20f), CVars.Crosshair.Enable, "Draw Crosshair");
            GUI.Label(new Rect(25f, 45f, 120f, 20f), string.Format("Style = ({0})", CVars.Crosshair.Style));
            CVars.Crosshair.Style = (int) GUI.HorizontalSlider(new Rect(125f, 50f, 80f, 12f), (float) CVars.Crosshair.Style, 0f, 4f);
            GUI.Label(new Rect(25f, 65f, 120f, 20f), string.Format("Color = ({0})", CVars.Crosshair.Color));
            CVars.Crosshair.Color = (int) GUI.HorizontalSlider(new Rect(125f, 70f, 80f, 12f), (float) CVars.Crosshair.Color, 0f, 5f);
            GUI.Label(new Rect(25f, 85f, 120f, 20f), string.Format("Opacity = ({0})", CVars.Crosshair.Opacity));
            CVars.Crosshair.Opacity = (int) GUI.HorizontalSlider(new Rect(125f, 90f, 80f, 12f), (float) CVars.Crosshair.Opacity, 1f, 255f);
            GUI.Label(new Rect(25f, 105f, 120f, 20f), string.Format("Size = ({0})", CVars.Crosshair.Size));
            CVars.Crosshair.Size = (int) GUI.HorizontalSlider(new Rect(125f, 110f, 80f, 12f), (float) CVars.Crosshair.Size, 1f, 25f);
            CVars.Crosshair.Style = Mathf.RoundToInt((float) CVars.Crosshair.Style);
            CVars.Crosshair.Color = Mathf.RoundToInt((float) CVars.Crosshair.Color);
            CVars.Crosshair.Opacity = Mathf.RoundToInt((float) CVars.Crosshair.Opacity);
            CVars.Crosshair.Size = Mathf.RoundToInt((float) CVars.Crosshair.Size);
            GUI.DragWindow(new Rect(0f, 0f, (float) Screen.width, (float) Screen.height));
        }

        private void OnGUI()
        {
            if (Local.ShowMenu)
            {
                startRect = GUI.Window(3, startRect, new GUI.WindowFunction(this.DoMyWindow), "Crosshair");
            }
        }

        private void Start()
        {
            startRect.x = GUIAimbot.startRect.xMax + 25f;
            startRect.y = GUIMisc.startRect.yMax + 25f;
        }
    }
}

