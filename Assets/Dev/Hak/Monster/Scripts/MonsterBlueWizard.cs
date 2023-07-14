using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterBlueWizard : Monster
{
    private string[] Patterns = new string[3];
    public MonsterBlueWizard()
    {
        Stats.DropItems = new[]
        {
            new MonsterDropItemData(1, ""),
        };
    }

    private void Start()
    {
        Patterns[0] = "Attack";
        Patterns[1] = "DefenseBuff";
        Patterns[2] = "DamageBuff";
        RandomPattern();
    }

    private void RandomPattern()
    {
        Patterns = Patterns.OrderBy(x => Random.Range(0, Patterns.Length)).ToArray();

        if (Patterns[0] is "Attack")
        {
            NextPattern = PatternAttack;
        }
        else if (Patterns[0] is "DefenseBuff")
        {
            NextPattern = PatternDefenseBuff;
        }
        else if (Patterns[0] is "DamageBuff")
        {
            NextPattern = PatternDamageBuff;
        }
    }

    private IEnumerator PatternAttack()
    {
        Debug.Log("공격");
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