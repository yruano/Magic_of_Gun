using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//이벤트와 상태를 중계합니다.
public class EventBus : MonoBehaviour, ITurn
{
    //중립상태, 행동 취소시 진입
    public event Action<int> Cancle;
    //에임 표식 클릭,중립 상태일 시 조준 중 상태로
    public event Action<int> ClickAim;
    //몬스터 클릭, 조준 중 상태일 시 목표 선택 상태가 됨
    public event Action<GameObject> ClickMonster;
    //방아쇠 클릭, 목표 선택 상태일 시 사격 진행
    public event Action<int> ClickTriger;
    //준비중인 탄창 클릭, 사격하지 않았고, 중립상태 일 때 전환가능
    public event Action<GameObject> ClickMagazine;
    //장전 확정, 사격할 수 없게됨
    public event Action<int> ClickLoadConfirmed;
    //플레이어 턴이 돌아왔으면 발생됨
    public event Action PlayerTurn;

    //현재 상태, 모든 리스너는 state를 수정해야함.
    public int State = 0;
    //중립 상태
    public static int NEUTRAL = 0;
    //조준중 상태
    public static int AIMING = 1;
    //목표 선택 상태 
    public static int SELECT_TARGET = 2;
    //탄창 선택 상태
    public static int SELECT_MAGAZINE = 3;

    //사격 여부
    public bool fired = false;
    //장전 여부
    public bool reloaded = false;

    //취소 이벤트 발생 중개
    public void PublishCancleEvent(int info)
    {
        Cancle?.Invoke(info);
    }

    //에임 클릭 이벤트 발생 중개
    public bool PublishClickAimEvent(int info)
    {
        //상태 확인, switch-case문은 static변수를 사용할 수 없으므로 if-else문으로 작성
        //장전하지 않았고, 중립 상태이거나 목표 지정 상태일 시
        if (reloaded == false && State == NEUTRAL || State == SELECT_TARGET)
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
        //목표 선택 상태라면
        if(State == SELECT_TARGET)
        {
            //방아쇠 이벤트 발생
            ClickTriger?.Invoke(info);
            //사격했다고 기록
            fired = true;
        }
    }

    //탄창 선택 이벤트 발생 중개, 무슨 탄창인지 정보 전달 필요
    public void PublishClickMagazineEvent(GameObject info)
    {
        //중립 상태이고, 사격한적이 없다면
        if(State == NEUTRAL && !fired)
        {
            //상태 탄창 선택 상태로 변결
            State = SELECT_MAGAZINE;
            //탄창 선택 이벤트 발생
            ClickMagazine?.Invoke(info);
        }
    }

    //탄창 교체 확정 이벤트 발생 중개
    public void PublishClickLoadConfirmedEvent(int info)
    {
        //탄창 선택 상태일 때
        if (State == SELECT_MAGAZINE)
        {
            //탄창클릭 이벤트 발생
            ClickLoadConfirmed?.Invoke(info);
            //장전했다고 기록
            reloaded = true;
        }
    }

    //플레이어 턴 이벤트 발생 중개
    public void PublishPlayerTurnEvent()
    {
        PlayerTurn?.Invoke();
    }
    public void Turn(bool IsTurn)
    {
        if (IsTurn)
            PublishPlayerTurnEvent();
    }
    
    //취소 이벤트 발생및 관련 상태 감지
    private void Update()
    {
        //오른쪽 마우스가 클릭 되었다면
        if (Input.GetMouseButtonDown(1))
        {
            // 마우스 오른쪽 버튼이 클릭되었다고 일단 로그에 보냄
            Debug.Log("Right button clicked.");
            //중립 상태가 아니라면
            if(State != NEUTRAL)
            {
                //중립상태로 변경
                State = NEUTRAL;
                //취소 이벤트 발생
                PublishCancleEvent(0);
                
            }
        }
        //!!임시, 다음턴 이벤트 굴리는 용도로 엔터 감지!!
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("enter key entered.");
            //플레이어 턴 이벤트 발생
            PlayerTurn?.Invoke();
        }
        

    }

    //PlayerTurn 발생시 초기화 하기 위해 이벤트 구독
    public void Start()
    {
        PlayerTurn += Initial;
    }

    //초기화, 중립이벤트 발생시키고 아무것도 하지않은 상태로 수정
    public void Initial()
    {
        State = 0;
        fired = false;
        reloaded = false;
        //초기화를 하기위해 중립상태 이벤트 발생, 자동 호출임을 나타내기 위해 음수로.
        Cancle?.Invoke(-1);
    }

}