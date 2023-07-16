using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이벤트와 상태를 중계합니다.
public class EventBus : MonoBehaviour
{
    //행동 취소후 중립상태
    public event Action<int> Cancle;
    //중립 상태에서 에임 표식 클릭, 조준 중 상태로
    public event Action<int> ClickAim;
    //조준 중 상태에서 몬스터 클릭, 목표 선택 상태가 됨
    public event Action<GameObject> ClickMonster;
    //목표 선택 상태에서 방아쇠 클릭, 사격 진행
    public event Action<int> ClickTriger;

    //현재 상태, 모든 리스너는 state를 수정해야함.
    public int State = 0;
    //중립 상태
    public static int NEUTRAL = 0;
    //조준중 상태
    public static int AIMING = 1;
    //목표 선택 상태 
    public static int SELECT_TARGET = 2;

    //취소 이벤트 발생 중개
    public void PublishCancleEvent(int info)
    {
        Cancle?.Invoke(info);
    }

    //에임 클릭 이벤트 발생 중개
    public bool PublishClickAimEvent(int info)
    {
        //상태 확인, switch-case문은 static변수를 사용할 수 없으므로 if-else문으로 작성
        //중립 상태이거나 목표 지정 상태일 시
        if (State == NEUTRAL || State == SELECT_TARGET)
        {
            //조준 중 상태로 변경
            State = AIMING;
            //이벤트 발생
            ClickAim?.Invoke(info);
            //이벤트가 발생 되었다고 호출한곳에 전달
            return true;
        }
        //else //이벤트가 발생할 상태가 아니라면
        //이벤트가 발생되지 않았다고 호출한곳에 전달
        return false;
    }

    //몬스터 클릭 이벤트 발생 중개
    public void PublishClickMonsterEvent(GameObject target)
    {
        //조준중 상태이라면
        if(State == AIMING)
        {
            //목표 지정 상태로 변경
            State = SELECT_TARGET;
            //클릭한 오브젝트를 구독한 메소드들에게 전달함
            ClickMonster?.Invoke(target);
        }
    }

    //방아쇠 클릭 이벤트 발생 중개
    public void PublishClickTrigerEvent(int info)
    {
        ClickTriger?.Invoke(info);
    }

    //취소 이벤트 발생및 관련 상태 감지
    private void Update()
    {
        //오른쪽 마우스가 클릭 되었다면
        if (Input.GetMouseButtonDown(1))
        {
            // 마우스 오른쪽 버튼이 클릭되었다고 일단 로그에 보냄
            Debug.Log("Right button clicked");
            //중립 상태가 아니라면
            if(State != NEUTRAL)
            {
                //중립상태로 변경
                State = NEUTRAL;
                //취소 이벤트 발생
                PublishCancleEvent(0);
                
            }
        }
    }
}