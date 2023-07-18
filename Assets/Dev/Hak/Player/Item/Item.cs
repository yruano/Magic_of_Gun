using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential)]
public struct ItemBaseData
{
    public string Name;
    public string Desc;
    public string ItemType;
    public Sprite Image;
}

public class Item
{
    public ItemBaseData BaseData;
    public ItemData ItemData;
    public int Count;

    public Hash128 ComputeHash()
    {
        Hash128 hash = new();
        HashUtilities.ComputeHash128(ref BaseData, ref hash);

        Hash128 hash2 = ItemData.ComputeHash();

        HashUtilities.AppendHash(ref hash2, ref hash);
        return hash;
    }
}
