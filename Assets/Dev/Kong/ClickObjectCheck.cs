using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ClickObjectCheck : MonoBehaviour
{
    private void OnMouseDown()
    {
        // 클릭 이벤트 발생 시 실행되는 코드
        UnityEngine.Debug.Log("Object clicked!");
    }
}
