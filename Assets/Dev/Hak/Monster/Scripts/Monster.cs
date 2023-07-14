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
    public MonsterDropItemData[] DropItems;

    public MonsterStats()
    {
        HP = 10;
        Damage = 10;
        Defense = 0;
    }
}
public class Monster : MonoBehaviour, IDamageable
{
    protected Func<IEnumerator> NextPattern;
    protected Coroutine CurrentPattern = null;
    protected bool _patternDone = true;
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
        Stats.HP -= damage;

        if (Stats.HP == 0)
        {
            Destroy(gameObject);
        }
    }
}
