using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnAndTurn : MonoBehaviour
{
    public List<GameObject> P_Monster;
    public List<Transform> Pos = new();
    public List<GameObject> SpawnMonster = new();
    public GameObject Player;
    public bool IsTurn;

    private void Start()
    {
        Spawn();
        IsTurn = true;
        StartTurn();
    }
    public void Spawn()
    {
        for (int i = 0; i < Pos.Count; i++)
        {
            var index = Random.Range(0, P_Monster.Count);
            GameObject obj = Instantiate(P_Monster[index], Pos[i].position, Quaternion.identity);
            SpawnMonster.Add(obj);
        }

    }

    public void StartTurn()
    {
        if (IsTurn)
        {
            Debug.Log("플레이어 턴 시작");
            var turn = Player.GetComponent<ITurn>();
            turn?.Turn();
        }
        else
        {
            Debug.Log("몬스터 턴 시작");
            for (int i = 0; i < SpawnMonster.Count; i++)
            {
                var turn = SpawnMonster[i].GetComponent<ITurn>();
                turn?.Turn();
            }

            EndTurn();
        }
    }

    public void EndTurn()
    {
        Debug.Log("턴 종료");
        IsTurn = !IsTurn;
        StartTurn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartTurn();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EndTurn();
        }
    }
}
