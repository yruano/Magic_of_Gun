using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterStats
{
    [SerializeField]
    public int HP;
    [SerializeField]
    public int Damage;
    [SerializeField]
    public int Defense;
    public int MaxHP;
    public MonsterDropItemData[] DropItems;

    public MonsterStats()
    {
        MaxHP = 10;
        HP = MaxHP;
        Damage = 10;
        Defense = 0;
    }
}
public class Monster : MonoBehaviour, IDamageable, ITurn
{
    protected Func<IEnumerator> NextPattern;
    protected Coroutine CurrentPattern = null;
    protected bool _patternDone = true;
    public List<Func<IEnumerator>> Patterns = new();
    public List<int> Weights = new();

    public MonsterStats Stats = new();

    protected virtual void Update()
    {
        if (_patternDone is true && NextPattern is not null && Input.GetKeyDown(KeyCode.W))
        {
            _patternDone = false;
            CurrentPattern = StartCoroutine(NextPattern());
        }
    }

    public virtual void Damage(int damage)
    {
        if (Stats.Defense == 0)
        {
            Stats.HP -= damage;
        }
        else
        {
            if (Stats.Defense >= damage)
            {
                Stats.Defense -= damage;
            }
            else
            {
                damage -= Stats.Defense;
                Stats.Defense = 0;
                Stats.HP -= damage;
            }
        }

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

    public void Turn()
    {
        _patternDone = false;
        CurrentPattern = StartCoroutine(NextPattern());
    }
}
