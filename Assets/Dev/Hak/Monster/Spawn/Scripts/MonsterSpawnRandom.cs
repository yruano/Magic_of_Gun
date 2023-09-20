using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawnRandom : MonoBehaviour
{
    public List<GameObject> P_SpawnPattern = new();
    public List<GameObject> SpawnMonster = new();
    public GameObject P_EliteSpawn;
    public GameObject P_BossSpawn;
    public GameObject TurnManager;

    public void Pattern()
    {
        var Pattern = P_SpawnPattern.OrderBy(x => Random.Range(0, P_SpawnPattern.Count)).ToArray();

        GameObject spawn = Instantiate(Pattern[0], Pattern[0].transform.position, Pattern[0].transform.rotation);
        spawn.GetComponent<MonsterSpawn>().Monsters = SpawnMonster;

        GameObject turn = Instantiate(TurnManager, TurnManager.transform.position, TurnManager.transform.rotation);
        turn.GetComponent<TurnManager>().Monsters = SpawnMonster;
    }

    public void EliteSpawn()
    {
        Instantiate(P_EliteSpawn, P_EliteSpawn.transform.position, P_EliteSpawn.transform.rotation);

        GameObject turn = Instantiate(TurnManager, TurnManager.transform.position, TurnManager.transform.rotation);
        turn.GetComponent<TurnManager>().Monsters = SpawnMonster;
    }

    public void BossSpawn()
    {
        Instantiate(P_BossSpawn, P_BossSpawn.transform.position, P_BossSpawn.transform.rotation);

        GameObject turn = Instantiate(TurnManager, TurnManager.transform.position, TurnManager.transform.rotation);
        turn.GetComponent<TurnManager>().Monsters = SpawnMonster;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Pattern();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EliteSpawn();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BossSpawn();
        }
    }
}
