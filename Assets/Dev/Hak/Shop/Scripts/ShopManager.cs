using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Transform Content;
    public List<GameObject> ShopSlots = new();
    public GameObject P_Slots;
    private Collider2D Player;
    private Collider2D ItemTable;

    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            var slot = Instantiate(P_Slots, Content);
            slot.AddComponent<Button>();
            var button = slot.GetComponent<Button>();

            ShopSlots.Add(slot);
            button.onClick.AddListener(() => SetInventory());
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

    public void SetInventory()
    {
        Debug.Log("버튼 눌림!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        GameObject slot = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        var setItem = Player.GetComponent<IInventorySetter>();
        setItem?.SetItem(slot.GetComponent<SlotItemData>().Slot);
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