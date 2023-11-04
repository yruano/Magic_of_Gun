using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemTable
{
    public Dictionary<Hash128, Item> ItemTable = new();

    public Item AddItem(TestItem obj = null)
    {
        var item = new Item();
        
        item.BaseData.Name = obj.Name;
        item.BaseData.Desc = obj.Desc;
        item.BaseData.ItemType = obj.ItemType;
        item.BaseData.Image = obj.Image;

        switch (obj.ItemType)
        {
            case "총알":
                var exbulletdata = new ItemBulletData();
                exbulletdata.BulletBaseData.Damage = obj.Damage;
                exbulletdata.BulletBaseData.PartType = obj.PartType;
                exbulletdata.BulletBaseData.Percent = obj.Percent;
                item.ItemData = exbulletdata;
                break;

            default:
                break;
        }

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
