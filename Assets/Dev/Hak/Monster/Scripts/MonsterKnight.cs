using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKnight : Monster
{
    public GameObject P_Bullet;
    public MonsterKnight()
    {
        Stats.DropItems = new[]
        {
            new MonsterDropItemData(1, ""),
        };
    }

    private void Start()
    {
        Weights = new List<int> { 2, 8 };

        Patterns.Add(PatternAttack);
        Patterns.Add(PatternRest);

        RandomPattern();
    }

    private void RandomPattern()
    {
        WeightedRandom weightedRandom = new WeightedRandom(Weights);
        int randomIndex = weightedRandom.GetRandomIndex();

        NextPattern = Patterns[randomIndex];
    }

    private IEnumerator PatternAttack()
    {
        Debug.Log("몬스터: 공격");

        var bullet = Instantiate(P_Bullet, gameObject.transform.position, gameObject.transform.rotation);
        bullet.GetComponent<MonsterMagic>().Damage = Stats.Damage;

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
