using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP_CameraMove : MonoBehaviour
{
    public float moveSpeed = 0.001f; // 이동 속도 조절을 위한 변수

    void Update()
    {
        // 키보드 입력을 받아 이동 방향을 설정
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        //실제로 이동
        transform.position += new Vector3(moveX, moveY, 0);
    }
}
