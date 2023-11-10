using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTable : MonoBehaviour
{
    public List<Hash128> KeyTable = new();
    public Dictionary<Hash128, Item> Table = new();
}