using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterStats
{
    [SerializeField]
    public float HP;
    [SerializeField]
    public float Damage;
    [SerializeField]
    public float Defense;
    public MonsterDropItemData[] DropItems;

    public MonsterStats()
    {
        HP = 10f;
        Damage = 10;
        Defense = 5f;
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
