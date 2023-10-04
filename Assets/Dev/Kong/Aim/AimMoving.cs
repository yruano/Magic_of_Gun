using UnityEngine;
using UnityEngine.UI;

//에임과 관련된 상태 전환과 그 움직임을 담당
public class AimMoving : MonoBehaviour
{
    [SerializeField]
    //이벤트 구독 연결
    public EventBus eventBus = null;
    //총 위치를 찾기위해 연결
    public GameObject gun = null;
    //마우스를 따라다니게 위해 연결
    public FollowMouse followMouse = null;
    //에임의 중립위치, 총 기준으로 어디위치인가, 기본값 3, 2는 권총기준
    public Vector2 offset = new Vector2(3f, 2f);

    //초기화
    private void Awake()
    {
        //이벤트가 연결 안되어있지 않다면
        if (eventBus == null)
        {
            //이벤트 연결
            eventBus = FindObjectOfType<EventBus>();
        }
        //마우스 위치로 이동하게 하는 스크립트가 연결되어 있지 않다면
        if(followMouse == null)
        {
            //스크립트 연결
            followMouse = FindObjectOfType<FollowMouse>();
        }
        //총 오브젝트가 연결되어있지 않다면
        if(gun == null)
        {
            //Gun태그를 이용해 연결
            gun = GameObject.FindGameObjectWithTag("Gun");
        }

        //에임 클릭 이벤트를 구독함
        eventBus.ClickAim += ToMouse;

        //중립 상태 이벤트를 구독함
        eventBus.Cancle += ToGun;

        //목표 선택 상태 이벤트를 구독함
        eventBus.ClickMonster += ToMonster;

        //탄창 교체 확정 이벤트를 구독함
        eventBus.ClickLoadConfirmed += Hide;

        //플레이어턴 이벤트를 구독함
        eventBus.PlayerTurn += Show;
    }

    //클릭 발생시 이벤트 발생 요청. 적당한 상태라면 상태 전환
    private void OnMouseDown()
    {
        //일단 로그에 올림
        UnityEngine.Debug.Log("aim object clicked");
        //이벤트 발생 요청
        eventBus.PublishClickAimEvent(0);

    }
    
    //중립 상태때 호출되어야함, 총구 위치로 이동 !!(현재 위치는 임시지정임)!!
    public void ToGun(int info)
    {
        //이번턴에 장전했다면
        if(eventBus.reloaded == true) 
            //아무것도 하지않고 종료
            return;

        //다시 에임객체가 클릭 될 수 있도록 colider를 켬
        GetComponent<CircleCollider2D>().enabled = true;
        //총의 위치를 찾아
        Vector3 TargetPosition = gun.transform.position;
        //기록해둔 위치로 이동
        transform.position = new Vector3(TargetPosition.x + offset.x, TargetPosition.y + offset.y);
    }
    
    //조준중 상태일 때 호출되어야함, 마우스 위치로 이동하는 스크립트를 켬.
    public void ToMouse(int info)
    {
        //마우스 따라다니는 스크립트를 켬.
        followMouse.enabled = true;
        //다시 에임이 클릭되지 않도록 colider를 끔
        GetComponent<CircleCollider2D>().enabled = false;
    }

    //목표 선택 상태일 때 호출 되어야함, 선택한 몬스터로 이동함
    public void ToMonster(GameObject target)
    {
        //다시 에임이 클릭될수 있도록 colider를 켬
        GetComponent<CircleCollider2D>().enabled = true;
        //몬스터의 정 가운데 위치로 이동
        transform.position = target.transform.position;
    }

    //탄창 교체 확정 이벤트 발생시 호출되어야함, 오브젝트가 숨겨지고, 클릭 이벤트를 받지 못하게만듬
    public void Hide(int info)
    {
        //오브젝트가 숨겨짐
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //클릭 못하게됨
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    //플레이어 턴 이벤트 발생시 호출되어야함, 숨겨진 오브젝트가 드러나야함
    public void Show()
    {
        //오브젝트가 다시 드러남
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
