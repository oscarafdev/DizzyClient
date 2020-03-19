namespace DizzyHacks.Rendering
{
    using DizzyHacks.Hacks;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class BoundingBox2D
    {
        public BoundingBox2D(Character character)
        {
            Vector3 position = character.transform.position;
            Vector3 vector2 = Local.GetHeadBone(character).transform.position;
            Vector3 vector3 = Camera.main.WorldToScreenPoint(vector2);
            Vector3 vector4 = Camera.main.WorldToScreenPoint(position);
            if ((vector3.z > 0f) && (vector4.z > 0f))
            {
                vector3.y = Screen.height - (vector3.y + 1f);
                vector4.y = Screen.height - (vector4.y + 1f);
                this.Height = (vector4.y + 10f) - vector3.y;
                this.Width = this.Height / 2f;
                this.X = vector3.x - (this.Width / 2f);
                this.Y = vector3.y;
                this.IsValid = true;
            }
            else
            {
                this.IsValid = false;
            }
        }

        public float Height { get; set; }

        public bool IsValid { get; set; }

        public float Width { get; set; }

        public float X { get; set; }

        public float Y { get; set; }
    }
}

