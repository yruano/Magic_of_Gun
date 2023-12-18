using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class AssembleManeger : MonoBehaviour
{
    //우클릭 시 슬라이더 안보이게 하기 위한 이벤트 연결용
    public event Action RightClick;

    //유저가 선택한 탄두와 탄피
    GameObject WarHead = null;
    GameObject Cartridge = null;

    //가조립된 총알
    GameObject Bullet = null;

    [SerializeField]
    //생성시 참조할 프리팹
    GameObject prefab = null;

    //조립된 총알을 저장할곳
    Dictionary<int ,GameObject> AssembledBullet = new Dictionary<int, GameObject>();
    //각각의 아이디 기억하기
    private int DicID = 0;

    //선택한 탄두-탄피 객체를 x축을 기준으로 이동시킴
    private void MoveObjectX(GameObject Object, int distance)
    {
        //임시 저장용 변수
        Vector3 tmpPosition;
        //선택한 객체의 좌표중
        tmpPosition = Object.transform.position;
        //x축 값을 수정하고
        tmpPosition.x += distance;
        //객체를 옮긴다
        Object.transform.position = tmpPosition;
    }

    //플레이어가 새로운 탄두를 선택한 상황 대응
    public void SelectWarHead(GameObject WarHead)
    {
        //기존 선택한 객체가 있었다면
        if (this.WarHead != null)
        {
            //객체 원위치
            MoveObjectX(this.WarHead, -1);
        }
        //선택한 객체 교체
        this.WarHead = WarHead;
        //객체를 선택한 객체위치로 이동
        MoveObjectX(WarHead, 1);

        //총알 조립 요청
        AssembleBullet();
    }
    //플레이어가 새로운 탄피를 선택함
    public void SelectCartridge(GameObject cartridge)
    {
        //기존 선택한 객체가 있었다면
        if (Cartridge != null)
        {
            //객체 원위치
            MoveObjectX(Cartridge, 1);
        }
        //선택한 객체 교체
        Cartridge = cartridge;
        //객체를 선택한 객체위치로 이동
        MoveObjectX(Cartridge, -1);

        //총알 조립 요청
        AssembleBullet();
    }

    //가조립된 총알 갱신
    public void AssembleBullet()
    {
        //시작전 예외조건 확인
        //탄두나 탄피 둘 중 하나라도 선택되지 않았다면
        if(WarHead == null || Cartridge == null)
            //실행하지 않음
            return;
        //총알이 생성되지 않은 상태라면
        if (Bullet == null)
        {
            //총알 생성
            Bullet = Instantiate(prefab);

            //Point의 자식으로 설정
            Bullet.transform.parent = GameObject.Find("Point").transform;

            //위치이동
            //위치 저장,
            Vector3 tmp = Bullet.transform.parent.transform.position;
            //x축 기준으로 살짝 틀어진 위치 조정
            tmp.x -= 0.1f;
            //객체 이동
            Bullet.transform.position = tmp;
        }

        //총알 데이터 교체
        Bullet Data = Bullet.GetComponent<Bullet>();

        //다시 클릭되는 일을 막기 위해 탄두와 탄피에 collider를 제거함
        //탄두 오브젝트 복사 및 collider제거
        GameObject tmpPointer = Instantiate(WarHead);
        Destroy(tmpPointer.GetComponent<CircleCollider2D>());
        Data.ReplaceWarHead(tmpPointer);
        //탄피 오브젝트 복사및 collider제거
        tmpPointer = Instantiate(Cartridge);
        Destroy(tmpPointer.GetComponent<BoxCollider2D>());
        Data.ReplaceCartridge(tmpPointer);
        //만들어진 총알 객체 색깔 새로고침
        Data.ColorRefresh();
    }

    //조립 확정 받은 총알을 리스트에 추가, instance해서 새로운 총알을 입력해야함.
    public void AddBullet(GameObject newBullet)
    {
        /* 총알을 아래로 나열하는 코드(였던 것), 현재 클릭하면 마우스 위치를 따라다니는 총알이 생성됨
        //새로운 총알은 마우스를 따라가지 않도록 조정
        newBullet.GetComponent<MouseDrag>().isFollowingMouse = false;

        //위치이동
        //위치 임시 저장
        Vector3 tmpPosition = newBullet.transform.position;
        //개수마다 다른 x축 위치를 위한 부분
        tmpPosition.x = -9.5f + (1.5f * AssembledBullet.Count);
        tmpPosition.y = -4;
        newBullet.transform.position = tmpPosition; */

        //크기조절
        newBullet.transform.localScale = new Vector3(0.45f, 0.45f, 1f);

        //이름 설정
        newBullet.name = "AssembledBullet_" + DicID + "_";

        //리스트에 추가
        AssembledBullet.Add(DicID++, newBullet);
        //무한 복제를 막기위해 매니저에게 정보를 전달하는 컴포넌트를 제거함
        Destroy(newBullet.GetComponent<BulletOnAssemble>());
        

        //매니저 연결
        newBullet.GetComponent<BulletOnAssemble>();

        //슬라이더 객체 관리 컴포넌트 추가
        newBullet.AddComponent<BulletSlider>();

        //마우스 클릭따라 옮겨지는 컴포넌트 켬
        newBullet.GetComponent<MouseDrag>().enabled = true;
    }

    //플레이어 입력을 받기위한 함수
    public void Update()
    {
        //마우스 우클릭 시
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("우클릭 감지");
            //이벤트에 연결된 리스너들 호출함
            RightClick?.Invoke();
        }
        //delete버튼 눌렸을 시
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Debug.Log("delete키 감지");
            //마우스에 뭐가 걸려있는지 확인
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //좌표 2차원으로 변경
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            //마우스 위치에 있는 콜라이더가 있는지 확인
            Collider2D collider = Physics2D.OverlapPoint(mousePosition2D);
            //콜라이더가 위치에 있고, 폴리곤콜라이더 = 총알 객체라면
            if(collider != null && collider is PolygonCollider2D)
            {
                //그 객체 참조 가져옴
                GameObject deleteTarget = collider.gameObject;

                //그 객체의 이름에 적혀있던 ID확인
                int deleteID = int.Parse(deleteTarget.name.Split('_')[1]);

                //ID로 리스트에서 삭제
                AssembledBullet.Remove(deleteID);

                //이벤트에서 제거
                RightClick -= deleteTarget.GetComponent<BulletSlider>().HideSlider;

                //객체파괴
                Destroy(deleteTarget);
            }
        }
    }
}
