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
    List<GameObject> AssembledBullet = new List<GameObject>();

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
        //탄두나 탄피 둘 중 하나라도 선택되지 않았다면
        if(WarHead == null || Cartridge == null)
            //실행하지 않음
            return;
        //총알이 생성되지 않은 상태라면
        if (Bullet == null)
            //총알 생성
            Bullet = Instantiate(prefab);

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
        //위치이동
        Vector3 tmpPosition = newBullet.transform.position;
        //개수마다 다른 x축 위치를 위한 부분
        tmpPosition.x = -9.5f + (1.5f * AssembledBullet.Count);
        tmpPosition.y = -4;
        newBullet.transform.position = tmpPosition;

        //리스트에 추가
        AssembledBullet.Add(newBullet);
        //무한 복제를 막기위해 매니저에게 정보를 전달하는 컴포넌트를 제거함
        Destroy(newBullet.GetComponent<BulletOnAssemble>());

        //슬라이더 객체 관리 컴포넌트 추가
        newBullet.AddComponent<BulletSlider>();
    }
    //마우스 오른쪽 클릭을 받기위한 함수
    public void Update()
    {
        //마우스 우클릭 시
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("우클릭 감지");
            //이벤트에 연결된 리스너들 호출함
            RightClick?.Invoke();
        }
    }
}
