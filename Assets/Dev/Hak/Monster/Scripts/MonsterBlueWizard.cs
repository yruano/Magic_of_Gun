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
        Patterns.Add(PatternAttack);
        Patterns.Add(PatternDefenseBuff);
        Patterns.Add(PatternDamageBuff);
        RandomPattern();
    }

    private void RandomPattern()
    {
        var Pattern = Patterns.OrderBy(x => Random.Range(0, Patterns.Count)).ToArray();

        NextPattern = Pattern[0];
    }

    private IEnumerator PatternAttack()
    {
        var bullet = Instantiate(P_Bullet, gameObject.transform.position, gameObject.transform.rotation);
        bullet.GetComponent<MonsterMagic>().Damage = Stats.Damage;

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    private IEnumerator PatternDefenseBuff()
    {
        Debug.Log("방어력 강화");
        Stats.Defense += 5;
        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    private IEnumerator PatternDamageBuff()
    {
        Debug.Log("대미지 강화");
        Stats.Damage += 5;
        _patternDone = true;
        RandomPattern();
        yield return null;
    }
}