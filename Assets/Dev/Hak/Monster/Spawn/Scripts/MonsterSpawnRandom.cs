using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawnRandom : MonoBehaviour
{
    public List<GameObject> P_SpwanPattern = new();

    public void Pattern()
    {
        var Pattern = P_SpwanPattern.OrderBy(x => Random.Range(0, P_SpwanPattern.Count)).ToArray();

        Instantiate(Pattern[0], Pattern[0].transform.position, Pattern[0].transform.rotation);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Pattern();
        }
    }
}
