using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public List<GameObject> ShopSlots = new();

    public void SetSlotsItem(Collider2D other)
    {
        var getItem = other.GetComponent<IItemGetter>();
        foreach (var slot in ShopSlots)
        {
            getItem?.GetItem(slot);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SetSlotsItem(other);
    }
}