using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    private bool isFollowingMouse = false; // 클릭 후 따라가는 여부
    private Vector3 offset; // 마우스 위치와의 차이

    private void Update()
    {
        // 클릭한 경우
        if (isFollowingMouse)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z; // 마우스 위치를 월드 좌표로 변환
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        // 클릭 시 마우스 위치와의 차이 계산하여 저장
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);

        isFollowingMouse = true; // 따라가도록 설정
    }

    private void OnMouseUp()
    {
        isFollowingMouse = false; // 클릭 해제하면 멈춤
    }
}
