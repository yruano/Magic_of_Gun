using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//자식 오브젝트에 색깔 데이터 나눠주고, 꺼지고 켜짐 중개
public class Bullet : MonoBehaviour, ISpriteToggleable
{
    //데미지 데이터
    public int Damage = 0;
    //탄피색깔
    Color cartridgeColor = new Color(0xFF / 255f,
                                     0xAE / 255f,
                                     0x00 / 255f);
    //탄두 색깔
    Color warHeadColor = new Color(0xFF / 255f,
                                   0xD7 / 255f,
                                   0x00 / 255f);

    //렌더러 끄기위해 자식들 렌더러 가지고 있음
    Renderer[] childRenderers = null;

    //자식 렌더러 가져옴
    public void Start()
    {
        childRenderers = GetComponentsInChildren<Renderer>();
    }

    //스프라이트 숨기기
    public void Hide(int info)
    {
        //가져온 자식을 꺼내서
        foreach (Renderer childRenderer in childRenderers)
        {
            //전부 꺼버림
            childRenderer.enabled = false;
        }
    }
    //스프라이트 켜기
    public void Show()
    {
        //가져온 자식을 꺼내서
        foreach (Renderer childRenderer in childRenderers)
        {
            //전부 킴
            childRenderer.enabled = false;
        }
    }

    //어쩌구 저쩌구 데이터.. 아마 디버프가 들어올것
}
