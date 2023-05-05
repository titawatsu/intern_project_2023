using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSo : ScriptableObject
{
    public enum weaponType
    {
        Pistol,
        Shotgun,
        Rifle,
        Sniper,
        Launcher
    }

    public bool isAuto;
    public bool isBulletSpread;

    public int ammoType;
    public int ammoDamage;
    public int ammoSpeed;
    public int ammoCapacity;

    public GameObject prefab;
}
