using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOnAssemble : MonoBehaviour
{

    //마우스를 끌고 있을 때 마우스를 쫒아감
    public void OnMouseDown()
    {
        // 마우스의 현재 위치를 월드 좌표로 얻기
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 마우스의 z 좌표를 0으로 고정 (2D 공간에서 사용)

        // 오브젝트를 마우스 위치로 이동시키기
        transform.position = mousePosition;
    }
}
