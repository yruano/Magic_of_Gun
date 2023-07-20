using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//탄창 교체 확인을 위해 임시로 총 벨트를 가짐
public class TmpPlayer : Player
{ 
    //총벨트, 총에서 왔다 갔다 하는걸 보여주기 위해 사용
    List<Bullet> magazineBelt = new List<Bullet>();

    //이벤트 버스 구독용
    public EventBus eventBus;

    public void Start()
    {
        //이벤트 버스연결
        eventBus = FindObjectOfType<EventBus>();
    }
}
