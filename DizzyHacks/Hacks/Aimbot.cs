namespace DizzyHacks.Hacks
{
    using DizzyHacks.GUIS;
    using DizzyHacks.Rendering;
    using DizzyHacks.Settings;
    using System;
    using System.Collections.Generic;
    using uLink;
    using UnityEngine;

    internal class Aimbot : UnityEngine.MonoBehaviour
    {
        private Vector2 heliosBoxPos = Vector2.zero;

        public void AutoAimAtPlayer(Character localCharacter, Character targetCharacter)
        {
            Vector3 startPosition = new Vector3(0f, 0f, 0f);
            Vector3 endPosition = new Vector3(0f, 0f, 0f);
            this.GetAimPosition(localCharacter, targetCharacter, ref startPosition, ref endPosition);
            Vector3 v = endPosition - startPosition;
            v.Normalize();
            Angle2 normalized = Angle2.LookDirection(v).normalized;
            localCharacter.eyesAngles = normalized;
        }

        public void GetAimPosition(Character localCharacter, Character targetCharacter, ref Vector3 startPosition, ref Vector3 endPosition)
        {
            startPosition = localCharacter.transform.position;
            endPosition = targetCharacter.transform.position;
            Transform eyeBone = Local.GetEyeBone(localCharacter);
            Transform transform2 = CVars.Aimbot.AimAtHead ? Local.GetHeadBone(targetCharacter) : Local.GetBodyBone(targetCharacter);
            startPosition.y++;
            if (eyeBone != null)
            {
                startPosition = eyeBone.position;
            }
            endPosition.y++;
            if (transform2 != null)
            {
                endPosition = transform2.position;
            }
        }

        private Character GetClosestToCrosshair()
        {
            Character localCharacter = ESP_UpdateOBJs.LocalCharacter;
            Character character2 = null;
            float num = 99999f;
            float num2 = Screen.width / 2;
            float num3 = Screen.height / 2;
            System.Collections.Generic.List<Character> playerList = ESP_UpdateOBJs.GetPlayerList();
            if (CVars.Aimbot.AimAtAnimals)
            {
                foreach (Character character3 in ESP_UpdateOBJs.GetAnimalList())
                {
                    playerList.Add(character3);
                }
            }
            foreach (Character character4 in playerList)
            {
                if (this.ValidatePlayerClient_ForTarget(character4))
                {
                    Vector3 vector = Camera.main.WorldToScreenPoint(character4.transform.position);
                    if (vector.z >= 0f)
                    {
                        vector.y = Screen.height - (vector.y + 1f);
                        float num4 = 0f;
                        float num5 = 0f;
                        if (vector.x > num2)
                        {
                            num4 = vector.x - num2;
                        }
                        else
                        {
                            num4 = num2 - vector.x;
                        }
                        if (vector.y > num3)
                        {
                            num5 = vector.y - num3;
                        }
                        else
                        {
                            num5 = num3 - vector.y;
                        }
                        float num6 = (float) Math.Sqrt((double) ((num4 * num4) + (num5 * num5)));
                        if (num6 < num)
                        {
                            character2 = character4;
                            num = num6;
                        }
                    }
                }
            }
            return character2;
        }

        private float GetRotationFov(Character localCharacte, ref Vector3 startPos, ref Vector3 endPos)
        {
            Vector3 vector = endPos - startPos;
            Angle2 normalized = Angle2.LookDirection(vector.normalized).normalized;
            normalized.pitch -= localCharacte.eyesAngles.pitch;
            normalized.yaw -= localCharacte.eyesAngles.yaw;
            return (float) Math.Sqrt((double) ((normalized.pitch * normalized.pitch) + (normalized.yaw * normalized.yaw)));
        }

        private bool IsTargetInFOV(Character localCharacter, Character targetCharacter)
        {
            Vector3 startPosition = new Vector3(0f, 0f, 0f);
            Vector3 endPosition = new Vector3(0f, 0f, 0f);
            this.GetAimPosition(localCharacter, targetCharacter, ref startPosition, ref endPosition);
            if ((CVars.Aimbot.AimAngle < 360f) && (this.GetRotationFov(localCharacter, ref startPosition, ref endPosition) > CVars.Aimbot.AimAngle))
            {
                return false;
            }
            return true;
        }

        private bool IsTargetVisible(Character localCharacter, Character targetCharacter)
        {
            RaycastHit hit;
            Vector3 startPosition = new Vector3(0f, 0f, 0f);
            Vector3 endPosition = new Vector3(0f, 0f, 0f);
            this.GetAimPosition(localCharacter, targetCharacter, ref startPosition, ref endPosition);
            if (endPosition.y > 1500f)
            {
                return false;
            }
            if (Physics.Linecast(startPosition, endPosition, out hit, 0x80401))
            {
                return (hit.transform.gameObject == targetCharacter.gameObject);
            }
            return true;
        }

        private void OnGUI()
        {
            if (this.heliosBoxPos != Vector2.zero)
            {
                Canvas.HeliosBox(this.heliosBoxPos.x, this.heliosBoxPos.y);
            }
        }

        private bool SilentAim(Character localCharacter, Character targetCharacter)
        {
            InventoryItem currentEquippedItem = Local.GetCurrentEquippedItem(localCharacter.GetComponent<HumanController>());
            if (currentEquippedItem == null)
            {
                return false;
            }
            uLink.BitStream stream = new uLink.BitStream(false);
            if (currentEquippedItem is BulletWeaponItem<BulletWeaponDataBlock>)
            {
                BulletWeaponItem<BulletWeaponDataBlock> item2 = currentEquippedItem as BulletWeaponItem<BulletWeaponDataBlock>;
                stream.WriteByte(9);
                stream.Write<NetEntityID>(NetEntityID.Get((UnityEngine.MonoBehaviour) targetCharacter), new object[0]);
                stream.WriteVector3(targetCharacter.transform.position);
                item2.itemRepresentation.ActionStream(1, uLink.RPCMode.Server, stream);
            }
            else if (currentEquippedItem is BowWeaponItem<BowWeaponDataBlock>)
            {
                BowWeaponItem<BowWeaponDataBlock> item3 = currentEquippedItem as BowWeaponItem<BowWeaponDataBlock>;
                stream.Write<NetEntityID>(NetEntityID.Get((UnityEngine.MonoBehaviour) targetCharacter), new object[0]);
                stream.Write<Vector3>(targetCharacter.transform.position, new object[0]);
                item3.itemRepresentation.ActionStream(2, uLink.RPCMode.Server, stream);
            }
            else
            {
                if (!(currentEquippedItem is BulletWeaponItem<ShotgunDataBlock>))
                {
                    return false;
                }
                BulletWeaponItem<ShotgunDataBlock> item4 = currentEquippedItem as BulletWeaponItem<ShotgunDataBlock>;
                for (int i = 0; i < item4.datablock.numPellets; i++)
                {
                    stream.WriteByte(9);
                    stream.Write<NetEntityID>(NetEntityID.Get((UnityEngine.MonoBehaviour) targetCharacter), new object[0]);
                    stream.WriteVector3(targetCharacter.transform.position);
                }
                item4.itemRepresentation.ActionStream(1, uLink.RPCMode.Server, stream);
            }
            return true;
        }

        private void Update()
        {
            if (ESP_UpdateOBJs.IsIngame)
            {
                this.Update_AimBot();
            }
        }

        private void Update_AimBot()
        {
            this.heliosBoxPos = Vector2.zero;
            if ((!ChatUI.IsVisible() && GUIAimbot.SetAimKey) && (Input.GetKey(CVars.Aimbot.AimKey) || CVars.Aimbot.AutoAim))
            {
                Character localCharacter = ESP_UpdateOBJs.LocalCharacter;
                Character closestToCrosshair = this.GetClosestToCrosshair();
                if (closestToCrosshair != null)
                {
                    if (!CVars.Aimbot.SilentAim)
                    {
                        this.heliosBoxPos = new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2));
                        this.AutoAimAtPlayer(localCharacter, closestToCrosshair);
                    }
                    else if (this.SilentAim(localCharacter, closestToCrosshair))
                    {
                        Vector3 position = Local.GetHeadBone(closestToCrosshair).transform.position;
                        Vector3 vector2 = Camera.main.WorldToScreenPoint(position);
                        if (vector2.z > 0f)
                        {
                            vector2.y = Screen.height - (vector2.y + 1f);
                            this.heliosBoxPos = new Vector2(vector2.x, vector2.y);
                        }
                    }
                }
            }
        }

        private bool ValidatePlayerClient_ForTarget(Character targetCharacter)
        {
            Character localCharacter = ESP_UpdateOBJs.LocalCharacter;
            if (!this.IsTargetInFOV(localCharacter, targetCharacter))
            {
                return false;
            }
            return (!CVars.Aimbot.VisibleCheck || this.IsTargetVisible(localCharacter, targetCharacter));
        }
    }
}

