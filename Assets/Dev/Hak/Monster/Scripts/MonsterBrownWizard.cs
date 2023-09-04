using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterBrownWizardStats
{
    public int CounterAttack;
    public bool Counter;
    public MonsterBrownWizardStats()
    {
        CounterAttack = 0;
        Counter = false;
    }
}

public class MonsterBrownWizard : Monster
{
    public GameObject P_Bullet;
    private int _maxCount;
    private int _patternCount;
    private MonsterBrownWizardStats _brownwizardstats = new();
    public MonsterBrownWizard()
    {
        Stats.DropItems = new[]
        {
            new MonsterDropItemData(1, ""),
        };
    }

    private void Start()
    {
        Weights = new List<int> { 6, 3 };

        _patternCount = 0;
        _maxCount = 5;

        Patterns.Add(PatternRest);
        Patterns.Add(PatternDefenseBuff);
        NextPattern = PatternCounterAttack;
    }

    private void RandomPattern()
    {
        WeightedRandom weightedRandom = new WeightedRandom(Weights);
        int randomIndex = weightedRandom.GetRandomIndex();

        NextPattern = Patterns[randomIndex];
    }

    private IEnumerator PatternCounterAttack()
    {
        Debug.Log("카운터 시작");
        _brownwizardstats.Counter = true;

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    private IEnumerator PatternAttack()
    {
        Debug.Log("공격");
        var bullet = Instantiate(P_Bullet, gameObject.transform.position, gameObject.transform.rotation);
        bullet.GetComponent<MonsterMagic>().Damage = Stats.Damage;
        _patternCount = 0;

        _patternDone = true;
        NextPattern = PatternCounterAttack;
        yield return null;
    }

    private IEnumerator PatternDefenseBuff()
    {
        Debug.Log("방어력 강화");
        Stats.Defense += 5;


        if (_maxCount == _patternCount) { NextPattern = PatternAttack; }
        else { RandomPattern(); }
        _patternCount += 1;

        _patternDone = true;
        yield return null;
    }

    private IEnumerator PatternRest()
    {
        Debug.Log("휴식");


        if (_maxCount == _patternCount) { NextPattern = PatternAttack; }
        else { RandomPattern(); }
        _patternCount += 1;

        _patternDone = true;
        yield return null;
    }

    public override void Damage(int damage)
    {
        Debug.Log("몬스터 : " + damage + "를 받았습니다.");

        if (_brownwizardstats.Counter is true)
        {
            _brownwizardstats.CounterAttack += damage;
            var bullet = Instantiate(P_Bullet, gameObject.transform.position, gameObject.transform.rotation);
            bullet.GetComponent<MonsterMagic>().Damage = _brownwizardstats.CounterAttack;
        }

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
}