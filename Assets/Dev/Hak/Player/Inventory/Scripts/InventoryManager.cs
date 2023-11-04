using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// 현재는 구글 시트의 값이 아니라 테스트를 위해 만들어둔 상황이고
// 실재 구글 시트를 이용 할 때는 게임을 실행 할때 로딩 상황에서
// 구글 시트의 값들을 미리 정장해둘 곳을 만들어 둔다
// 아이템 오브젝트에 해쉬값을 두고 해쉬 값을 이용해서 아이템을 인벤토리에 추가할 것이다
public class InventoryManager
{
    public void InventoryTables(GameObject obj, Item item)
    {
        var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

        itemName.text = item.BaseData.Name;
        itemIcon.sprite = item.BaseData.Image;
    }
}