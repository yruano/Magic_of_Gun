using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterStat
{
  [SerializeField]
  public float HP;
  [SerializeField]
  public float Damage;
  [SerializeField]
  public float Defense;

  public MonsterStat()
  {
    HP = 10;
    Damage = 10;
    Defense = 5;
  }
}
public class Monster : MonoBehaviour
{
  [HideInInspector]
  public MonsterStat Stat;
  public MonsterSpawn Spawn;

  private void Awake()
  {
    Spawn = new(this);
    Spawn.AddMonsters(0);
  }

  private void Update() 
  {  
  }
}
