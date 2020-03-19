namespace DizzyHacks.Hacks
{
    using DizzyHacks.Rendering;
    using DizzyHacks.Settings;
    using System;
    using UnityEngine;

    internal class ESP_Player : MonoBehaviour
    {
        private UColor playerColor = new UColor(255f, 0f, 0f, 255f);
        private UColor sleeperColor = new UColor(255f, 153f, 255f, 255f);

        private void DrawPlayers()
        {
            if (CVars.ESP.DrawPlayers)
            {
                foreach (Character character in ESP_UpdateOBJs.GetPlayerList())
                {
                    Color color = this.playerColor.Get();
                    string equippedItemName = Local.GetEquippedItemName(character.transform);
                    BoundingBox2D boxd = new BoundingBox2D(character);
                    if (boxd.IsValid)
                    {
                        float x = boxd.X;
                        float y = boxd.Y;
                        float width = boxd.Width;
                        float height = boxd.Height;
                        float num5 = Vector3.Distance(character.transform.position, ESP_UpdateOBJs.LocalCharacter.transform.position);
                        Canvas.DrawString(new Vector2(x + (width / 2f), y - 22f), color, Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, character.playerClient.userName);
                        Canvas.DrawString(new Vector2(x + (width / 2f), (y + height) + 2f), color, Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, ((int) num5).ToString());
                        Canvas.DrawBoxOutlines(new Vector2(x, y), new Vector2(width, height), 1f, color);
                        if (equippedItemName != string.Empty)
                        {
                            Vector2 vector = Canvas.TextBounds(equippedItemName);
                            Canvas.DrawString(new Vector2((x - vector.x) - 8f, (y + (height / 2f)) - (vector.y / 2f)), color, Canvas.TextFlags.TEXT_FLAG_OUTLINED, equippedItemName);
                        }
                    }
                }
            }
        }

        private void DrawSleepers()
        {
            if (CVars.ESP.DrawSleepers)
            {
                foreach (UnityEngine.Object obj2 in ESP_UpdateOBJs.SleeperOBJs)
                {
                    if (obj2 != null)
                    {
                        SleepingAvatar avatar = (SleepingAvatar) obj2;
                        Vector3 vector = Camera.main.WorldToScreenPoint(avatar.transform.position);
                        if (vector.z > 0f)
                        {
                            vector.y = Screen.height - (vector.y + 1f);
                            Canvas.DrawString(new Vector2(vector.x, vector.y), this.sleeperColor.Get(), Canvas.TextFlags.TEXT_FLAG_DROPSHADOW, string.Format("[S] [{0}]", (int) vector.z));
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
                    this.DrawPlayers();
                    this.DrawSleepers();
                }
                catch
                {
                }
            }
        }
    }
}

