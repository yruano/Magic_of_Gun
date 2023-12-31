using UnityEngine;

//장전 확정을 위한 이벤트 발생 담당
public class Slide : MonoBehaviour
{
    //이벤트 구독 위해 이벤트 버스 레퍼런스 연결
    public EventBus eventBus = null;

    private void OnEnable()
    {
        //이벤트 버스가 미리 연결 안되어있다면
        if (eventBus == null)
            //이벤트 버스 연결
            eventBus = FindObjectOfType<EventBus>();
    }

    //클릭시 이벤트 발생 요청. 적당한 상태라면 상태 전환
    private void OnMouseDown()
    {
        //로그에 클릭된걸 남기고
        Debug.Log($"{name}: Slide clicked");
        //이벤트 발생 요청
        eventBus.PublishClickLoadConfirmedEvent(0);
    }
}
