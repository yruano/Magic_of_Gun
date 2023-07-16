using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

//클릭시 호출, 엄청 단순한 구조라 상속할거까진 아닌거 같아서 참고용 코드로 남김.  
public class ClickMonster : MonoBehaviour
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
        //몬스터가 클릭되었다고 보냄
        UnityEngine.Debug.Log($"montser clicked: {gameObject.name}");
        //이벤트 발생 요청
        eventBus.PublishClickMonsterEvent(gameObject);
    }
}
