using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonster
{
  public void Attack();
  public void Skill();
}

public class MonsterSpawn
{
  public static string[] MonsterPaths =
  {
    
  };
  public List<IMonster> Monsters = new();
  private Monster _monster;

  public MonsterSpawn(Monster monster)
  {
    _monster = monster;
  }
  public void AddMonsters(int id)
  {
    // Monsters.Add()
  }
}
