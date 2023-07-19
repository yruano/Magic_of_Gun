using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpwanRandom : MonoBehaviour
{
    public List<GameObject> P_SpwanPatten = new();

    public void Patten()
    {
        var Pattern = P_SpwanPatten.OrderBy(x => Random.Range(0, P_SpwanPatten.Count)).ToArray();

        Instantiate(Pattern[0], Pattern[0].transform.position, Pattern[0].transform.rotation);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Patten();
        }
    }
}
