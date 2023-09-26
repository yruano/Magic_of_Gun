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
    [SerializeField]
    public int MaxHP;
    [SerializeField]
    public MonsterDropItemData[] DropItems;

    public MonsterStats()
    {
        MaxHP = 1000;
        HP = MaxHP;
        Damage = 10;
        Defense = 20;
    }
}
public class Monster : MonoBehaviour, IDamageable, ITurn
{
    protected Func<IEnumerator> _nextPattern;
    protected Coroutine _currentPattern = null;
    protected bool _patternDone = true;
    public List<Func<IEnumerator>> Patterns = new();
    public List<int> Weights = new();
    public MonsterStats Stats = new();

    protected virtual void Update()
    {
        if (_patternDone is true && _nextPattern is not null && Input.GetKeyDown(KeyCode.W))
        {
            _patternDone = false;
            _currentPattern = StartCoroutine(_nextPattern());
        }
    }

    public virtual void Damage(int damage)
    {
        Debug.Log("몬스터 : " + damage + "를 받았습니다.");

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

        Debug.Log("몬스터 남은 Defense : " + Stats.Defense);
        Debug.Log("몬스터 남은 HP : " + Stats.HP);

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

    public virtual void Turn()
    {
        if (_patternDone is true && _nextPattern is not null)
        {
            _patternDone = false;
            _currentPattern = StartCoroutine(_nextPattern());
        }
    }
}
