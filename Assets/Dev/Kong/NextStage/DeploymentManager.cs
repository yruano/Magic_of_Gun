using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//다음 칸들을 입력받아서 뿌려줌
public class DeploymentManager : MonoBehaviour
{
    public float y = 2f;
    public float xMin = -9.5f;
    public float xMax = 9.5f;
    public List<GameObject> nextStage = new List<GameObject>();
}
