using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    [SerializeField]
    public int MaxHP;
    [SerializeField]
    public int HP;
    [SerializeField]
    public int MaxMP;
    [SerializeField]
    public int MP;
    [SerializeField]
    public int Shield;
    [SerializeField]
    public int Shield_Increment;

    public PlayerStats()
    {
        MaxHP = 100;
        HP = MaxHP;
        MaxMP = 100;
        MP = 100;
        Shield = 0;
        Shield_Increment = 10;
    }
}

public class Player : MonoBehaviour, IDamageable, IInventorySetter
{
    public Rigidbody2D Rd2d;
    public PlayerStats Stats;
    public PlayerItemTable PlayerItemTable = new();
    public InventoryManager InventoryManager = new();
    public Item TestItem;
    public Transform ItemContent;
    public GameObject P_InventoryItem;

    private void Awake()
    {
        Rd2d = GetComponent<Rigidbody2D>();
        Stats = new();
    }

    protected virtual void Obscuration()
    {
        Stats.Shield += Stats.Shield_Increment;
        Debug.Log(Stats.Shield);
    }

    //데미지 피격구현
    public void Damage(int damage)
    {
        Debug.Log("플레이어 데미지 : " + damage + "받았습니다.");
        if (Stats.Shield == 0)
        {
            Stats.HP -= damage;
        }
        else
        {
            if (Stats.Shield >= damage)
            {
                Stats.Shield -= damage;
            }
            else
            {
                damage -= Stats.Shield;
                Stats.Shield = 0;
                Stats.HP -= damage;
            }
        }

        Debug.Log("플레이어 남은 HP : " + Stats.HP);
        Debug.Log("플레이어 남은 Shield : " + Stats.Shield);
        if (Stats.HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heel(int heel)
    {
        if (Stats.HP + heel >= Stats.MaxHP)
        {
            Stats.HP = Stats.MaxHP;
        }
        else
        {
            Stats.HP += heel;
        }
    }

    public void SetItem(Item item)
    {
        Debug.Log("인벤토리에 추가됨!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        // GameObject obj = Instantiate(P_InventoryItem, ItemContent);
        // TestItem = PlayerItemTable.AddItem(item);
        // InventoryManager.InventoryTables(obj, TestItem);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {

            GameObject obj = Instantiate(P_InventoryItem, ItemContent);
            TestItem = PlayerItemTable.AddItem(other.gameObject.GetComponent<TestItem>());
            InventoryManager.InventoryTables(obj, TestItem);

            if (other.gameObject.GetComponent<TestItem>() == null)
            {
                Item item = other.gameObject.GetComponent<Item>();
                SetItem(item);
            }

            Destroy(other);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject obj = Instantiate(P_InventoryItem, ItemContent);
            TestItem = PlayerItemTable.AddItem();
            InventoryManager.InventoryTables(obj, TestItem);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerItemTable.RemoveItem(TestItem.ComputeHash());
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Obscuration();
        }
    }
}
