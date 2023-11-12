using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject ItemTable;
    public List<GameObject> ShopSlots = new();
    void Start()
    {
        var table = ItemTable.GetComponent<ItemTable>();
        if (table.Table.Count != 0)
        {
            foreach (var slot in ShopSlots)
            {
                var slotIcon = slot.transform.Find("ItemIcon").GetComponent<Image>();
                var slotName = slot.transform.Find("ItemName").GetComponent<Text>();
                var slotPrice = slot.transform.Find("ItemPrice").GetComponent<Text>();

                var index = Random.Range(0, table.KeyTable.Count);

                slot.name = table.KeyTable[index].ToString();
                slotIcon.sprite = table.Table[table.KeyTable[index]].BaseData.Image;
                slotName.text = table.Table[table.KeyTable[index]].BaseData.Name;
                slotPrice.text = table.Table[table.KeyTable[index]].BaseData.Price;
            }
        }
    }
}