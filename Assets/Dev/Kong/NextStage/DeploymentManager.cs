using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//새로운 스테이지를 만들고, 여기저기 날려줌
public class DeploymentManager : MonoBehaviour
{
    public float y = 2f;
    public float xMin = -9.5f;
    public float xMax = 9.5f;
    //반환할것
    public List<NextStage> nextStage = new List<NextStage>();
    //생성시 사용할 도구
    public GenerateStageList generateStageList = new GenerateStageList();
    //생성개수, inspector창에서 수정할 수 있도록 따로 변수 추가
    [SerializeField]
    public int count = 0;

    //스테이지 생성시 참고할 프리팹
    [SerializeField]
    public GameObject prefeb = null;
    //스테이지 버튼 생성시 참조할 스프라이트
    [SerializeField]
    public List<Sprite> originSprites = new List<Sprite>();
    //생성된 버튼 오브젝트
    [SerializeField]
    public List<GameObject> buttons = new List<GameObject>();

    //새로운 스테이지들 정보 생성
    public List<NextStage> NewStageInfo(int minLeft, int extraLeft, int count)
    {
        //초기화
        nextStage.Clear();
        //기본 1개 생성
        nextStage.Add(generateStageList.nextStage(minLeft));
        //요구분량만큼 생성
        while (nextStage.Count < count)
        {
            nextStage.Add(generateStageList.nextStage(minLeft, minLeft + extraLeft));
        }
        //반환
        return nextStage;
    }

    //정보를 기반으로 스테이지 버튼 오브젝트 생성
    //정보는 이미 생성되었다 가정하고 진행
    public List<GameObject> GenerateButton()
    {
        //초기화
        buttons.Clear();
        foreach (var stage in nextStage)
        {
            //지금 만드는 버튼 
            GameObject button = Instantiate(prefeb);

            //정보에 맞게 버튼 스프라이트 수정
            button.GetComponent<SpriteRenderer>().sprite = originSprites[(int)stage.type];
            //정보를 오브젝트에 입력
            button.GetComponent<StageButton>().Data = stage;

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
        float lengthX = xMax - xMin;
        //각각 버튼간 거리 확인, 버튼 개수 + 2(앞 뒤 거리)
        float distance = lengthX / (buttons.Count + 2);

        for (int i = 0; i < buttons.Count; i++)
        {
            //i번째 버튼은 x시작부분에서 버튼간거리 * (i + 1)만큼 뒤로 밀리고, y높이에 위치함
            buttons[i].transform.position = new Vector3(xMin + (distance * (i + 1)), y);
        }
    }

    //위의 절차를 
    public void run()
    {

    }
}
