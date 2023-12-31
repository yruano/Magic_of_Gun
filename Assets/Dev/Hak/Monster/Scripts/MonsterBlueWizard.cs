using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterBlueWizard : Monster
{
    public GameObject P_Bullet;
    public MonsterBlueWizard()
    {
        Stats.DropItems = new[]
        {
            new MonsterDropItemData(1, ""),
        };
    }

    private void Start()
    {
        Weights = new List<int> { 5, 4, 3, 2 };

        Patterns.Add(PatternAttack);
        Patterns.Add(PatternRest);
        Patterns.Add(PatternDefenseBuff);
        Patterns.Add(PatternDamageBuff);

        RandomPattern();
    }

    private IEnumerator PatternAttack()
    {
        Debug.Log("몬스터: 공격");

        var bullet = Instantiate(P_Bullet, gameObject.transform.position, gameObject.transform.rotation);
        bullet.GetComponent<MonsterMagic>().Damage = Stats.Damage;
        bullet.GetComponent<MonsterMagic>().Speed = 5f;

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    private IEnumerator PatternDefenseBuff()
    {
        Debug.Log("몬스터: 방어력 강화");
        Stats.Defense += 5;

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    private IEnumerator PatternDamageBuff()
    {
        Debug.Log("몬스터: 대미지 강화");
        Stats.Damage += 5;

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    private IEnumerator PatternRest()
    {
        Debug.Log("몬스터: 휴식");

        _patternDone = true;
        RandomPattern();
        yield return null;
    }
}