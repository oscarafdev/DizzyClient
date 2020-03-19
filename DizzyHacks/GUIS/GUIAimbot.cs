namespace DizzyHacks.GUIS
{
    using DizzyHacks.Hacks;
    using DizzyHacks.Rendering;
    using DizzyHacks.Settings;
    using System;
    using UnityEngine;

    internal class GUIAimbot : MonoBehaviour
    {
        private KeyCode lastPressedKey;
        public static bool SetAimKey = true;
        public static Rect startRect = new Rect(200f, 150f, 235f, 170f);

        private void DoMyWindow(int windowID)
        {
            GUI.backgroundColor = new UColor(160f, 32f, 240f, 255f).Get();
            GUI.Label(new Rect(10f, 20f, 115f, 20f), "Aim Key =");
            string text = SetAimKey ? CVars.Aimbot.AimKey.ToString() : "Set Key";
            if (GUI.Button(new Rect(120f, 20f, 100f, 20f), text))
            {
                SetAimKey = false;
            }
            GUI.Label(new Rect(10f, 40f, 110f, 20f), string.Format("Aim FOV = ({0})", (int) CVars.Aimbot.AimAngle));
            CVars.Aimbot.AimAngle = GUI.HorizontalSlider(new Rect(120f, 45f, 100f, 12f), CVars.Aimbot.AimAngle, 1f, 360f);
            CVars.Aimbot.AimAtHead = GUI.Toggle(new Rect(10f, 60f, 120f, 20f), CVars.Aimbot.AimAtHead, "Aim At Head");
            CVars.Aimbot.AimAtAnimals = GUI.Toggle(new Rect(10f, 80f, 120f, 20f), CVars.Aimbot.AimAtAnimals, "Aim At Animals");
            CVars.Aimbot.VisibleCheck = GUI.Toggle(new Rect(10f, 100f, 120f, 20f), CVars.Aimbot.VisibleCheck, "Visible Check");
            CVars.Aimbot.SilentAim = GUI.Toggle(new Rect(10f, 120f, 120f, 20f), CVars.Aimbot.SilentAim, "Silent Kill");
            CVars.Aimbot.AutoAim = GUI.Toggle(new Rect(10f, 140f, 120f, 20f), CVars.Aimbot.AutoAim, "Auto Aim");
            GUI.DragWindow(new Rect(0f, 0f, (float) Screen.width, (float) Screen.height));
        }

        private KeyCode GetPressedKey()
        {
            int length = Enum.GetNames(typeof(KeyCode)).Length;
            for (int i = 0; i < length; i++)
            {
                if (Input.GetKeyDown((KeyCode) i))
                {
                    return (KeyCode) i;
                }
            }
            return KeyCode.None;
        }

        private void OnGUI()
        {
            if (!SetAimKey)
            {
                if ((Event.current.type == EventType.KeyDown) || (Event.current.type == EventType.MouseDown))
                {
                    this.lastPressedKey = Event.current.keyCode;
                }
                if (this.lastPressedKey != KeyCode.None)
                {
                    SetAimKey = true;
                    CVars.Aimbot.AimKey = this.lastPressedKey;
                    this.lastPressedKey = KeyCode.None;
                }
            }
            if (Local.ShowMenu)
            {
                startRect = GUI.Window(0, startRect, new GUI.WindowFunction(this.DoMyWindow), "Aimbot");
            }
        }
    }
}

