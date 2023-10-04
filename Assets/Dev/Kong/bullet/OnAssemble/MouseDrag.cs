using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    //마우스를 따라가는지 안따라가는지 체크
    private bool isFollowingMouse = false;
    //
    private Vector3 offset = Vector3.zero;

    //오브젝트가 움직이게 만드는 부분
    private void Update()
    {
        //오브젝트가 따라가도록 한 경우
        if (isFollowingMouse)
        {
            //마우스 위치를 받아오고
            Vector3 mousePosition = Input.mousePosition;
            //z축이 꼬여서 카메라 뒤로 넘어가지 않게 조절하고,
            mousePosition.z = -Camera.main.transform.position.z;
            //오브젝트 위치를 변경함
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
        }
    }

    //오브젝트가 따라가는걸 관리하는 부분
    //마우스를 누를 때
    private void OnMouseDown()
    {
        //마우스 위치를 가져오고
        Vector3 mousePosition = Input.mousePosition;
        //z축이 꼬여서 카메라 뒤로 넘어가지 않게 조절하고,
        mousePosition.z = -Camera.main.transform.position.z;
        //마우스 클릭한 부분과 오브젝트의 중심부가 틀어진 정도를 기록해서 갑자기 움직이지 않도록 조정
        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
        //따라가도록 설정
        isFollowingMouse = true;
    }

    //마우스를 뗄 때
    private void OnMouseUp()
    {
        //따라가지 않도록 설정
        isFollowingMouse = false; 
    }
}
