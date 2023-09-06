using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBossWizard : Monster
{
    public GameObject P_Bullet;
    private bool _attributeCounter;
    private bool _chargeAttack;
    private int _attributeCounterCount;
    private int _maxCount;
    private int _charge;
    private List<string> _attributes;
    private string _attribute = "";
    public MonsterBossWizard()
    {
        Stats.DropItems = new[]
        {
            new MonsterDropItemData(1, ""),
        };
    }

    private void Start()
    {
        Weights = new List<int> { 5, 4, 3, 2, 1 };
        _attributes = new List<string> { "Fire", "Water", "Earth", "Wind" };

        Patterns.Add(PatternAttack);
        Patterns.Add(PatternRest);
        Patterns.Add(PatternAttributeCounter);
        Patterns.Add(PatternCharge);
        Patterns.Add(PatternDeleteBullet);

        _attributeCounter = false;
        _chargeAttack = false;
        _charge = 0;
        _attributeCounterCount = 0;
        _maxCount = 5;

        RandomPattern();
    }

    private void RandomPattern()
    {
        WeightedRandom weightedRandom = new WeightedRandom(Weights);
        int randomIndex = weightedRandom.GetRandomIndex();

        if (_chargeAttack)
        {
            if (_charge == 0)
            {
                NextPattern = PatternChargeAttack;
            }
            else
            {
                NextPattern = PatternRest;
            }
        }
        else
        {
            NextPattern = Patterns[randomIndex];
        }
    }

    private IEnumerator PatternAttack()
    {
        Debug.Log("보스: 공격");
        var bullet = Instantiate(P_Bullet, gameObject.transform.position, gameObject.transform.rotation);
        bullet.GetComponent<MonsterMagic>().Damage = Stats.Damage;

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    private IEnumerator PatternRest()
    {
        Debug.Log("보스: 휴식");

        _patternDone = true;
        RandomPattern();
        yield return null;
    }
    private IEnumerator PatternAttributeCounter()
    {
        Debug.Log("보스: 속성 카운터");

        _attributeCounter = true;
        _attributeCounterCount = _maxCount;

        int index = Random.Range(0, _attributes.Count);
        _attribute = _attributes[index];

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    private IEnumerator PatternCharge()
    {
        Debug.Log("보스: 차지");

        _chargeAttack = true;
        _charge = Random.Range(0, 10);

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    private IEnumerator PatternChargeAttack()
    {
        Debug.Log("보스: 차지 어택");

        _chargeAttack = false;

        var bullet = Instantiate(P_Bullet, gameObject.transform.position, gameObject.transform.rotation);
        bullet.GetComponent<MonsterMagic>().Damage = 50;

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    private IEnumerator PatternDeleteBullet()
    {
        _patternDone = true;
        RandomPattern();
        yield return null;
    }


    public override void Damage(int damage)
    {
        if (_attributeCounter)
        {
            Debug.Log("보스 : " + damage + "를 받았습니다.");

            // if (_attribute is "")
            // {
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
            // }
        }
        else
        {
            Debug.Log("보스 : " + damage + "를 받았습니다.");
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
        }

        Debug.Log("몬스터 남은 Defense : " + Stats.Defense);
        Debug.Log("몬스터 남은 HP : " + Stats.HP);

        if (Stats.HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void Turn()
    {
        if (_patternDone is true && NextPattern is not null)
        {
            _patternDone = false;
            CurrentPattern = StartCoroutine(NextPattern());

            if (_attributeCounter)
            {
                _attributeCounterCount -= 1;
            }
            if (_chargeAttack)
            {
                _charge -= 1;
            }
        }
    }
}
