using UnityEditor.UI;
using UnityEngine;
//마우스를 따라다님
public class FollowMouse : MonoBehaviour
{
    [SerializeField]
    EventBus eventBus = null;

    //켜질 때 마다 이벤트 버스에 연결되어 있는지 확인
    private void OnEnable()
    {
        //연결되어 있지 않다면
        if (eventBus == null)
        {
            //다시 연결
            eventBus = FindObjectOfType<EventBus>();
        }
    }
    void Update()
    {
        //현재 상태가 조준중 상태가 아니라면
        if(eventBus.State != EventBus.AIMING)
        { 
            //현재 스크립트 비활성화
            enabled = false;
            return;
        }

        //마우스 위치 가져오기
        Vector3 mousePosition = Input.mousePosition;
        //마우스 위치를 월드 좌표로 변환하기
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //z축 틀어지는거 맞춰주기(마우스 따라가는 컴포넌트가 켜지면 안보이는 이슈 이거로 해결)
        worldPosition.z = 0;
        //오브젝트 위치 업데이트하기
        transform.position = worldPosition;
    }
}