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
    private void Start()
    {
        Weights = new List<int> { 5, 3, 2 };

        Patterns.Add(PatternRest);
        Patterns.Add(PatternDefenseBuff);
        Patterns.Add(PatternHeel);
        
        RandomPattern();
    }
    private void RandomPattern()
    {
        WeightedRandom weightedRandom = new WeightedRandom(Weights);
        int randomIndex = weightedRandom.GetRandomIndex();

        NextPattern = Patterns[randomIndex];
    }

    private IEnumerator PatternDefenseBuff()
    {
        Debug.Log("방어력 강화");
        Stats.Defense += 5;

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    private IEnumerator PatternHeel()
    {
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

    private IEnumerator PatternSpawnPreparation()
    {
        _patternDone = true;
        NextPattern = PatternSpawn;
        yield return null;
    }

    private IEnumerator PatternSpawn()
    {
        Instantiate(P_Monster, gameObject.transform.position, gameObject.transform.rotation);

        _patternDone = true;
        NextPattern = PatternDefenseBuff;
        yield return null;
    }
}
