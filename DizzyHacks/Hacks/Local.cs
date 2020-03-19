namespace DizzyHacks.Hacks
{
    using DizzyHacks.Rendering;
    using DizzyHacks.Settings;
    using Facepunch.Cursor;
    using System;
    using UnityEngine;
    using static DizzyHacks.Settings.CVars;

    internal class Local : MonoBehaviour
    {
        private UnlockCursorNode cursor;
        public static bool ShowMenu;
        public static ConsoleNetworker singleton;

        public static Transform GetBodyBone(Character character)
        {
            foreach (Transform transform in character.GetComponentsInChildren<Transform>(false))
            {
                if (transform.gameObject.name.Contains("Pelvis"))
                {
                    return transform;
                }
            }
            return null;
        }

        public static InventoryItem GetCurrentEquippedItem(Character character)
        {
            InventoryHolder component = character.GetComponent<InventoryHolder>();
            if ((component != null) && (component.itemRepresentation != null))
            {
                return (InventoryItem) component.inputItem;
            }
            return null;
        }

        public static InventoryItem GetCurrentEquippedItem(Controller controller)
        {
            Inventory component = controller.GetComponent<Inventory>();
            if (((component != null) && (component.activeItem != null)) && (component.activeItem.datablock != null))
            {
                return (InventoryItem) component.activeItem;
            }
            return null;
        }

        public static string GetEquippedItemName(Transform parent)
        {
            string str = string.Empty;
            foreach (Transform transform in parent.GetComponentsInChildren<Transform>())
            {
                ItemRepresentation component = transform.gameObject.GetComponent<ItemRepresentation>();
                if (component != null)
                {
                    return component.datablock.name;
                }
            }
            return str;
        }

        public static Transform GetEyeBone(Character character)
        {
            foreach (Transform transform in character.GetComponentsInChildren<Transform>(false))
            {
                if (transform.gameObject.name == "Eyes")
                {
                    return transform;
                }
            }
            return null;
        }

        public static Transform GetHeadBone(Character character)
        {
            foreach (Transform transform in character.GetComponentsInChildren<Transform>(false))
            {
                if (transform.gameObject.name.Contains("_Head1") || (transform.gameObject.name == "Head"))
                {
                    return transform;
                }
            }
            return null;
        }

        public static void LogToConsole(string message)
        {
            ConsoleWindow window = FindObjectOfType<ConsoleWindow>();
            if (window != null)
            {
                window.AddText(message, false);
            }
        }

        public void OnGUI()
        {
            if (Event.current.type == EventType.Repaint)
            {
                Canvas.DrawWatermark();
                Canvas.DrawFPS();
                Canvas.DrawCrosshair();
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F2))
            {
                ShowMenu = !ShowMenu;
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F4))
            {
                singleton = new ConsoleNetworker();
                if (singleton != null)
                {
                    singleton.networkView.RPC<string>("SV_RunConsoleCommand", uLink.RPCMode.Server, "global.say HOLA");
                }
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F3))
            {
                if(Ready.Load.activeSelf)
                {
                    CVars.Aimbot.AimKey = KeyCode.LeftAlt;
                    CVars.Aimbot.AimAngle = 360f;
                    CVars.Aimbot.AimAtHead = false;
                    CVars.Aimbot.AimAtAnimals = false;
                    CVars.Aimbot.VisibleCheck = false;
                    CVars.Aimbot.SilentAim = false;
                    CVars.Aimbot.AutoAim = false;
                    CVars.Aimbot.AutoShoot = false;
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
                    CVars.Misc.JumpModifer = 1f;
                    CVars.Misc.SpeedModifer = 10f;
                    CVars.Misc.NoFallDamage = false;
                    CVars.Misc.FlyHack = false;
                    CVars.Misc.LightHack = false;
                    CVars.Misc.NoRecoil = false;
                    CVars.Misc.NoReload = false;
                    CVars.Misc.ShowWatermark = true;
                    CVars.Misc.Blueprints = false;
                    CVars.Misc.AutoWood = false;
                    //Ready.Load.SetActive(false);
                } 
            }
            if (this.cursor == null)
            {
                this.cursor = LockCursorManager.CreateCursorUnlockNode(false, "Death Screen");
            }
            this.cursor.On = ShowMenu;
            Canvas.UpdateFPS();
        }
    }
}

