namespace DizzyHacks.Hacks
{
    using System;
    using UnityEngine;

    internal class Bypass : MonoBehaviour
    {
        private void Update()
        {
            if (SteamClient.steamClientObject != null)
            {
                SteamClient.steamClientObject.SetActive(false);
                SteamClient.SteamClient_Cycle();
            }
        }
    }
}

