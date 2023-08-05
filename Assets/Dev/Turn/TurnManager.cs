using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool IsTurn { get; private set; }

    private void Start()
    {
        // 게임이 시작할 때 플레이어 턴부터 시작하도록 설정
        IsTurn = true;
        StartTurn();
    }

    public void StartTurn()
    {
    }

    public void EndTurn()
    {
    }

    private void Update()
    {
    }
}
