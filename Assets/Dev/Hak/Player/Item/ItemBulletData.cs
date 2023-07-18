using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential)]
public struct ItemBulletBaseData
{
    public int Damage;
    public string PartType;
    public float Percent;
}
public class ItemBulletData : ItemData
{
    public ItemBulletBaseData BulletBaseData;

    public override Hash128 ComputeHash()
    {
        Hash128 hash = new();
        HashUtilities.ComputeHash128(ref BulletBaseData, ref hash);
        return hash;
    }
}
