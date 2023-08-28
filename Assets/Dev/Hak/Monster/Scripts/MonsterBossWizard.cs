using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBossWizard : Monster
{
    public GameObject P_Bullet;
    private bool _attributeCounter;
    private bool _actionCounterBuff;
    private int _attributeCounterCount;
    private int _maxCount;
    private int _actionCounterBuffShield;
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

        Patterns.Add(PatternAttack);
        Patterns.Add(PatternRest);
        Patterns.Add(PatternAttributeCounter);
        Patterns.Add(PatternDeleteBullet);
        Patterns.Add(PatternActionCounterBuff);

        _attributeCounter = false;
        _actionCounterBuff = false;
        _attributeCounterCount = 0;
        _maxCount = 5;
        _actionCounterBuffShield = 0;

        RandomPattern();
    }

    private void RandomPattern()
    {
        WeightedRandom weightedRandom = new WeightedRandom(Weights);
        int randomIndex = weightedRandom.GetRandomIndex();

        NextPattern = Patterns[randomIndex];
    }

    // 플레이어의 총알을 강제로 줄임
    // 몬스터가 랜덤으로 속성 선택 그 속성에 맞지 않는다면 반격데미지를 줌
    // 자신에게 버프를검 버프는 플레이어의 행동에 따라 추가적인 행동을함

    private IEnumerator PatternAttack()
    {
        var bullet = Instantiate(P_Bullet, gameObject.transform.position, gameObject.transform.rotation);
        bullet.GetComponent<MonsterMagic>().Damage = Stats.Damage;

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    private IEnumerator PatternRest()
    {
        Debug.Log("휴식");

        _patternDone = true;
        RandomPattern();
        yield return null;
    }
    private IEnumerator PatternAttributeCounter()
    {
        _attributeCounter = true;
        _attributeCounterCount = _maxCount;

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

    private IEnumerator PatternActionCounterBuff()
    {
        _actionCounterBuff = true;
        _actionCounterBuffShield = 1000;

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    public override void Damage(int damage)
    {
        if (_attributeCounter)
        {
            Stats.HP -= damage;
        }
        else if (_actionCounterBuff)
        {
            _actionCounterBuffShield -= damage;

            if (_actionCounterBuffShield <= 0)
            {
                _actionCounterBuffShield = 0;
                _actionCounterBuff = false;
            }
        }
        else
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
        }

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
        }
    }
}
