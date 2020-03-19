namespace DizzyHacks.Hacks
{
    using DizzyHacks.Settings;
    using System;
    using UnityEngine;

    public class ESP_RaidHelper : MonoBehaviour
    {
        private Shader shader;
        private Shader shaderStandart;
        private Shader shaderStandart1;
        private Shader shaderStandart2;

        private void DrawRaidESP()
        {
            Character localCharacter = ESP_UpdateOBJs.LocalCharacter;
            if (ESP_UpdateOBJs.DoorOBJs != null)
            {
                foreach (UnityEngine.Object obj2 in ESP_UpdateOBJs.DoorOBJs)
                {
                    if (obj2 != null)
                    {
                        BasicDoor door = (BasicDoor) obj2;
                        float num = Vector3.Distance(door.transform.position, localCharacter.transform.position);
                        if (CVars.ESP.DrawRaid && (num < 200f))
                        {
                            door.gameObject.renderer.material.shader = this.shader;
                        }
                        else if (door.name.Contains("Metal"))
                        {
                            if (this.shaderStandart1 == null)
                            {
                                this.shaderStandart1 = door.gameObject.renderer.material.shader;
                            }
                            door.gameObject.renderer.material.shader = this.shaderStandart1;
                        }
                        else if (door.name.Contains("Wooden"))
                        {
                            if (this.shaderStandart == null)
                            {
                                this.shaderStandart = door.gameObject.renderer.material.shader;
                            }
                            door.gameObject.renderer.material.shader = this.shaderStandart;
                        }
                        else
                        {
                            if (this.shaderStandart2 == null)
                            {
                                this.shaderStandart2 = door.gameObject.renderer.material.shader;
                            }
                            door.gameObject.renderer.material.shader = this.shaderStandart2;
                        }
                    }
                }
            }
            if (ESP_UpdateOBJs.LootableOBJs != null)
            {
                foreach (UnityEngine.Object obj3 in ESP_UpdateOBJs.LootableOBJs)
                {
                    if (obj3 != null)
                    {
                        LootableObject obj4 = (LootableObject) obj3;
                        float num2 = Vector3.Distance(obj4.transform.position, localCharacter.transform.position);
                        if (CVars.ESP.DrawRaid)
                        {
                            if (obj4.name.Contains("WoodBoxLarge") || obj4.name.Contains("Stash"))
                            {
                                if (num2 < 200f)
                                {
                                    obj4.gameObject.renderer.material.shader = this.shader;
                                }
                                else if (obj4.name.Contains("Stash"))
                                {
                                    obj4.gameObject.renderer.material.shader = this.shaderStandart1;
                                }
                            }
                        }
                        else
                        {
                            obj4.gameObject.renderer.material.shader = this.shaderStandart2;
                        }
                    }
                }
            }
        }

        private void OnGUI()
        {
            try
            {
                if ((Event.current.type == EventType.Repaint) && ESP_UpdateOBJs.IsIngame)
                {
                    this.DrawRaidESP();
                }
            }
            catch
            {
            }
        }

        private void Start()
        {
            this.shader = Shader.Find("GUI/Text Shader");
            this.shaderStandart = Shader.Find("Bumped Specular");
            this.shaderStandart1 = Shader.Find("Bumped Diffuse");
            this.shaderStandart2 = Shader.Find("Diffuse");
        }
    }
}

