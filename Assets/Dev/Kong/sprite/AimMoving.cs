using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMoving : MonoBehaviour
{
    [SerializeField]
    GameObject gun;

    private void ToGun()
    {
        Vector3 TargetPosition = gun.transform.position;
        transform.position = new Vector3(TargetPosition.x + 3, TargetPosition.y + 2);
    }
}
