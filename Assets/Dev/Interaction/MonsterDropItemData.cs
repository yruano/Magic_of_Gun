using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public struct MonsterDropItemData : Utils.IWeighted
{
    public readonly int RandomWeight;
    public readonly string Path;

    public int Weight => RandomWeight;

    public MonsterDropItemData(int randomWeight, string path)
    {
        RandomWeight = randomWeight;
        Path = path;
    }
}

public static class MonsterDropItem
{
    public static void SpawnDropItem(MonsterDropItemData[] DropItems, Vector3 pos)
    {
        var itemPicker = new Utils.RandomWeightedPicker<MonsterDropItemData>(DropItems);
        var itemData = itemPicker.PickAnItem();

        if (string.IsNullOrEmpty(itemData.Path))
            return;

        Addressables.InstantiateAsync(itemData.Path, pos, Quaternion.identity).Completed +=
            (AsyncOperationHandle<GameObject> itemHandle) =>
            {
                if (itemHandle.Status is not AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError($"Addressables.InstantiateAsync failed! (Item path: {itemData.Path})");
                    return;
                }
            };
    }
}