using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class ItemTable : MonoBehaviour, IItemGetter, IItemSearch
{
    public List<Hash128> KeyTable = new();
    public Dictionary<Hash128, Item> Table = new();

    public void GetItem(GameObject obj)
    {
        if (Table.Count != 0)
        {
            var slotIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var slotName = obj.transform.Find("ItemName").GetComponent<Text>();
            var slotPrice = obj.transform.Find("ItemPrice").GetComponent<Text>();
            var slotdata = obj.GetComponent<SlotItemData>();

            var index = Random.Range(0, KeyTable.Count);

            obj.name = KeyTable[index].ToString();
            slotIcon.sprite = Table[KeyTable[index]].BaseData.Image;
            slotName.text = Table[KeyTable[index]].BaseData.Name;
            slotPrice.text = Table[KeyTable[index]].BaseData.Price;
            slotdata.Slot = KeyTable[index];
        }
        else
        {
            int val = Table.Count;
            string msg = "Table의 사이즈";
            Debug.LogError($"변수 값: {val}, 메시지: {msg}");
        }
    }

    public Item ItemSearch(Hash128 key)
    {
        return Table[key];
    }
}