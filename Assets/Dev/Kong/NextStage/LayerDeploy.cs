using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//새로운 층 한개를 만들고, 입력된 X축 길이에 맞게 위치를 조정함
public class LayerDeploy : MonoBehaviour
{
    public struct BasicData
    {
        //층의 Y축
        public float y; //2f
        //층의 X축 위치
        public float xMin; //-9.5f
        public float xMax; //9.5f 정도가 적당한듯
        //생성개수
        public int count;// 5
    }
    
    //그동안 생성한 층 버튼들 정보
    public List<NextStage> oneStageFloor = new List<NextStage>();
    //버튼 생성시 사용할 도구
    public GenerateStageList generateStageList = new GenerateStageList();
    //생성된 버튼 오브젝트
    public List<GameObject> buttons = new List<GameObject>();
    //생성시 사용할 데이터 저장소
    public BasicData basicData = new BasicData();

    //스테이지 생성시 참고할 프리팹
    public GameObject prefeb = null;
    //스테이지 버튼 생성시 참조할 스프라이트
    public List<Sprite> originSprites = new List<Sprite>();

    //새로운 스테이지들 정보 생성
    public void NewStageInfo(int count)
    {
        //초기화
        oneStageFloor.Clear();
        //기본으로 단계 1개 생성
        oneStageFloor.Add(generateStageList.newStage());
        //요구분량만큼 생성
        while (oneStageFloor.Count < count)
        {
            oneStageFloor.Add(generateStageList.newStage());
        }
    }

    //정보를 기반으로 스테이지 버튼 오브젝트 생성
    //정보는 이미 생성되었다 가정하고 진행
    public List<GameObject> GenerateButton()
    {
        //초기화
        buttons.Clear();
        //몇번째 객체인지 기록용 변수
        int i = 1;
        foreach (var stage in oneStageFloor)
        {
            //지금 만드는 버튼 
            GameObject button = Instantiate(prefeb);

            //앞으로 계속 수정할 버튼 스프라이트를 참조변수에 연결
            StageButton stageButton = button.GetComponent<StageButton>();
            //정보에 맞게 버튼 스프라이트 수정
            button.GetComponent<SpriteRenderer>().sprite = originSprites[(int)stage.type];
            //정보를 오브젝트에 입력
            stageButton.Data = stage;
            //이름에 몇번째인지 기록함
            button.name = "Stage_Button_" + i;
            //정보 저장소에도 기록함
            stageButton.onFloorID = i++;

            //만들어진 오브젝트를 배열에 추가
            buttons.Add(button);
        }

        //반환
        return buttons;
    }

    //생성한 오브젝트를 배치
    public void Relocate()
    {
        //지금 입력된 길이 확인
        float lengthX = basicData.xMax - basicData.xMin;
        //각각 버튼간 거리 확인, 버튼 개수 + 1(화면 앞 끝 과의 거리)
        float distance = lengthX / (buttons.Count + 1);

        for (int i = 0; i < buttons.Count; i++)
        {
            //i번째 버튼은 x시작부분에서 버튼간거리 * (i + 1)만큼 뒤로 밀리고, y높이에 위치함
            buttons[i].transform.position = new Vector3(basicData.xMin + (distance * (i + 1)), basicData.y);
        }
    }

    //위의 절차를 따라서 층 1개를 만들고 위치를 정함
    public void GenerateOneFloor()
    {
        //입력받은 최소 남은 거리와 준비되어있는 갯수만큼 생성
        NewStageInfo(basicData.count);
        //오브젝트 생성
        GenerateButton();
        //오브젝트 위치 이동
        Relocate();
    }

    //조건입력 받아서 층 1개를 만들고, 위치 조정해서 반환함
    public GameObject NewFloor(int count, int floorLevel, out List<GameObject> floor)
    {
        //같은 층 객체를 묶을 부모생성
        GameObject newLayer = new GameObject("floor_" + floorLevel);
        //대표 Y축 위치로 이동
        newLayer.transform.position = new Vector3(0, basicData.y, 0);
        //변수 설정
        basicData.count = count;
        //층 생성 후
        GenerateOneFloor();
        //한개의 객체의 자식으로 등록 후
        foreach (GameObject onebutton in buttons)
        {
            onebutton.transform.SetParent(newLayer.transform);
        }
        floor = buttons;
        //등록한 객체를 반환
        return newLayer;
    }
}
