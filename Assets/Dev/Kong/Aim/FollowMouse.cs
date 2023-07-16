using UnityEditor.UI;
using UnityEngine;
//마우스를 따라다니게 됩니다. 
//gpt코드 그대로 긁어왔습니다
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

        // 1. 마우스 위치 가져오기
        Vector3 mousePosition = Input.mousePosition;

        // 2. 마우스 위치를 월드 좌표로 변환하기
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f; // 오브젝트가 2D 평면에 있는 경우, Z 축을 조정해야 할 수 있습니다.

        // 3. 오브젝트 위치 업데이트하기
        transform.position = worldPosition;
    }
}