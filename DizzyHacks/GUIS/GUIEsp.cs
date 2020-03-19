namespace DizzyHacks.GUIS
{
    using DizzyHacks.Hacks;
    using DizzyHacks.Rendering;
    using DizzyHacks.Settings;
    using System;
    using UnityEngine;

    internal class GUIEsp : MonoBehaviour
    {
        public static Rect startRect = new Rect(200f, 150f, 240f, 400f);

        private void DoMyWindow(int windowID)
        {
            GUI.backgroundColor = new UColor(160f, 32f, 240f, 255f).Get();
            CVars.ESP.DrawPlayers = GUI.Toggle(new Rect(10f, 20f, 100f, 20f), CVars.ESP.DrawPlayers, "Players");
            CVars.ESP.DrawLoot = GUI.Toggle(new Rect(10f, 40f, 100f, 20f), CVars.ESP.DrawLoot, "Loot");
            CVars.ESP.DrawRaid = GUI.Toggle(new Rect(10f, 60f, 100f, 20f), CVars.ESP.DrawRaid, "Raid Helper");
            CVars.ESP.DrawSleepers = GUI.Toggle(new Rect(120f, 20f, 100f, 20f), CVars.ESP.DrawSleepers, "Sleepers");
            CVars.ESP.DrawAnimals = GUI.Toggle(new Rect(120f, 40f, 100f, 20f), CVars.ESP.DrawAnimals, "Animals");
            CVars.ESP.DrawResources = GUI.Toggle(new Rect(120f, 60f, 100f, 20f), CVars.ESP.DrawResources, "Resources");
            GUI.Label(new Rect(70f, 80f, 120f, 20f), "Loot Options");
            ESP_Loot.Lootlargebox = GUI.Toggle(new Rect(10f, 100f, 100f, 20f), ESP_Loot.Lootlargebox, "Large Box");
            ESP_Loot.Lootsmallbox = GUI.Toggle(new Rect(10f, 120f, 100f, 20f), ESP_Loot.Lootsmallbox, "Small Box");
            ESP_Loot.Lootstash = GUI.Toggle(new Rect(10f, 140f, 100f, 20f), ESP_Loot.Lootstash, "Stash");
            ESP_Loot.Lootplayerbag = GUI.Toggle(new Rect(10f, 160f, 100f, 20f), ESP_Loot.Lootplayerbag, "Player Bag");
            ESP_Loot.Lootboxloot = GUI.Toggle(new Rect(10f, 180f, 100f, 20f), ESP_Loot.Lootboxloot, "Box Loot");
            ESP_Loot.Lootmedicalloot = GUI.Toggle(new Rect(10f, 200f, 100f, 20f), ESP_Loot.Lootmedicalloot, "Medical Loot");
            ESP_Loot.Lootrepairbench = GUI.Toggle(new Rect(120f, 100f, 100f, 20f), ESP_Loot.Lootrepairbench, "Repair Bench");
            ESP_Loot.Lootcampfire = GUI.Toggle(new Rect(120f, 120f, 100f, 20f), ESP_Loot.Lootcampfire, "Campfire");
            ESP_Loot.Lootfurnace = GUI.Toggle(new Rect(120f, 140f, 100f, 20f), ESP_Loot.Lootfurnace, "Furnace");
            ESP_Loot.Lootsupplycrate = GUI.Toggle(new Rect(120f, 160f, 100f, 20f), ESP_Loot.Lootsupplycrate, "Supply Crate");
            ESP_Loot.Lootammoloot = GUI.Toggle(new Rect(120f, 180f, 100f, 20f), ESP_Loot.Lootammoloot, "Ammo Loot");
            ESP_Loot.Lootweaponloot = GUI.Toggle(new Rect(120f, 200f, 100f, 20f), ESP_Loot.Lootweaponloot, "Weapon Loot");
            GUI.Label(new Rect(70f, 220f, 120f, 20f), "Animal Options");
            ESP_Animal.Anichicken = GUI.Toggle(new Rect(10f, 240f, 100f, 20f), ESP_Animal.Anichicken, "Chicken");
            ESP_Animal.Anirabbit = GUI.Toggle(new Rect(10f, 260f, 100f, 20f), ESP_Animal.Anirabbit, "Rabbit");
            ESP_Animal.Anistag = GUI.Toggle(new Rect(10f, 280f, 100f, 20f), ESP_Animal.Anistag, "Deer");
            ESP_Animal.Aniboar = GUI.Toggle(new Rect(10f, 300f, 100f, 20f), ESP_Animal.Aniboar, "Boar");
            ESP_Animal.Aniwolf = GUI.Toggle(new Rect(120f, 240f, 100f, 20f), ESP_Animal.Aniwolf, "Wolf");
            ESP_Animal.Anibear = GUI.Toggle(new Rect(120f, 260f, 100f, 20f), ESP_Animal.Anibear, "Bear");
            ESP_Animal.Animutantwolf = GUI.Toggle(new Rect(120f, 280f, 100f, 20f), ESP_Animal.Animutantwolf, "Mutant Wolf");
            ESP_Animal.Animutantbear = GUI.Toggle(new Rect(120f, 300f, 100f, 20f), ESP_Animal.Animutantbear, "Mutant Bear");
            GUI.Label(new Rect(70f, 320f, 120f, 20f), "Resource Options");
            ESP_Resource.Resmetal = GUI.Toggle(new Rect(10f, 340f, 100f, 20f), ESP_Resource.Resmetal, "Metal");
            ESP_Resource.Ressulfur = GUI.Toggle(new Rect(10f, 360f, 100f, 20f), ESP_Resource.Ressulfur, "Sulfur");
            ESP_Resource.Resstone = GUI.Toggle(new Rect(120f, 340f, 100f, 20f), ESP_Resource.Resstone, "Stone");
            ESP_Resource.Reswood = GUI.Toggle(new Rect(120f, 360f, 100f, 20f), ESP_Resource.Reswood, "Wood");
            GUI.DragWindow(new Rect(0f, 0f, (float) Screen.width, (float) Screen.height));
        }

        private void OnGUI()
        {
            if (Local.ShowMenu)
            {
                startRect = GUI.Window(4, startRect, new GUI.WindowFunction(this.DoMyWindow), "ESP");
            }
        }

        private void Start()
        {
            startRect.x = GUIMisc.startRect.xMax + 25f;
        }
    }
}

