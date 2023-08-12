using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AssembleManeger : MonoBehaviour
{
    //유저가 선택한 탄두와 탄피
    GameObject WarHead = null;
    GameObject Cartridge = null;
    //가조립된 총알
    GameObject Bullet = null;
    [SerializeField]
    //생성시 참조할 프리팹
    GameObject prefab = null;

    //선택한 객체를 x축을 기준으로 이동시킴
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

    //플레이어가 새로운 탄두를 선택함
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
        //객체 이동
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
        //객체 이동
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
        //총알이 생성되지 않았다면
        if (Bullet == null)
            //총알 생성
            Bullet = Instantiate(prefab);

        //총알 데이터 교체
        Bullet Data = Bullet.GetComponent<Bullet>();
        Data.ReplaceWarHead(Instantiate(WarHead));
        Data.ReplaceCartridge(Instantiate(Cartridge));
        Data.ColorRefresh();
    }
}
