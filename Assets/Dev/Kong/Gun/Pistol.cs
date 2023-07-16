using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public Pistol()
    {
        //탄창 크기, m1911기준으로 7발(내가 좋아하는 총임)
        MaxMagazine = 7;
        //턴당 발사 횟수는 탄창크기만큼
        BulletPerTrun = MaxMagazine;
        //탄창 초기화
        Magazines.Clear();
        //집중 미사용
        Focus = false;
        //고장확률은 아직 미정이니 0
        JamChance = 0;
    }
}