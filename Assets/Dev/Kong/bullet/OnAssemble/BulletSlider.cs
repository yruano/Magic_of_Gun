using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletSlider : MonoBehaviour
{
    //왼쪽 마우스 클릭 이벤트 연결용
    [SerializeField]
    AssembleManeger AM = null;
    //장약량 전달하기 위해 참조 가지고 있음
    Bullet data = null;

    //ui좌표계와 오브젝트 좌표계를 이어주기 위해 사용하는 카메라
    [SerializeField]
    private Camera mainCamera = null;
    [SerializeField]
    public GameObject slider = null;
    // Start is called before the first frame update
    void Start()
    {
        //마우스 클릭 이벤트 전달 위해 매니저 연결
        AM = FindObjectOfType<AssembleManeger>();
        //우클릭 이벤트 리스너 등록
        //우클릭시 슬라이더를 숨기고
        AM.RightClick += HideSlider;
        //슬라이더 value값을 이동
        AM.RightClick += TransferValue;

        //메인 카메라 연결
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        //슬라이더의 부모, 배경에 남는 그림도 숨기기 위해 부모를 연결, 앞으로는 간결함을 위해 슬라이더의 부모가 아닌 슬라이더라고 칭함
        slider = transform.Find("Canvas").gameObject;
        //슬라이더 끄기
        slider.SetActive(false);

        //데이터 저장용 위치 연결
        data = GetComponent<Bullet>();
        
    }

    //마우스 우클릭을 처리하기 위한 함수
    private void OnMouseOver()
    {
        //마우스 왼쪽 버튼 클릭시
        if (Input.GetMouseButtonDown(0))
        {
            //슬라이더를 비 활성화 시킴
            slider.SetActive(false);

        }
    }
    //마우스 좌클릭을 놓을 시 슬라이더가 옮겨가고, 활성화됨
    private void OnMouseUp()
    {
        //슬라이더를 활성화 시킴
        slider.SetActive(true);

        //슬라이더를 오브젝트 위치로 이동
        Vector3 tmpPosition = transform.position;
        //살짝 아래로 옮겨서 오브젝트와 겹치지 않도록 함
        tmpPosition.y -= 1f;
        //--canvas를 옮기려고 했는데 이게 움직이면 다른 것도 안움직임, 나중에 개선 가능하면 수정하길..
        //slider.GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(tmpPosition);
        slider.transform.Find("Slider").GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(tmpPosition);
    }

    //슬라이더 숨기는 함수.
    private void HideSlider()
    {
        slider.SetActive(false);
    }

    //슬라이더 value를 bullet컴포넌트에 전달
    public void TransferValue()
    {
        data.charge = slider.GetComponentInChildren<Slider>().value;
    }
} 