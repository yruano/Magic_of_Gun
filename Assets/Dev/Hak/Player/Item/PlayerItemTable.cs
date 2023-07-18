using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemTable
{
    public Dictionary<Hash128, Item> ItemTable = new();

    public Item AddItem(int Id)
    {
        var item = new Item();
        item.BaseData.Name = "몰라";
        item.BaseData.Desc = "테스트";
        item.BaseData.ItemType = "글자";

        var exbulletdata = new ItemBulletData();
        exbulletdata.BulletBaseData.Damage = 10;
        exbulletdata.BulletBaseData.PartType = "탄두";
        exbulletdata.BulletBaseData.Percent = 1.0f;

        item.ItemData = exbulletdata;

        Hash128 hash = item.ComputeHash();
        if (ItemTable.ContainsKey(hash))
        {
            Debug.Log("이미 있습니다.");
        }
        else
        {
            Debug.Log("새로운 아이템 추가.");
            ItemTable.Add(hash, item);
        }

        return item;
    }

    public void RemoveItem(Hash128 hash)
    {
        if (ItemTable.ContainsKey(hash))
        {
            Debug.Log("이미 있습니다. 제거");
            ItemTable.Remove(hash);
        }
        else
        {
            Debug.Log("없습니다. 제거 불가");
        }
    }
}
