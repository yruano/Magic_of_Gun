using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDeploy : MonoBehaviour
{
    //맵 생성시 사용할 클래스/컴포넌트
    public LayerDeploy layerDeploy;
    //한개 층 생성시 사용할 정보
    public LayerDeploy.BasicData BasicData = new LayerDeploy.BasicData();

    //Y축 최소 최대 범위
    public float yMin = -2f;
    public float yMax = 2f;
    //x축 최대 최소 범위
    public float xMin = -9.5f;
    public float xMax = 9.5f;

    //생성할 index층에 붙은 버튼 개수, 객체의 입력역할
    public List<int> layerButton;

    //버튼 생성시 참조할 프리팹, inspector창에 추가 입력이 없으면 컴포넌트 파일에 달린걸 그대로 사용
    public GameObject prefeb = null;

    //버튼들을 묶어 저장할 곳
    public List<List<GameObject>> Floors = new List<List<GameObject>>();
    //층들을 묶어 저장할 곳
    public List<GameObject> Map = new List<GameObject>();
    //층들을 엮어 맵으로 갖고있는 오브젝트
    public GameObject mapObject;

    //입력받은 기초값으로 초기화
    void Start()
    {
        //프리팹을 등록하라고 Inspector창에서 등록했다면
        if (prefeb != null)
            //등록한 프리팹 전달
            layerDeploy.prefeb = prefeb;
        //x축 위치도 전달
        layerDeploy.basicData.xMin = xMin;
        layerDeploy.basicData.xMax = xMax;

        //레이어 디폴로이 연결안되어있다면
        if (layerDeploy == null)
        {
            //찾아서 연결
            layerDeploy = GetComponent<LayerDeploy>();
        }

        //맵 오브젝트 생성
        mapObject = new GameObject("Map");
        //!!임시, 일단 생성 메소드 실행!!
        makeMap();
    }

    //입력 받은 정보로 생성
    public void makeMap()
    {
        //한 칸의 y축 차이를 계산
        float yGap = (yMax - yMin) / layerButton.Count;
        for (int i = 0; i < layerButton.Count; i++)
        {
            //y축 높이 다시 입력하고
            layerDeploy.basicData.y = yMin + i * yGap;
            //버튼 리스트 받아올 참조변수
            List<GameObject> floor = new List<GameObject>();
            //만드는 개수 지정하며 생성 지시
            GameObject newFloor = layerDeploy.NewFloor(layerButton[i], layerButton.Count-i, out floor);
            //층에 연결된 아래 층 기록
            ConnectRandomFloor(floor);
            //층 객체 기록
            Map.Add(newFloor);
            //버튼 하나하나 객체 엮음
            /*
            floor.Add(newFloor.GetComponent<LayerDeploy.>); dfdfdsfsddfasdfsdafasd 지금 한 층에 대한 점보를 담을 컴포넌트가 필요함, 아니면 floor를 전달할 방법이 필요함*/ 
            //버튼 모음집을 모음집 모음집에 저장
            Floors.Add(floor);
        }
        //Game창에서 보이는 순서와 Hierarchy창의 순서를 맞추기 위하여 역순으로 뒤집음
        Map.Reverse();
        //층 객체에 기록된 객체들 자식으로 등록
        for (int i = 0; i < layerButton.Count; i++)
        {
            Map[i].transform.SetParent(mapObject.transform);
        }
    }

    //아래층의 버튼과 랜덤으로 연결 입력: 추가되기 전의 새로운 층, 사용: 엮여져 있는 층들
    public void ConnectRandomFloor(List<GameObject> newFloor)
    {
        //엮을 '아래층'이 없다면
        if(Floors.Count == 0)
        {
            //아무것도 안하고 반환
            return;
        }

        //연결할 아래층을 가져옴
        List<GameObject> prevFloor = Floors[Floors.Count - 1];

        //각각 층에 랜덤으로 아래층과 연결
        foreach (GameObject floor in newFloor)
        {
            //데이터 저장할 컴포넌트 가져오고
            StageButton buttonData = floor.GetComponent<StageButton>();

            //컴포넌트에 랜덤으로 숫자 넣어줌
            for(int i = 0; i < prevFloor.Count; i++)
            {
                // 1/2확률로
                if(Random.Range(0,2) == 0)
                {
                    //정수 추가
                    buttonData.connectedFloorID.Add(i);
                }
            }
        }
    }
}
