using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public List<GameObject> ShopSlots = new();
    public Collider2D Player = new();
    public Collider2D ItemTable = new();

    void Start()
    {
        foreach (var slot in ShopSlots)
        {
            var button = slot.GetComponent<Button>();

            button.onClick.AddListener(() => SetInventory(slot.GetComponent<SlotItemData>()));
        }
    }

    public void GetSlotsItem()
    {
        var getItem = ItemTable.GetComponent<IItemGetter>();
        foreach (var slot in ShopSlots)
        {
            getItem?.GetItem(slot);
        }
    }

    public void SetInventory(SlotItemData slot)
    {
        Debug.Log("버튼 눌림!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        var setItem = Player.GetComponent<IInventorySetter>();
        setItem?.SetItem(slot.Slot);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player = other;
        }
        else if (other.CompareTag("ItemTable"))
        {
            ItemTable = other;
            GetSlotsItem();
        }
    }
}