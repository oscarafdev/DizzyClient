namespace DizzyHacks.Hacks
{
    using DizzyHacks.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    internal class ESP_UpdateOBJs : MonoBehaviour
    {
        public static UnityEngine.Object[] CharacterOBJs;
        public static UnityEngine.Object[] DoorOBJs;
        public static UnityEngine.Object[] LootableOBJs;
        public static UnityEngine.Object[] PlayerOBJs;
        public static UnityEngine.Object[] ResourceOBJs;
        public static UnityEngine.Object[] SleeperOBJs;
        public static UnityEngine.Object[] StructureOBJs;
        public static Character LocalCharacter;
        public static HumanController LocalController;
        public static PlayerClient LocalPlayerClient;
        public static bool IsIngame;

        public static System.Collections.Generic.List<Character> GetAnimalList()
        {
            System.Collections.Generic.List<Character> list = new System.Collections.Generic.List<Character>();
            foreach (UnityEngine.Object obj2 in CharacterOBJs)
            {
                if (obj2 != null)
                {
                    Character item = (Character) obj2;
                    if (!((((item == null) || (item.playerClient != null)) || (!item.alive || item.dead)) || item.name.Contains("Ragdoll")))
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static System.Collections.Generic.List<Character> GetPlayerList()
        {
            System.Collections.Generic.List<Character> list = new System.Collections.Generic.List<Character>();
            foreach (UnityEngine.Object obj2 in PlayerOBJs)
            {
                if (obj2 != null)
                {
                    Player player = (Player) obj2;
                    if (((player.gameObject != LocalCharacter.gameObject) && (player.playerClient != null)) && (player.alive && !player.dead))
                    {
                        list.Add(player.character);
                    }
                }
            }
            return list;
        }

        private void Start()
        {
            base.StartCoroutine(this.UpdateObjects());
        }

        private IEnumerator UpdateObjects()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                try
                {
                    IsIngame = false;
                    LocalPlayerClient = PlayerClient.GetLocalPlayer();
                    if (LocalPlayerClient != null)
                    {
                        Controllable controllable = LocalPlayerClient.controllable;
                        if (controllable != null)
                        {
                            LocalCharacter = controllable.character;
                            if (LocalCharacter != null)
                            {
                                LocalController = LocalCharacter.controller as HumanController;
                                if ((LocalCharacter.gameObject != null) && (LocalController != null))
                                {
                                    IsIngame = true;
                                    if (CVars.ESP.DrawPlayers)
                                    {
                                        PlayerOBJs = FindObjectsOfType<Player>();
                                    }
                                    if (CVars.ESP.DrawRaid || CVars.ESP.DrawLoot)
                                    {
                                        LootableOBJs = FindObjectsOfType<LootableObject>();
                                    }
                                    if (CVars.ESP.DrawRaid)
                                    {
                                        DoorOBJs = FindObjectsOfType<BasicDoor>();
                                    }
                                    if (CVars.ESP.DrawResources)
                                    {
                                        ResourceOBJs = FindObjectsOfType<ResourceObject>();
                                    }
                                    if (CVars.ESP.DrawAnimals)
                                    {
                                        CharacterOBJs = FindObjectsOfType<Character>();
                                    }
                                    if (CVars.ESP.DrawSleepers)
                                    {
                                        SleeperOBJs = FindObjectsOfType<SleepingAvatar>();
                                    }
                                    if (CVars.WH.whack)
                                    {
                                        StructureOBJs = UnityEngine.Resources.FindObjectsOfTypeAll(typeof(StructureComponent));
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }

    }
}

