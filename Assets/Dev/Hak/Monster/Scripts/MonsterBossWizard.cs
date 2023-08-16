using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBossWizard : Monster
{
    public GameObject P_Bullet;
    public MonsterBossWizard()
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

    public IEnumerator PatternRest()
    {
        Debug.Log("휴식");
        _patternDone = true;
        RandomPattern();
        yield return null;
    }
}
