namespace DizzyHacks.Hacks
{
    using DizzyHacks.Settings;
    using Facepunch;
    using Rust;
    using System;
    using System.Collections.Generic;
    using uLink;
    using UnityEngine;

    internal class Misc : UnityEngine.MonoBehaviour
    {
        private CCMotor.Jumping? defaultJumping = null;
        private CCMotor.Movement? defaultMovement = null;
        private GameObject lightGameObject;
        private bool AUTO_WOOD_GATHER = false;
        private float LAST_ATTACK_TIME = 0f;

        private float defaultBulletRange = 0f;
        private float defaultRecoilPitchMin = 0f;
        private float defaultRecoilPitchMax = 0f;
        private float defaultRecoilYawMin = 0f;
        private float defaultRecoilYawMax = 0f;
        private float defaultAimSway = 0f;
        private float defaultAimSwaySpeed = 0f;
        private float defaultAimRotationScalar = 0f;
        private float defaultViewModelRotationScalar = 0f;

        public static void AllBlueprints()
        {
            PlayerInventory component = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>().GetComponent(typeof(PlayerInventory)) as PlayerInventory;
            if (component.dirtySlotCount != 0)
            {
                System.Collections.Generic.List<BlueprintDataBlock> boundBPs = component.GetBoundBPs();
                foreach (BlueprintDataBlock block in Bundling.LoadAll<BlueprintDataBlock>())
                {
                    if (!boundBPs.Contains(block))
                    {
                        Notice.Inventory(" ", block.name);
                        boundBPs.Add(block);
                    }
                }
            }
        }

        public void AutoWood()
        {
            if (CVars.Misc.AutoWood)
            {
                Character component = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                Inventory inventory = component.GetComponent(typeof(Inventory)) as Inventory;
                MeleeWeaponItem<MeleeWeaponDataBlock> item = inventory._activeItem as MeleeWeaponItem<MeleeWeaponDataBlock>;
                if (((inventory._activeItem is MeleeWeaponItem<MeleeWeaponDataBlock>) && ((Time.time - this.LAST_ATTACK_TIME) > item.datablock.fireRate)) && this.AUTO_WOOD_GATHER)
                {
                    RaycastHit2 hit;
                    this.LAST_ATTACK_TIME = Time.time;
                    ItemRepresentation itemRepresentation = item.itemRepresentation;
                    IMeleeWeaponItem iface = item.iface as IMeleeWeaponItem;
                    bool flag = Physics2.Raycast2(component.eyesRay, out hit, item.datablock.range, 0x183e1411);
                    Vector3 point = hit.point;
                    itemRepresentation.Action(3, uLink.RPCMode.Server);
                    uLink.BitStream stream = new uLink.BitStream(false);
                    if (flag)
                    {
                        HUDHitIndicator indicator;
                        IDMain idMain = hit.idMain;
                        stream.WriteBoolean(true);
                        stream.Write<NetEntityID>(NetEntityID.Get((UnityEngine.MonoBehaviour) idMain), new object[0]);
                        stream.WriteVector3(point);
                        stream.WriteBoolean(false);
                        itemRepresentation.ActionStream(1, uLink.RPCMode.Server, stream);
                        if (Bundling.Load<HUDHitIndicator>("content/hud/HUDHitIndicator", out indicator))
                        {
                            HUDHitIndicator.CreateIndicator(point, true, indicator);
                        }
                    }
                    else
                    {
                        stream.WriteBoolean(false);
                        stream.WriteVector3(Vector3.zero);
                        stream.WriteBoolean(true);
                        itemRepresentation.ActionStream(1, uLink.RPCMode.Server, stream);
                    }
                }
            }
        }

        private void LightHack()
        {
            this.lightGameObject.SetActive(CVars.Misc.LightHack);
            this.lightGameObject.transform.position = Camera.main.transform.position;
        }

        private void MotorHacks()
        {
            HumanController localController = ESP_UpdateOBJs.LocalController;
            Character localCharacter = ESP_UpdateOBJs.LocalCharacter;
            CCMotor ccmotor = localController.ccmotor;
            if (ccmotor != null)
            {
                if (!this.defaultJumping.HasValue)
                {
                    this.defaultJumping = new CCMotor.Jumping?(ccmotor.jumping.setup);
                }
                else
                {
                    //ccmotor.minTimeBetweenJumps = 0.1f;
                    ccmotor.jumping.setup.baseHeight = this.defaultJumping.Value.baseHeight * CVars.Misc.JumpModifer;
                }
                if (!this.defaultMovement.HasValue)
                {
                    this.defaultMovement = new CCMotor.Movement?(ccmotor.movement.setup);
                }
                else
                {
                    ccmotor.movement.setup.maxForwardSpeed = (this.defaultMovement.Value.maxForwardSpeed * CVars.Misc.SpeedModifer) / 10f;
                    ccmotor.movement.setup.maxSidewaysSpeed = (this.defaultMovement.Value.maxSidewaysSpeed * CVars.Misc.SpeedModifer) / 10f;
                    ccmotor.movement.setup.maxBackwardsSpeed = (this.defaultMovement.Value.maxBackwardsSpeed * CVars.Misc.SpeedModifer) / 10f;
                    ccmotor.movement.setup.maxGroundAcceleration = (this.defaultMovement.Value.maxGroundAcceleration * CVars.Misc.SpeedModifer) / 10f;
                    ccmotor.movement.setup.maxAirAcceleration = (this.defaultMovement.Value.maxAirAcceleration * CVars.Misc.SpeedModifer) / 10f;
                    if (CVars.Misc.NoFallDamage)
                    {
                        ccmotor.movement.setup.maxFallSpeed = 17f;
                    }
                    else
                    {
                        ccmotor.movement.setup.maxFallSpeed = this.defaultMovement.Value.maxFallSpeed;
                    }
                }
                if (CVars.Misc.FlyHack)
                {
                    ccmotor.velocity = Vector3.zero;
                    Vector3 forward = localCharacter.eyesAngles.forward;
                    Vector3 right = localCharacter.eyesAngles.right;
                    if (!ChatUI.IsVisible())
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            ccmotor.velocity += forward * (ccmotor.movement.setup.maxForwardSpeed * 3f);
                        }
                        if (Input.GetKey(KeyCode.S))
                        {
                            ccmotor.velocity += forward * (ccmotor.movement.setup.maxBackwardsSpeed * 3f);
                        }
                        if (Input.GetKey(KeyCode.A))
                        {
                            ccmotor.velocity += forward * (ccmotor.movement.setup.maxSidewaysSpeed * 3f);
                        }
                        if (Input.GetKey(KeyCode.D))
                        {
                            ccmotor.velocity += forward * (ccmotor.movement.setup.maxSidewaysSpeed * 3f);
                        }
                        if (Input.GetKey(KeyCode.Space))
                        {
                            ccmotor.velocity += Vector3.up * (this.defaultMovement.Value.maxAirAcceleration * 3f);
                        }
                    }
                    if (ccmotor.velocity == Vector3.zero)
                    {
                        ccmotor.velocity += Vector3.up * ((ccmotor.settings.gravity * Time.deltaTime) * 0.5f);
                    }
                }
            }
        }

        private void NoRecoil()
        {
            if (CVars.Misc.NoRecoil)
            {
                HumanController localController = ESP_UpdateOBJs.LocalController;
                InventoryItem currentEquippedItem = Local.GetCurrentEquippedItem(localController);
                if ((currentEquippedItem != null) && !(currentEquippedItem is MeleeWeaponItem<MeleeWeaponDataBlock>))
                {
                    BulletWeaponItem<BulletWeaponDataBlock> item2 = currentEquippedItem as BulletWeaponItem<BulletWeaponDataBlock>;
                    if (item2 != null)
                    {
                        defaultBulletRange = item2.datablock.bulletRange;
                        defaultRecoilPitchMin = item2.datablock.recoilPitchMin;
                        defaultRecoilPitchMax = item2.datablock.recoilPitchMax;
                        defaultRecoilYawMin = item2.datablock.recoilYawMin;
                        defaultRecoilYawMax = item2.datablock.recoilYawMax;
                        defaultAimSway = item2.datablock.aimSway;
                        defaultAimSwaySpeed = item2.datablock.aimSwaySpeed;

                        //item2.datablock.bulletRange = 300f;
                        item2.datablock.recoilPitchMin = 0f;
                        item2.datablock.recoilPitchMax = 0f;
                        item2.datablock.recoilYawMin = 0f;
                        item2.datablock.recoilYawMax = 0f;
                        item2.datablock.aimSway = 0f;
                        item2.datablock.aimSwaySpeed = 0f;
                    }
                }
                CameraMount componentInChildren = localController.GetComponentInChildren<CameraMount>();
                if (componentInChildren != null)
                {
                    HeadBob component = componentInChildren.GetComponent<HeadBob>();
                    if (component != null)
                    {
                        defaultAimRotationScalar = component.aimRotationScalar;
                        defaultViewModelRotationScalar = component.viewModelRotationScalar;
                        component.aimRotationScalar = 0f;
                        component.viewModelRotationScalar = 0f;
                    }
                }
            }
            else
            {
                if(defaultBulletRange == 0f)
                {
                    HumanController localController = ESP_UpdateOBJs.LocalController;
                    InventoryItem currentEquippedItem = Local.GetCurrentEquippedItem(localController);
                    if ((currentEquippedItem != null) && !(currentEquippedItem is MeleeWeaponItem<MeleeWeaponDataBlock>))
                    {
                        BulletWeaponItem<BulletWeaponDataBlock> item2 = currentEquippedItem as BulletWeaponItem<BulletWeaponDataBlock>;
                        if (item2 != null)
                        {
                            defaultBulletRange = item2.datablock.bulletRange;
                            defaultRecoilPitchMin = item2.datablock.recoilPitchMin;
                            defaultRecoilPitchMax = item2.datablock.recoilPitchMax;
                            defaultRecoilYawMin = item2.datablock.recoilYawMin;
                            defaultRecoilYawMax = item2.datablock.recoilYawMax;
                            defaultAimSway = item2.datablock.aimSway;
                            defaultAimSwaySpeed = item2.datablock.aimSwaySpeed;
                        }
                    }
                    CameraMount componentInChildren = localController.GetComponentInChildren<CameraMount>();
                    if (componentInChildren != null)
                    {
                        HeadBob component = componentInChildren.GetComponent<HeadBob>();
                        if (component != null)
                        {
                            defaultAimRotationScalar = component.aimRotationScalar;
                            defaultViewModelRotationScalar = component.viewModelRotationScalar;
                        }
                    }
                }
                else
                {
                    HumanController localController = ESP_UpdateOBJs.LocalController;
                    InventoryItem currentEquippedItem = Local.GetCurrentEquippedItem(localController);
                    if ((currentEquippedItem != null) && !(currentEquippedItem is MeleeWeaponItem<MeleeWeaponDataBlock>))
                    {
                        BulletWeaponItem<BulletWeaponDataBlock> item2 = currentEquippedItem as BulletWeaponItem<BulletWeaponDataBlock>;
                        if (item2 != null)
                        {
                            //item2.datablock.bulletRange = defaultBulletRange;
                            item2.datablock.recoilPitchMin = defaultRecoilPitchMin;
                            item2.datablock.recoilPitchMax = defaultRecoilPitchMax;
                            item2.datablock.recoilYawMin = defaultRecoilYawMin;
                            item2.datablock.recoilYawMax = defaultRecoilYawMax;
                            item2.datablock.aimSway = defaultAimSway;
                            item2.datablock.aimSwaySpeed = defaultAimSwaySpeed;
                        }
                    }
                    CameraMount componentInChildren = localController.GetComponentInChildren<CameraMount>();
                    if (componentInChildren != null)
                    {
                        HeadBob component = componentInChildren.GetComponent<HeadBob>();
                        if (component != null)
                        {
                            component.aimRotationScalar = defaultAimRotationScalar;
                            component.viewModelRotationScalar = defaultViewModelRotationScalar;
                        }
                    }
                }
            }
        }

        private void NoReload()
        {
            if (CVars.Misc.NoReload)
            {
                InventoryItem currentEquippedItem = Local.GetCurrentEquippedItem(ESP_UpdateOBJs.LocalController);
                if ((currentEquippedItem != null) && (currentEquippedItem is BulletWeaponItem<BulletWeaponDataBlock>))
                {
                    BulletWeaponItem<BulletWeaponDataBlock> item2 = currentEquippedItem as BulletWeaponItem<BulletWeaponDataBlock>;
                    if (item2.clipAmmo <= 1)
                    {
                        item2.itemRepresentation.Action(3, uLink.RPCMode.Server);
                    }
                }
            }
        }

        private void Start()
        {
            this.lightGameObject = new GameObject();
            Light light = this.lightGameObject.AddComponent<Light>();
            light.type = LightType.Point;
            light.range = 1000f;
            light.intensity = 1f;
            light.color = Color.white;
            this.lightGameObject.SetActive(false);
            DontDestroyOnLoad(this.lightGameObject);
        }

        private void Update()
        {
            if (ESP_UpdateOBJs.IsIngame)
            {
                try
                {
                    this.LightHack();
                    this.MotorHacks();
                    this.NoRecoil();
                    this.NoReload();
                    this.AutoWood();
                }
                catch
                {
                }
            }
        }
    }
}

