namespace DizzyHacks.Hacks
{
    using DizzyHacks.Rendering;
    using DizzyHacks.Settings;
    using System;
    using UnityEngine;

    public class ESP_Resource : MonoBehaviour
    {
        private UColor resourceColor = new UColor(255f, 255f, 0f, 255f);
        public static bool Resmetal;
        public static bool Ressulfur;
        public static bool Resstone;
        public static bool Reswood;

        private void DrawResources()
        {
            if (CVars.ESP.DrawResources)
            {
                foreach (UnityEngine.Object obj2 in ESP_UpdateOBJs.ResourceOBJs)
                {
                    if (obj2 == null)
                    {
                        continue;
                    }
                    ResourceObject obj3 = (ResourceObject) obj2;
                    Vector3 vector = Camera.main.WorldToScreenPoint(obj3.transform.position);
                    if (vector.z <= 0f)
                    {
                        continue;
                    }
                    string text = "";
                    vector.y = Screen.height - (vector.y + 1f);
                    string str2 = obj3.name.Replace("(Clone)", "");
                    if (str2 != null)
                    {
                        if (str2 != "Ore1")
                        {
                            if (str2 == "Ore2")
                            {
                                goto Label_011F;
                            }
                            if (str2 == "Ore3")
                            {
                                goto Label_0149;
                            }
                            if (str2 == "WoodPile")
                            {
                                goto Label_0173;
                            }
                        }
                        else if (Ressulfur)
                        {
                            text = string.Format("Sulfur Ore [{0}]", (int) vector.z);
                        }
                    }
                    goto Label_019D;
                Label_011F:
                    if (Resmetal)
                    {
                        text = string.Format("Metal Ore [{0}]", (int) vector.z);
                    }
                    goto Label_019D;
                Label_0149:
                    if (Resstone)
                    {
                        text = string.Format("Stone Ore [{0}]", (int) vector.z);
                    }
                    goto Label_019D;
                Label_0173:
                    if (Reswood)
                    {
                        text = string.Format("Wood Pile [{0}]", (int) vector.z);
                    }
                Label_019D:
                    if (text != "")
                    {
                        Canvas.DrawString(new Vector2(vector.x, vector.y), this.resourceColor.Get(), Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, text);
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
                    this.DrawResources();
                }
                catch
                {
                }
            }
        }
    }
}

