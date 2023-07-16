using UnityEngine;
//마우스를 따라다니게 됩니다. 
//gpt코드 그대로 긁어왔습니다
public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        // 1. 마우스 위치 가져오기
        Vector3 mousePosition = Input.mousePosition;

        // 2. 마우스 위치를 월드 좌표로 변환하기
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f; // 오브젝트가 2D 평면에 있는 경우, Z 축을 조정해야 할 수 있습니다.

        // 3. 오브젝트 위치 업데이트하기
        transform.position = worldPosition;
    }
}