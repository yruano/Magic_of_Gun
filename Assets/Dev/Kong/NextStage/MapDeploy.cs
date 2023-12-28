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
    public GameObject mapObject = null;
    //맵 프리팹

    //입력받은 기초값으로 초기화
    void Start()
    {
        //프리펩을 등록하라고 Inspector창에서 등록했다면
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

        //맵 객체를 연결함
        ConnectMap();
    }

    public void ConnectMap()
    {
        //미리 연결해둔 맵이 없다면
        if (mapObject == null)
            //맵을 찾아 연결
            mapObject = GameObject.FindGameObjectWithTag("Map");

        //찾은 맵이 없다면
        if (mapObject == null)
            //맵을 만들어 연결
            makeMap();
        //찾은 맵이 있다면
        else
            //맵 객체가 해야할 일을 시킴
            mapObject.GetComponent<Map>().ToNextStage();
    }

    //입력 받은 정보로 생성
    public void makeMap()
    {
        //맵 오브젝트 생성
        mapObject = new GameObject("Map");
        //map컴포넌트 추가
        mapObject.AddComponent<Map>();
        //map태그 기록
        mapObject.tag = "Map";
        //채운 오브젝트를 DontDestroyOnLoad에 저장해서 씬 넘어가도 저장하는 객체로 지정
        DontDestroyOnLoad(mapObject);

        //층 오브젝트 생성
        //한 칸의 y축 차이를 계산
        float yGap = (yMax - yMin) / layerButton.Count;

        //버튼 리스트 받아올 참조변수, 층을 정의함
        List<GameObject> newFloorsBotton;
        //층 오브젝트를 만들며 배치함
        for (int i = 0; i < layerButton.Count; i++)
        {
            //y축 높이 조정
            layerDeploy.basicData.y = yMin + i * yGap;
            //리스트 객체 새로 받아옴
            newFloorsBotton = null;
            newFloorsBotton = new List<GameObject>();
            //미리 입력된 만들 버튼 개수 만큼 만듦
            GameObject newFloor = layerDeploy.NewFloor(layerButton[i], layerButton.Count-i, out newFloorsBotton);

            //각 버튼을 아래층의 버튼과 랜덤으로 연결함
            ConnectRandomFloor(newFloorsBotton);

            //층 객체 
            Map.Add(newFloor);
            //버튼 모음집을 모음집 모음집에 저장
            Floors.Add(newFloorsBotton);
        }
        //Game창에서 보이는 순서와 Hierarchy창의 순서를 맞추기 위하여 역순으로 뒤집음
        Map.Reverse();
        //층 객체에 기록된 객체들 자식으로 등록 
        for (int i = 0; i < layerButton.Count; i++)
        {
            Map[i].transform.SetParent(mapObject.transform);
        }

        //마지막으로 만들어둔 객체들 map에 기록
        mapObject.GetComponent<Map>().Floors = Map;
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

        //지금 버튼이 하나라면
        if (newFloor.Count == 1)
        {
            //아래층의 모든 버튼과 연결
            //저장할 버튼을 가져옴
            StageButton button = newFloor[0].GetComponent<StageButton>();
            //아래층의 모든 버튼과 연결
            for (int i = 0; i < prevFloor.Count; i++)
            {
                button.connectedFloorID.Add(i);
            }
        }
        //버튼이 여러개라면
        else
        {
            //각각 층에 랜덤으로 아래층과 연결
            foreach (GameObject floor in newFloor)
            {
                //데이터 저장할 컴포넌트 가져오고
                StageButton buttonData = floor.GetComponent<StageButton>();

                //추가된 숫자가 없다면 다시 랜덤 확률로 추가
                while (buttonData.connectedFloorID.Count == 0)
                    //컴포넌트에 랜덤으로 숫자 넣어줌
                    for (int i = 0; i < prevFloor.Count; i++)
                    {
                        // 1/2확률로
                        if (Random.Range(0, 2) == 0)
                        {
                            //정수 추가
                            buttonData.connectedFloorID.Add(i);
                        }
                    }
            }
        }
    }
}
