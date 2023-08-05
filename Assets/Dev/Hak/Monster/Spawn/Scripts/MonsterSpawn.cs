using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public List<GameObject> P_Monster;
    public List<Transform> Pos = new();
    public TurnManager Turnmanager;

    private void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        for (int i = 0; i < Pos.Count; i++)
        {
            GameObject obj = Instantiate(P_Monster[1], Pos[i].position, Quaternion.identity);
            // TurnManager.AddMonster(obj);
        }
    }
}
