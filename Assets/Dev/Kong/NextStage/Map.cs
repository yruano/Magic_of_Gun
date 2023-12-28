using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//map객체가 해야할 작동, 현재 씬 넘어갈 때 비활성화 기능만 가짐
public class Map : MonoBehaviour
{
    //자식, 층 객체들 저장용
    public List<GameObject> Floors = new List<GameObject>();
    //현재 층의 인덱스 저장용
    public int CurrentFloor;
    //현재 스테이지의 인덱스 저장용
    public int CurrentStage;

    public void Start()
    { 
        //맨 아래 층이 현재 층
        CurrentFloor = 0;
        //0번 버튼이 현재 버튼
        CurrentStage = 0;
    }

    //층 객체들을 끔
    public void OffFloors()
    {
        for(int i = 0; i < Floors.Count; i++)
        {
            //다 꺼버림
            Floors[i].SetActive(false);
        }
    }
    //층 객체들을 켬
    public void OnFloors()
    {
        for (int i = 0; i < Floors.Count; i++)
        {
            //다 켜버림
            Floors[i].SetActive(true);
        }
    }

    //층 객체들을 모두 클릭 불가모드로 만듬
    public void OffClick()
    {
        foreach (GameObject child in Floors)
        {
            child.GetComponent<Layer>().OffClick();
        }
    }

    //지금 버튼과 연결된 다음층의 버튼만 활성화 시킴
    public void OnConnetedButton()
    {
        //꼭대기 층이라면
        if(CurrentFloor+1 >= Floors.Count)
        {
            //불러와지면 안되는 상황임. 일단 오류는 나면 안되니까 함수 종료
            return;
        }
        //다음층의 버튼들을 가져옴
        List<GameObject> nextFloorButtons = Floors[CurrentFloor+1].GetComponent<Layer>().buttons;
        
        //각 버튼들이 현재 층과 연결되어있다면 버튼을 클릭 가능모드로 바꿈
        foreach(GameObject button in nextFloorButtons)
            //지금 확인중인 버튼이 현재 층과 연결되어있다면,
            if (button.GetComponent<StageButton>().connectedFloorID.Contains(CurrentFloor))
                //클릭 가능 모드로 바꿈
                button.GetComponent<StageButton>().OnClick();
        
    }

    //다음 스테이지로 넘어가기 위해 이 객체가 불러와졌을 때 해야할 일
    public void ToNextStage()
    {
        //꺼진 층 객체들을 키고
        OnFloors();
        //모든 버튼을 클릭 불가로 만듦
        OffClick();
        //가지고 있는 정보를 토대로 '지금 버튼'과 연결된 '다음 버튼'만 활성화 시킴
        OnConnetedButton();
    }
}
