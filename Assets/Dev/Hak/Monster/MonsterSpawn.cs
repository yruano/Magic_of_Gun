using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public List<GameObject> P_Monster;
    public Transform FirstSpawnPos;
    public Transform SecondSpawnPos;
    public Transform ThirdSpawnPos;
    private List<Vector3> _pos = new();

    private void Awake()
    {
        _pos.Add(FirstSpawnPos.position);
        _pos.Add(SecondSpawnPos.position);
        _pos.Add(ThirdSpawnPos.position);
    }

    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(P_Monster[Random.Range(0, 4)], _pos[i], Quaternion.identity);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Spawn(1);
        }
    }
}
