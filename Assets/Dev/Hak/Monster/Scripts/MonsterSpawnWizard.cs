using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawnWizard : Monster
{
    public GameObject P_Monster;
    public MonsterSpawnWizard()
    {
        Stats.DropItems = new[]
        {
            new MonsterDropItemData(1, ""),
        };
    }

    public IEnumerator PatternDefenseBuff()
    {
        Debug.Log("방어력 강화");
        Stats.Defense += 5;
        _patternDone = true;
        NextPattern = PatternHeel;
        yield return null;
    }

    public IEnumerator PatternHeel()
    {
        _patternDone = true;
        NextPattern = PatternSpawnPreparation;
        yield return null;
    }

    public IEnumerator PatternSpawnPreparation()
    {
        _patternDone = true;
        NextPattern = PatternSpawn;
        yield return null;
    }

    public IEnumerator PatternSpawn()
    {
        Instantiate(P_Monster, gameObject.transform.position, gameObject.transform.rotation);

        _patternDone = true;
        NextPattern = PatternDefenseBuff;
        yield return null;
    }
}
