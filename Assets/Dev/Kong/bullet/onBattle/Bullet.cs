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
    [SerializeField]
    //내부 화약량 데이터, 0~1
    public float charge = 0;

    public void Start()
    {
        //만약 자식이 연결되어있지 않다면
        if(Warhead == null || CartridgeCase == null)
            //자식 오브젝트 연결
            ConnecetChild();
        //색깔 반영
        ColorRefresh();
    }
    //자식오브젝트가 연결되어 있지 않다면 가장 위에있는 탄두와 탄피를 골라 연결
    public void ConnecetChild()
    {
        //자식 오브젝트 연결
        for (int i = 0; i < transform.childCount; i++)
        {
            //탄두 오브젝트는
            if (Warhead == null && transform.GetChild(i).gameObject.tag == "WarHead")
            {
                //탄두랑 연결
                Warhead = transform.GetChild(i).gameObject;
            }
            //탄피 오브젝트는
            else if (CartridgeCase == null && transform.GetChild(i).gameObject.tag == "CartridgeCase")
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

    //자식 오브젝트 교체
    //탄두 교체
    public void ReplaceWarHead(GameObject newWarhead)
    {
        //기존 오브젝트가 존재한다면
        if (Warhead != null)
        {
            //기존 오브젝트 삭제
            Destroy(Warhead);
        }

        //새로 입력받은 오브젝트를 연결
        Warhead = newWarhead;
        //자식으로 등록
        Warhead.transform.SetParent(transform);
        //색깔 데이터 교체
        warHeadColor = Warhead.GetComponent<SpriteRenderer>().color;
        //탄두 위치 이동
        Warhead.transform.localPosition = new Vector3(1, 0, 0);
    }
    //탄피 교체
    public void ReplaceCartridge(GameObject newCartridgeCase)
    {
        //기존 오브젝트가 존재한다면
        if (CartridgeCase != null)
        {
            //기존 오브젝트 삭제
            Destroy(CartridgeCase);
        }
        //새로 입력받은 오브젝트로 교체
        CartridgeCase = newCartridgeCase;
        //자식으로 등록
        CartridgeCase.transform.SetParent(transform);
        //색깔 데이터 교체
        cartridgeColor = CartridgeCase.GetComponent<SpriteRenderer>().color;
        //탄피 위치 이동
        CartridgeCase.transform.localPosition = new Vector3(0, 0, 0);
    }
    //어쩌구 저쩌구 데이터.. 아마 디버프가 들어올것
}
