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

    public PlayerStats()
    {
        MaxHP = 100;
        HP = 100;
        MaxMP = 100;
        MP = 100;
        Shield = 0;
    }
}

public class Player : MonoBehaviour, IDamageable
{
    public Rigidbody2D Rd2d;
    public PlayerStats Stats;
    public PlayerItemTable ItemTable = new();
    public Item TestItem;

    private void Awake()
    {
        Rd2d = GetComponent<Rigidbody2D>();
        Stats = new();
    }

    //데미지 피격구현
    public void Damage(int damage)
    {
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

        Debug.Log(Stats.HP);
        if (Stats.HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            TestItem = ItemTable.AddItem(0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            TestItem = ItemTable.AddItem(0);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ItemTable.RemoveItem(TestItem.ComputeHash());
        }
    }
}
