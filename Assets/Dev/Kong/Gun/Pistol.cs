using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public Pistol()
    {
        //탄창 크기, 7발(그림 모양 맞춤)
        MaxMagazine = 7;
        //턴당 발사 횟수는 탄창크기만큼
        BulletPerTrun = MaxMagazine;
        //탄창 초기화
        Magazine.Clear();
        //고장확률은 아직 미정이니 0
        JamChance = 0;
        
    }
    //총알 스프라이트의 위치를 조정함.
    public override void MoveBulletSprite()
    {
        for (int i = 0; i < bulletSprite.Count; i++)
            //좌표 설정
            bulletSprite[i].transform.position = new Vector3(transform.position.x - 3f, transform.position.y + 2.0f - (i * 0.6f), transform.position.z);
    }
}
