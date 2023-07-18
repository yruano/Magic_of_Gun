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
        Magazines.Clear();
        //집중 미사용
        Focus = false;
        //고장확률은 아직 미정이니 0
        JamChance = 0;
        //!!데이터 확인용 임시!!
        SampleBullet sd = new SampleBullet();
        Magazines.Add(sd);
        
    }
}
