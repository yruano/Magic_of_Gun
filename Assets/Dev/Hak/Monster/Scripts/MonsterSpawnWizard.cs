using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawnWizard : Monster
{
    public GameObject P_Area;
    public Transform MagicPos;
    public KnightSpawn KnightSpawn;
    private List<Func<IEnumerator>> _knightLivePatterns = new();
    private string _spawnState;
    private int _patternCount;
    private bool _knightLive;

    public MonsterSpawnWizard()
    {
        Stats.DropItems = new[]
        {
            new MonsterDropItemData(1, ""),
        };
    }
    private void Start()
    {
        _patternCount = 5;
        Weights = new List<int> { 8, 2 };

        Patterns.Add(PatternRest);
        Patterns.Add(PatternDefenseBuff);

        _knightLivePatterns.Add(PatternRest);
        _knightLivePatterns.Add(PatternHeel);

        _spawnState = "Impossible";
        _knightLive = false;
        RandomPattern();
    }
    public override void RandomPattern()
    {
        WeightedRandom weightedRandom = new WeightedRandom(Weights);
        int randomIndex = weightedRandom.GetRandomIndex();

        if (!_knightLive)
        {
            if (_spawnState == "Impossible")
            {
                _patternCount -= 1;
                _nextPattern = Patterns[randomIndex];
            }
            else
            {
                _nextPattern = PatternSpawn;
            }
        }
        else
        {
            _nextPattern = _knightLivePatterns[randomIndex];
        }

        if (_patternCount < 0)
        {
            _spawnState = "possible";
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

        var bullet = Instantiate(P_Area, MagicPos.position, MagicPos.rotation);
        bullet.GetComponent<MonsterMagic>().Heel = Stats.Damage;
        bullet.GetComponent<MonsterMagic>().Speed = 1f;

        _patternDone = true;
        RandomPattern();
        yield return null;
    }
    private IEnumerator PatternSpawn()
    {
        Debug.Log("나이트 소환");
        _knightLive = true;
        KnightSpawn.Spawn();

        _patternDone = true;
        RandomPattern();
        yield return null;
    }

    public override void Damage(int damage)
    {
        Debug.Log("몬스터 : " + damage + "를 받았습니다.");

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
            KnightSpawn.Done();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (KnightSpawn.Monsters.Count == 1 && _knightLive)
        {
            _knightLive = false;
            _patternCount = 5;
            _spawnState = "Impossible";
        }
    }
}
