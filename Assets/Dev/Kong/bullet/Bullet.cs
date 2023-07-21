using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
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

    //어쩌구 저쩌구 데이터.. 아마 디버프가 들어올것
}
