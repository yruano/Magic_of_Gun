using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawnWizard : Monster
{
    public GameObject P_Bullet;
    public KnightSpawn KnightSpawn;
    private string SpawnState;
    private int PatternCount;

    public MonsterSpawnWizard()
    {
        Stats.DropItems = new[]
        {
            new MonsterDropItemData(1, ""),
        };
    }
    private void Start()
    {
        PatternCount = 5;
        Weights = new List<int> { 5, 4, 1 };

        Patterns.Add(PatternRest);
        Patterns.Add(PatternDefenseBuff);
        Patterns.Add(PatternHeel);

        SpawnState = "Impossible";
        RandomPattern();
    }
    private void RandomPattern()
    {
        WeightedRandom weightedRandom = new WeightedRandom(Weights);
        int randomIndex = weightedRandom.GetRandomIndex();

        if (SpawnState == "Impossible")
        {
            PatternCount -= 1;
            NextPattern = Patterns[randomIndex];
        }
        else
        {
            NextPattern = Patterns[randomIndex];
        }

        if (KnightSpawn.Monsters.Count == 1)
        {
            SpawnState = "possible";
        }

    }
    private IEnumerator PatternDefenseBuff()
    {
        Debug.Log("방어력 강화");
        Stats.Defense += 5;

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
    private IEnumerator PatternHeel()
    {
        Debug.Log("힐");

        // var bullet = Instantiate(P_Bullet, gameObject.transform.position, gameObject.transform.rotation);
        // bullet.GetComponent<MonsterMagic>().Heel = Stats.Damage;

        _patternDone = true;
        RandomPattern();
        yield return null;
    }
    private IEnumerator PatternSpawn()
    {
        KnightSpawn.Spawn();

        _patternDone = true;
        RandomPattern();
        yield return null;
    }
}
