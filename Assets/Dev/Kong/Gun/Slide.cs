using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//장전 확정을 받는부분
public class Slide : MonoBehaviour
{
    //이벤트 구독을 위함
    EventBus eventBus = null;
    
    public void Start()
    {
        //이벤트 버스 연결
        eventBus = FindObjectOfType<EventBus>();
    }

    //클릭 발생시 이벤트 발생 요청. 적당한 상태라면 상태 전환
    private void OnMouseDown()
    {
        //일단 로그에 올림
        UnityEngine.Debug.Log("Slide clicked");
        //이벤트 발생 요청
        eventBus.PublishClickLoadConfirmedEvent(0);

    }
}
