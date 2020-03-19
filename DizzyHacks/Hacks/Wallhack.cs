namespace DizzyHacks.Hacks
{
    using DizzyHacks.Settings;
    using System;
    using UnityEngine;

    internal class Wallhack : MonoBehaviour
    {
        public void Update()
        {
            this.WallHack();
        }

        public void WallHack()
        {
            if (CVars.WH.whack && (ESP_UpdateOBJs.StructureOBJs != null))
            {
                foreach (StructureComponent component in ESP_UpdateOBJs.StructureOBJs)
                {
                    if (component.type == StructureComponent.StructureComponentType.Wall)
                    {
                        component.gameObject.SetActive(!CVars.WH.NoWalls);
                    }
                    if (component.type == StructureComponent.StructureComponentType.Ceiling)
                    {
                        component.gameObject.SetActive(!CVars.WH.NoCeilings);
                    }
                    if (component.type == StructureComponent.StructureComponentType.Doorway)
                    {
                        component.gameObject.SetActive(!CVars.WH.NoDoorways);
                    }
                    if (component.type == StructureComponent.StructureComponentType.Pillar)
                    {
                        component.gameObject.SetActive(!CVars.WH.NoPillars);
                    }
                    if (component.type == StructureComponent.StructureComponentType.WindowWall)
                    {
                        component.gameObject.SetActive(!CVars.WH.NoWindowWalls);
                    }
                    if (component.type == StructureComponent.StructureComponentType.Foundation)
                    {
                        component.gameObject.SetActive(!CVars.WH.NoFoundations);
                    }
                    if (component.type == StructureComponent.StructureComponentType.Stairs)
                    {
                        component.gameObject.SetActive(!CVars.WH.NoStairs);
                    }
                    if (component.type == StructureComponent.StructureComponentType.Ramp)
                    {
                        component.gameObject.SetActive(!CVars.WH.NoRamps);
                    }
                }
            }
            if (!CVars.WH.whack && (ESP_UpdateOBJs.StructureOBJs != null))
            {
                foreach (StructureComponent component in ESP_UpdateOBJs.StructureOBJs)
                {
                    if ((((component.type == StructureComponent.StructureComponentType.Wall) || (component.type == StructureComponent.StructureComponentType.Doorway)) || (component.type == StructureComponent.StructureComponentType.Ceiling)) || (component.type == StructureComponent.StructureComponentType.Pillar))
                    {
                        component.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}

