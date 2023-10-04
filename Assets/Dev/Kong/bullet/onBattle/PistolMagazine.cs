using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolMagazine : Magazine
{
    //초기화
    new public void Start()
    {
        base.Start();
        //플레이어 턴 때 위치 잡도록 이벤트 연결
        eventBus.PlayerTurn += LocationBullets;
        //시작할 때 탄창 내부 총알 위치 잡음.
        LocationBullets();
    }

    //권총의 탄창 내부 총알 위치를 잡음
    public void LocationBullets()
    {
        float x = 0;
        float y = 0;
        for (int i = 0; i < bullets.Count; i++)
        {
            //x좌표 조정
            if (i < 2) { }
            else if(i < 4) { x = - 0.05756663f; }
            else if(i < 5) { x = - 0.1151333f; }
            else if(i < 7) { x = - 0.1726999f; }
            //y좌표 조정
            y = 0.3884358f - (i * 0.1387271f);
            //좌표 설정
            bullets[i].gameObject.transform.localPosition = new Vector3(x, y , 0f);
        }
    }
}
