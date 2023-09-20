using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteSpawn : MonoBehaviour
{
    public GameObject P_Monster;
    public GameObject P_Spawn;
    public Transform Pos;
    public List<GameObject> Monsters = new();

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        GameObject obj = Instantiate(P_Monster, Pos.position, Quaternion.identity);
        Monsters.Add(obj);

        GameObject spawn = Instantiate(P_Spawn, P_Spawn.transform.position, Quaternion.identity);
        spawn.GetComponent<MonsterSpawn>().Monsters = Monsters;
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
