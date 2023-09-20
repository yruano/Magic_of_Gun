using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public List<GameObject> P_Monster = new();
    public List<Transform> Pos = new();
    public List<GameObject> Monsters = new();

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        for (int i = 0; i < Pos.Count; i++)
        {
            var index = Random.Range(0, P_Monster.Count);
            GameObject obj = Instantiate(P_Monster[index], Pos[i].position, Quaternion.identity);
            Monsters.Add(obj);
        }
    }

    private void Update()
    {
        Monsters.RemoveAll(item => item == null);

        if (Monsters.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
