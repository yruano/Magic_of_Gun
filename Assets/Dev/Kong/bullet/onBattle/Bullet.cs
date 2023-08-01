using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//자식 오브젝트에 색깔 데이터 나눠주고, 꺼지고 켜짐 중개
public class Bullet : MonoBehaviour
{
    [SerializeField]
    //데미지 데이터
    public int Damage = 0;
    [SerializeField]
    //탄피색깔
    public Color cartridgeColor;
    [SerializeField]
    //탄두 색깔
    public Color warHeadColor;
    [SerializeField]
    //탄두 오브젝트 연결
    public GameObject Warhead = null;
    [SerializeField]
    //탄피 오브젝트 연결
    public GameObject CartridgeCase = null;

    public void Start()
    {
        //자식 오브젝트 연결
        ConnecetChild();
        //색깔 변경
        ColorRefresh();
        
    }
    public void ConnecetChild()
    {
        //자식 오브젝트 연결
        for (int i = 0; i < transform.childCount; i++)
        {
            //탄두 오브젝트는
            if (transform.GetChild(i).gameObject.tag == "WarHead")
            {
                //탄두랑 연결
                Warhead = transform.GetChild(i).gameObject;
            }
            //탄피 오브젝트는
            else if (transform.GetChild(i).gameObject.tag == "CartridgeCase")
            {
                //탄피랑 연결
                CartridgeCase = transform.GetChild(i).gameObject;
            }
            //둘 다 연결 됐으면 반복문 종료
            if (Warhead != null && CartridgeCase != null) 
                break;
        }
    }
    //색깔 반영
    public void ColorRefresh()
    {
        //탄두랑 탄피가 연결 안되어 있으면
        if (Warhead == null || CartridgeCase == null)
            //다시 연결시도
            ConnecetChild();
        //탄두
        Warhead.GetComponent<SpriteRenderer>().color = warHeadColor;
        //탄피
        CartridgeCase.GetComponent<SpriteRenderer>().color = cartridgeColor;
    }
    //어쩌구 저쩌구 데이터.. 아마 디버프가 들어올것
}
