using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSpawn : MonoBehaviour
{
    public GameObject P_Monster;
    public List<Transform> Pos = new();
    public List<GameObject> Monsters = new();

    public void Spawn()
    {
        for (int i = 0; i < Pos.Count; i++)
        {
            GameObject obj = Instantiate(P_Monster, Pos[i].position, Quaternion.identity);
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
