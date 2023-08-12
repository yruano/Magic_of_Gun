using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarHeadOnAssemble : MonoBehaviour
{
    [SerializeField]
    public AssembleManeger maneger = null;
    [SerializeField]
    //가지고 있는 정보
    public Item data = null;

    public void Awake()
    {
        //매니저 연결
        maneger = FindObjectOfType<AssembleManeger>();
    }

    //클릭시 매니저에 정보전달
    public void OnMouseDown()
    {
        //nullpointer참조 방지용
        if (maneger == null)
        {
            Debug.Log($"{gameObject.name} has got no Manager");
            return;
        }

        //클릭한 오브젝트 전달
        maneger.SelectWarHead(gameObject);
    }
}