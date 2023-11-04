using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential)]
public struct ItemHeavyWeaponsBaseData
{
    public int Damage;
    public int Count;
    public string PartType;
    public string Element;
}

public class ItemHeavyWeaponsData : ItemData
{
    public ItemHeavyWeaponsBaseData HeavyWeaponsBaseData;
    public override Hash128 ComputeHash()
    {
        Hash128 hash = new();
        HashUtilities.ComputeHash128(ref HeavyWeaponsBaseData, ref hash);
        return hash;
    }
}
