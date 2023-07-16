using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

//클릭시 호출, 엄청 단순한 구조라 상속할거까진 아닌거 같아서 참고용 코드로 남김.  
public class ClickObjectCheck : MonoBehaviour
{
    //이벤트 구독 위해
    private EventBus eventBus = null;

    //이벤트 연결
    private void OnEnable()
    {
        //이벤트 연결 안되어있다면
        if (eventBus == null) 
        {
            //이벤트 연결 
            eventBus = FindObjectOfType<EventBus>();
        }
    }

    //클릭 이벤트 발생시
    private void OnMouseDown()
    {
        // 클릭 이벤트 발생 시 실행되는 코드
        UnityEngine.Debug.Log("trigger clicked!");
        //이벤트 호출
        eventBus.PublishClickTrigerEvent(0);
    }
}
