using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class StageText : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform = null;
    [SerializeField]
    private Camera mainCamera = null;
    [SerializeField]
    public TMP_Text TextMesh = null;
    //초기화
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        TextMesh = GetComponent<TMP_Text>();
        MoveToParent();
    }
    
    //부모, 스테이지 버튼 옆으로 이동
    public void MoveToParent()
    {
        //필요한 컴포넌트 참조 가져오기
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        if(mainCamera == null)
        {
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        //위치 다시잡기
        Vector3 ps = GetComponentInParent<CircleCollider2D>().transform.position;
        //현재위치, 일단 임의로 작성함
        ps.x -= 0.6f;
        ps.y += 0.6f;
        //일반 좌표계는 먹히지 않아서 아래처럼 작성함, !현재 x좌표에 따라 위치가 살짝 틀어지는 문제 확인함!
        rectTransform.position = mainCamera.WorldToScreenPoint(ps);
    }
}
