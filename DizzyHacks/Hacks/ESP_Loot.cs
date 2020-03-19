namespace DizzyHacks.Hacks
{
    using DizzyHacks.Rendering;
    using DizzyHacks.Settings;
    using System;
    using UnityEngine;

    public class ESP_Loot : MonoBehaviour
    {
        public static bool Lootlargebox;
        public static bool Lootsmallbox;
        public static bool Lootstash;
        public static bool Lootplayerbag;
        public static bool Lootrepairbench;
        public static bool Lootcampfire;
        public static bool Lootfurnace;
        public static bool Lootsupplycrate;
        public static bool Lootboxloot;
        public static bool Lootammoloot;
        public static bool Lootmedicalloot;
        public static bool Lootweaponloot;

        private void DrawLoot()
        {
            if (CVars.ESP.DrawLoot)
            {
                foreach (UnityEngine.Object obj2 in ESP_UpdateOBJs.LootableOBJs)
                {
                    if (obj2 == null)
                    {
                        continue;
                    }
                    LootableObject obj3 = (LootableObject) obj2;
                    Vector3 vector = Camera.main.WorldToScreenPoint(obj3.transform.position);
                    if (vector.z > 0f)
                    {
                        string str = "";
                        vector.y = Screen.height - (vector.y + 1f);
                        switch (obj3.name.Replace("(Clone)", ""))
                        {
                            case "WoodBoxLarge":
                                if (Lootlargebox)
                                {
                                    str = string.Format("Large Box [{0}]", (int) vector.z);
                                }
                                break;

                            case "WoodBox":
                                if (Lootsmallbox)
                                {
                                    str = string.Format("Small Box [{0}]", (int) vector.z);
                                }
                                break;

                            case "LootSack":
                                if (Lootplayerbag)
                                {
                                    str = string.Format("Player Bag [{0}]", (int) vector.z);
                                }
                                break;

                            case "SmallStash":
                                if (Lootstash)
                                {
                                    str = string.Format("Stash [{0}]", (int) vector.z);
                                }
                                break;

                            case "RepairBench":
                                if (Lootrepairbench)
                                {
                                    str = string.Format("Repair Bench [{0}]", (int) vector.z);
                                }
                                break;

                            case "Furnace":
                                if (Lootfurnace)
                                {
                                    str = string.Format("Furnace [{0}]", (int) vector.z);
                                }
                                break;

                            case "Campfire":
                                if (Lootcampfire)
                                {
                                    str = string.Format("Campfire [{0}]", (int) vector.z);
                                }
                                break;

                            case "BoxLoot":
                                if (Lootboxloot)
                                {
                                    str = string.Format("Box Loot [{0}]", (int) vector.z);
                                }
                                break;

                            case "AmmoLootBox":
                                if (Lootammoloot)
                                {
                                    str = string.Format("Ammo Loot [{0}]", (int) vector.z);
                                }
                                break;

                            case "MedicalLootBox":
                                if (Lootmedicalloot)
                                {
                                    str = string.Format("Medical Loot [{0}]", (int) vector.z);
                                }
                                break;

                            case "WeaponLootBox":
                                if (Lootweaponloot)
                                {
                                    str = string.Format("Weapon Loot [{0}]", (int) vector.z);
                                }
                                break;

                            case "SupplyCrate":
                                if (Lootsupplycrate)
                                {
                                    Canvas.DrawString(new Vector2(vector.x, vector.y), Color.magenta, Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, string.Format("Supply Crate [{0}]", (int) vector.z));
                                }
                                break;
                        }
                        if (str != "")
                        {
                            Canvas.DrawString(new Vector2(vector.x, vector.y), Color.cyan, Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, string.Format("{0}", str));
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
                    this.DrawLoot();
                }
                catch
                {
                }
            }
        }
    }
}

