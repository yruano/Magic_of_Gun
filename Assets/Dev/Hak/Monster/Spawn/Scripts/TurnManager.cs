using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<GameObject> Monsters;
    public GameObject Player;
    public bool IsTurn;

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
            for (int i = 0; i < Monsters.Count; i++)
            {
                var turn = Monsters[i].GetComponent<ITurn>();
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
            IsTurn = true;
            StartTurn();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            EndTurn();
        }

        if (Monsters.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
