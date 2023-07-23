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
    Color cartridgeColor = new Color(0xFF / 255f,
                                     0xAE / 255f,
                                     0x00 / 255f);
    [SerializeField]
    //탄두 색깔
    Color warHeadColor = new Color(0xFF / 255f,
                                   0xD7 / 255f,
                                   0x00 / 255f);

    //색깔 반영
    public void Start()
    {
        
    }

    //어쩌구 저쩌구 데이터.. 아마 디버프가 들어올것
}
