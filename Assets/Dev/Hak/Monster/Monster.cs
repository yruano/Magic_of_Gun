using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterStat
{
    [SerializeField]
    public float HP;
    [SerializeField]
    public float Damage;
    [SerializeField]
    public float Defense;

    public MonsterStat()
    {
        HP = 10f;
        Damage = 10f;
        Defense = 5f;
    }
}
public class Monster : MonoBehaviour, IDamageable
{
    private Func<IEnumerator> NextPattern;
    private Coroutine CurrentPattern = null;
    public MonsterStat stat;
    private void Start()
    {
        NextPattern = PatternAttack;
    }

    private void Update()
    {
        if (CurrentPattern is null && NextPattern is not null && Input.GetKeyDown(KeyCode.W))
            CurrentPattern = StartCoroutine(NextPattern());
    }

    private IEnumerator PatternAttack()
    {
        CurrentPattern = null;
        NextPattern = PatternSkill;
        yield return null;
    }

    private IEnumerator PatternSkill()
    {
        CurrentPattern = null;
        yield return null;
    }

    public void Damage(int damage)
    {
        stat.HP -= damage;

        if (stat.HP == 0)
        {
            Destroy(gameObject);
        }
    }
}
