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
    void Start()
    {
        MoveToParent();
    }
    public void MoveToParent()
    {
        //필요한 컴포넌트 참조 가져오기
        rectTransform = GetComponent<RectTransform>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        //위치 다시잡기
        Vector3 ps = GetComponentInParent<CircleCollider2D>().transform.position;
        ps.x -= 0.6f;
        ps.y += 0.6f;
        rectTransform.position = mainCamera.WorldToScreenPoint(ps);
    }
}
