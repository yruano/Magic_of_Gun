using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGun : MonoBehaviour
{
    [SerializeField]
    GameObject gun;
    private void OnEnable()
    {
        Vector3 TargetPosition = gun.transform.position;
        transform.position = new Vector3(TargetPosition.x + 3, TargetPosition.y + 2);
    }
}
