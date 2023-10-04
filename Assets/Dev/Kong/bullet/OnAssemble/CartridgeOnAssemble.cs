using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeOnAssemble : MonoBehaviour
{
    [SerializeField]
    public AssembleManeger maneger = null;
    [SerializeField]
    //가지고 있는 정보
    public Item data = null;

    //초기화
    public void Awake()
    {
        //매니저 연결
        if(maneger == null) 
            maneger = FindObjectOfType<AssembleManeger>();
    }

    //클릭시 매니저에 정보전달
    public void OnMouseDown()
    {
        //maneger가 nullpointer일 시
        if (maneger == null)
        {
            //참조하기 전에 함수 끝내버림
            Debug.Log($"{gameObject.name} has got no Manager");
            return;
        }

        //클릭한 오브젝트 전달
        maneger.SelectCartridge(gameObject);
    }
}