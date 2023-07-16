using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이벤트를 중계ㅡ합니다.
public class EventBus : MonoBehaviour
{
    //행동 취소후 중립상태
    public event Action<int> Cancle;
    //중립 상태에서 에임 표식 클릭, 조준 중 상태로
    public event Action<int> ClickAim;
    //조준 중 상태에서 몬스터 클릭, 목표 선택 상태가 됨
    public event Action<int> ClickMonster;
    //목표 선택 상태에서 방아쇠 클릭, 사격 진행
    public event Action<int> ClickTriger;

    //취소 이벤트 발생, //아마 상태번호를 전달
    public void PublishCancleEvent(int info)
    {
        Cancle?.Invoke(info);
    }

    //에임 클릭 이벤트 발생 
    public void PublishClickAimEvent(int info)
    {
        ClickAim?.Invoke(info);
    }

    //몬스터 클릭 이벤트 발생, 몇번 몬스터가 클릭 되었는지 포함
    public void PublishClickMonsterEvent(int info)
    {
        ClickMonster?.Invoke(info);
    }

    //방아쇠 클릭 이벤트 발생
    public void PublishClickTrigerEvent(int info)
    {
        ClickTriger?.Invoke(info);
    }
}