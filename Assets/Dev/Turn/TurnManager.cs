using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> Monster;
    public bool IsTurn;

    private void Start()
    {
        IsTurn = true;
    }

    public void AddMonster(GameObject obj)
    {
        Monster.Add(obj);
    }

    public void StartTurn()
    {
        if (IsTurn)
        {
            var turn = Player.GetComponent<ITurn>();
            turn?.Turn();
        }
        else
        {
            for (int i = 0; i < Monster.Count; i++)
            {
                var turn = Monster[i].GetComponent<ITurn>();
                turn?.Turn();
            }

            EndTurn();
        }
    }

    public void EndTurn()
    {
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
