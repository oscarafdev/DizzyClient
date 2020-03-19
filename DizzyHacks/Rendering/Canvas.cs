namespace DizzyHacks.Rendering
{
    using DizzyHacks.Settings;
    using System;
    using UnityEngine;

    public class Canvas : MonoBehaviour
    {
        private static float averageFramesPerSecond = 0f;
        private static Rect brandingRect;
        private static Texture2D drawingTex = null;
        private static float framesPerSecond = 0f;
        private static float lastFrameTime = Environment.TickCount;
        private static Color lastTexColor;
        private static Material lineMaterial;
        private static float totalFramesPerSecond = 0f;
        private float timeA;
        public bool showinjtxt = true;
        public static Texture2D blip = null;

        public static void DrawBox(Vector2 pos, Vector2 size, Color color)
        {
            if (drawingTex == null)
            {
                drawingTex = new Texture2D(1, 1);
            }
            if (color != lastTexColor)
            {
                drawingTex.SetPixel(0, 0, color);
                drawingTex.Apply();
                lastTexColor = color;
            }
            GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), drawingTex);
        }

        public static void DrawBoxOutlines(Vector2 position, Vector2 size, float borderSize, Color color)
        {
            DrawBox(new Vector2(position.x + borderSize, position.y), new Vector2(size.x - (2f * borderSize), borderSize), color);
            DrawBox(new Vector2(position.x, position.y), new Vector2(borderSize, size.y), color);
            DrawBox(new Vector2((position.x + size.x) - borderSize, position.y), new Vector2(borderSize, size.y), color);
            DrawBox(new Vector2(position.x + borderSize, (position.y + size.y) - borderSize), new Vector2(size.x - (2f * borderSize), borderSize), color);
        }

        public static void DrawCrosshair()
        {
            if (CVars.Crosshair.Enable)
            {
                int style = CVars.Crosshair.Style;
                int num2 = CVars.Crosshair.Color;
                int opacity = CVars.Crosshair.Opacity;
                int size = CVars.Crosshair.Size;
                int num5 = Screen.width / 2;
                int num6 = Screen.height / 2;
                Color white = Color.white;
                switch (num2)
                {
                    case 0:
                        white = new Color(138f, 43f, 226f, (float) opacity);
                        break;

                    case 1:
                        white = new Color(205f, 0f, 0f, (float) opacity);
                        break;

                    case 2:
                        white = new Color(0f, 255f, 0f, (float) opacity);
                        break;

                    case 3:
                        white = new Color(0f, 255f, 255f, (float) opacity);
                        break;

                    case 4:
                        white = new Color(255f, 255f, 255f, (float) opacity);
                        break;

                    case 5:
                        white = new Color(0f, 0f, 0f, (float) opacity);
                        break;
                }
                if (opacity != 220)
                {
                    white.r /= 255f;
                    white.g /= 255f;
                    white.b /= 255f;
                    white.a /= 255f;
                }
                switch (style)
                {
                    case 0:
                        DrawLine(new Vector2((float) num5, (float) (num6 - size)), new Vector2((float) num5, (float) num6), white);
                        DrawLine(new Vector2((float) num5, (float) num6), new Vector2((float) (num5 + size), (float) num6), white);
                        DrawLine(new Vector2((float) num5, (float) num6), new Vector2((float) num5, (float) (num6 + size)), white);
                        DrawLine(new Vector2((float) (num5 - size), (float) num6), new Vector2((float) num5, (float) num6), white);
                        break;

                    case 1:
                        DrawBox(new Vector2((float) (num5 - size), (float) (num6 - 2)), new Vector2((float) (size * 2), 4f), Color.black);
                        DrawBox(new Vector2((float) (num5 - 2), (float) (num6 - size)), new Vector2(4f, (float) (size * 2)), Color.black);
                        DrawBox(new Vector2((float) ((num5 - size) + 1), (float) (num6 - 1)), new Vector2((float) ((size * 2) - 2), 2f), white);
                        DrawBox(new Vector2((float) (num5 - 1), (float) ((num6 - size) + 1)), new Vector2(2f, (float) ((size * 2) - 2)), white);
                        break;

                    case 2:
                        DrawLine(new Vector2((float) num5, (float) (num6 - size)), new Vector2((float) num5, (float) (num6 - 2)), white);
                        DrawLine(new Vector2((float) (num5 + 2), (float) num6), new Vector2((float) (num5 + size), (float) num6), white);
                        DrawLine(new Vector2((float) num5, (float) (num6 + 2)), new Vector2((float) num5, (float) (num6 + size)), white);
                        DrawLine(new Vector2((float) (num5 - size), (float) num6), new Vector2((float) (num5 - 2), (float) num6), white);
                        break;

                    case 3:
                        DrawLine(new Vector2((float) (num5 - size), (float) (num6 - size)), new Vector2((float) (num5 - 2), (float) (num6 - 2)), white);
                        DrawLine(new Vector2((float) (num5 - size), (float) (num6 + size)), new Vector2((float) (num5 - 2), (float) (num6 + 2)), white);
                        DrawLine(new Vector2((float) (num5 + size), (float) (num6 - size)), new Vector2((float) (num5 + 2), (float) (num6 - 2)), white);
                        DrawLine(new Vector2((float) (num5 + size), (float) (num6 + size)), new Vector2((float) (num5 + 2), (float) (num6 + 2)), white);
                        break;

                    case 4:
                        DrawBox(new Vector2((float) (num5 - (size / 2)), (float) (num6 - (size / 2))), new Vector2((float) size, (float) size), white);
                        break;
                }
            }
        }

        public static void DrawFPS()
        {
            float num;
            if (CVars.Misc.ShowWatermark)
            {
                num = 20f;
            }
            else
            {
                num = 0f;
            }
            DrawString(new Vector2(brandingRect.xMax + 5f, num), new UColor(160f, 32f, 240f, 255f).Get(), TextFlags.TEXT_FLAG_OUTLINED, string.Format("FPS: {0}", (averageFramesPerSecond == 0f) ? ((int) totalFramesPerSecond) : ((int) averageFramesPerSecond)));
        }

        public void DrawInjectTXT()
        {
            if (this.showinjtxt)
            {
                DrawString(new Vector2(brandingRect.xMax + 5f, Screen.height - 30f), new UColor(160f, 32f, 240f, 255f).Get(), TextFlags.TEXT_FLAG_OUTLINED, "Injection Successful, Press INSERT To toggle the menu!");
            }
        }

        public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color)
        {
            if (lineMaterial == null)
            {
                lineMaterial = new Material("Shader \"Lines/Colored Blended\" {SubShader { Pass {   BindChannels { Bind \"Color\",color }   Blend SrcAlpha OneMinusSrcAlpha   ZWrite Off Cull Off Fog { Mode Off }} } }");
                lineMaterial.hideFlags = HideFlags.HideAndDontSave;
                lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
            }
            lineMaterial.SetPass(0);
            GL.Begin(1);
            GL.Color(color);
            GL.Vertex3(pointA.x, pointA.y, 0f);
            GL.Vertex3(pointB.x, pointB.y, 0f);
            GL.End();
        }

        public static void DrawString(Vector2 pos, Color color, TextFlags flags, string text)
        {
            bool center = (flags & TextFlags.TEXT_FLAG_CENTERED) == TextFlags.TEXT_FLAG_CENTERED;
            if ((flags & TextFlags.TEXT_FLAG_OUTLINED) == TextFlags.TEXT_FLAG_OUTLINED)
            {
                DrawStringInternal(pos + new Vector2(1f, 0f), Color.black, text, center);
                DrawStringInternal(pos + new Vector2(0f, 1f), Color.black, text, center);
                DrawStringInternal(pos + new Vector2(0f, -1f), Color.black, text, center);
            }
            if ((flags & TextFlags.TEXT_FLAG_DROPSHADOW) == TextFlags.TEXT_FLAG_DROPSHADOW)
            {
                DrawStringInternal(pos + new Vector2(1f, 1f), Color.black, text, center);
            }
            DrawStringInternal(pos, color, text, center);
        }

        private static void DrawStringInternal(Vector2 pos, Color color, string text, bool center)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label) {
                normal = { textColor = color },
                fontSize = 13
            };
            if (center)
            {
                pos.x -= style.CalcSize(new GUIContent(text)).x / 2f;
            }
            GUI.Label(new Rect(pos.x, pos.y, 264f, 20f), text, style);
        }

        public static void DrawWatermark()
        {
            if (CVars.Misc.ShowWatermark)
            {
                DrawString(new Vector2(brandingRect.xMax + 5f, 0f), new UColor(160f, 32f, 240f, 255f).Get(), TextFlags.TEXT_FLAG_OUTLINED, string.Format("RainbowClient {0}", CVars.Misc.ClientVersion));
            }
        }

        public static void HeliosBox()
        {
            HeliosBox((float) (Screen.width / 2), (float) (Screen.height / 2));
        }

        public static void HeliosBox(float sx, float sy)
        {
            Color color = new Color(255f, 255f, 0f);
            DrawLine(new Vector2(sx - 8f, sy - 8f), new Vector2(sx + 8f, sy - 8f), color);
            DrawLine(new Vector2(sx + 8f, sy - 8f), new Vector2(sx + 8f, sy + 8f), color);
            DrawLine(new Vector2(sx - 8f, sy + 8f), new Vector2(sx + 8f, sy + 8f), color);
            DrawLine(new Vector2(sx - 8f, sy - 8f), new Vector2(sx - 8f, sy + 8f), color);
        }

        public void InjectText()
        {
            this.showinjtxt = !this.showinjtxt;
        }

        public void Start()
        {
            this.DrawInjectTXT();
            base.Invoke("InjectText", 10f);
            this.timeA = Time.timeSinceLevelLoad;
            DontDestroyOnLoad(this);
        }

        public static Vector2 TextBounds(string text)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label) {
                fontSize = 13
            };
            return style.CalcSize(new GUIContent(text));
        }

        public static void UpdateFPS()
        {
            float tickCount = Environment.TickCount;
            totalFramesPerSecond = (framesPerSecond + 0.1f) / ((tickCount - lastFrameTime) / 1000f);
            if ((tickCount - lastFrameTime) > 1000f)
            {
                lastFrameTime = tickCount;
                framesPerSecond = 0f;
                averageFramesPerSecond = totalFramesPerSecond;
            }
            framesPerSecond++;
        }

        [Flags]
        public enum TextFlags
        {
            TEXT_FLAG_NONE,
            TEXT_FLAG_CENTERED,
            TEXT_FLAG_OUTLINED,
            TEXT_FLAG_DROPSHADOW
        }
    }
}

