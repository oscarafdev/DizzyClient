namespace DizzyHacks.Hacks
{
    using DizzyHacks.Rendering;
    using DizzyHacks.Settings;
    using System;
    using UnityEngine;

    public class ESP_Animal : MonoBehaviour
    {
        public static bool Anichicken;
        public static bool Anirabbit;
        public static bool Anistag;
        public static bool Anibear;
        public static bool Aniboar;
        public static bool Aniwolf;
        public static bool Animutantwolf;
        public static bool Animutantbear;

        private void DrawAnimals()
        {
            if (CVars.ESP.DrawAnimals)
            {
                foreach (Character character in ESP_UpdateOBJs.GetAnimalList())
                {
                    Vector3 vector = Camera.main.WorldToScreenPoint(character.transform.position);
                    if ((vector.z > 0f) && (character.transform.position.y > 100f))
                    {
                        string text = "";
                        vector.y = Screen.height - (vector.y + 1f);
                        switch (character.name.Replace("(Clone)", ""))
                        {
                            case "Chicken_A":
                                if (Anichicken)
                                {
                                    text = string.Format("Chicken [{0}]", (int) vector.z);
                                }
                                break;

                            case "Rabbit_A":
                                if (Anirabbit)
                                {
                                    text = string.Format("Rabbit [{0}]", (int) vector.z);
                                }
                                break;

                            case "Stag_A":
                                if (Anistag)
                                {
                                    text = string.Format("Deer [{0}]", (int) vector.z);
                                }
                                break;

                            case "Bear_A":
                                if (Anibear)
                                {
                                    text = string.Format("Bear [{0}]", (int) vector.z);
                                }
                                break;

                            case "Boar_A":
                                if (Aniboar)
                                {
                                    text = string.Format("Boar [{0}]", (int) vector.z);
                                }
                                break;

                            case "Wolf":
                                if (Aniwolf)
                                {
                                    text = string.Format("Wolf [{0}]", (int) vector.z);
                                }
                                break;

                            case "MutantWolf":
                                if (Animutantwolf)
                                {
                                    text = string.Format("Mutant Wolf [{0}]", (int) vector.z);
                                }
                                break;

                            case "MutantBear":
                                if (Animutantbear)
                                {
                                    text = string.Format("Mutant Bear [{0}]", (int) vector.z);
                                }
                                break;
                        }
                        if (text != "")
                        {
                            Canvas.DrawString(new Vector2(vector.x, vector.y), Color.gray, Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, text);
                        }
                    }
                }
            }
        }

        private void OnGUI()
        {
            if ((Event.current.type == EventType.Repaint) && ESP_UpdateOBJs.IsIngame)
            {
                try
                {
                    this.DrawAnimals();
                }
                catch
                {
                }
            }
        }
    }
}

