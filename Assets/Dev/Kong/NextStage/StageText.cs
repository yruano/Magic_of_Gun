using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageText : MonoBehaviour
{
    void Start()
    {
        MoveToParent();
    }
    public void MoveToParent()
    {
        Transform parnet = transform.parent;
        transform.position = parnet.transform.position;
    }
}
