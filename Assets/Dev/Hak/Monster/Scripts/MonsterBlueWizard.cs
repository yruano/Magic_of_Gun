using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBlueWizard : Monster
{
    public MonsterBlueWizard()
    {
        Stats.DropItems = new []
        {
            new MonsterDropItemData(1, ""),
        };
    }

    private void Start()
    {
        NextPattern = PatternAttack;
    }
    private IEnumerator PatternAttack()
    {
        Debug.Log("공격");
        _patternDone = true;
        NextPattern = PatternSkill;
        yield return null;
    }

    private IEnumerator PatternSkill()
    {
        Debug.Log("버프 사용");
        _patternDone = true;
        yield return null;
    }
}