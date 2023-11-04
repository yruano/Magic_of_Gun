using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential)]
public struct ItemGunBaseData
{
    public int Damage;
    public string PartType;
    public float Percent;
    public string Element;
}
public class ItemGunData : ItemData
{
    ItemGunBaseData GunBaseData;
    public override Hash128 ComputeHash()
    {
        Hash128 hash = new();
        HashUtilities.ComputeHash128(ref GunBaseData, ref hash);
        return hash;
    }
}
