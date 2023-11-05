using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential)]
public struct ItemConsumptionBaseData
{
    public int Count;
    public int Heel;
    public float Percent;
    public string PartType;
    public string Element;
}
public class ItemConsumptionData : ItemData
{
    ItemConsumptionBaseData ConsumptionBaseData;
    public override Hash128 ComputeHash()
    {
        Hash128 hash = new();
        HashUtilities.ComputeHash128(ref ConsumptionBaseData, ref hash);
        return hash;
    }
}
