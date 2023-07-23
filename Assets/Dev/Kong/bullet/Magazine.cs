using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1번째 칸 y +1.4 x 0
//2번째 칸 y +0.9 x 0
//3번째 칸 y +0.4 x -0.17
//4번째 칸 y -0.1 x -0.17
//5번째 칸 y -0.6 x -0.34
//6번째 칸 y -1.1 x -0.51
//7번째 칸 y -1.1 x -0.51
public class Magazine : MonoBehaviour, ISpriteToggleable
{
    //탄창역할을 위해 bullet리스트를 담는 무언가
    [SerializeField]
    public List<Bullet> bullets = new List<Bullet>();

    //이벤트 구독을 위해 연결
    public EventBus eventBus = null;

    //렌더러 끄기위해 자식들 렌더러 가지고 있음
    Renderer[] childRenderers = null;


    public void Start()
    {
        //이벤트 버스 구독준비
        if (eventBus == null)
        {
            //이벤트 연결
            eventBus = FindObjectOfType<EventBus>();
        }

        //방아쇠 클릭 이벤트 구독
        eventBus.ClickTriger += Hide;

        //플레이어 턴 이벤트 구독
        eventBus.PlayerTurn += Show;

        //자식 렌더러 가져옴
        childRenderers = GetComponentsInChildren<Renderer>();
    }

    //방아쇠 클릭 이벤트 발생시 호출되어야함, 오브젝트가 숨겨지고, 클릭 이벤트를 받지 못하게만듬
    public void Hide(int info)
    {
        //오브젝트가 숨겨짐,    SpriteRenderer는 Renderer를 상속받음
        gameObject.GetComponent<Renderer>().enabled = false;
        //클릭 못하게됨         ~~Collider2D 는 Collider2D를 상속받음
        gameObject.GetComponent<Collider2D>().enabled = false;
        //자식 렌더러도 꺼버림
        foreach (Renderer childRenderer in childRenderers)
        {
            childRenderer.enabled = false;
        }
    }

    //플레이어 턴 이벤트 발생시 호출되어야함, 숨겨진 오브젝트가 드러나야함, 클릭 이벤트를 받아야함
    public void Show()
    {
        //오브젝트가 다시 드러남SpriteRenderer는 Renderer를 상속받음
        gameObject.GetComponent<Renderer>().enabled = true;
        //클릭 가능하게됨       ~~Collider2D 는 Collider2D를 상속받음
        gameObject.GetComponent<Collider2D>().enabled = true;
        //자식 렌더러도 켬
        foreach (Renderer childRenderer in childRenderers)
        {
            childRenderer.enabled = true;
        }
    }

    //클릭되었을 때 이벤트를 발생시켜 정보를 보내야함.
    public void OnMouseDown() 
    {
        //로그발생
        Debug.Log($"Magazine Clicked: {gameObject.name}");
        eventBus.PublishClickMagazineEvent(gameObject);
    }


}
