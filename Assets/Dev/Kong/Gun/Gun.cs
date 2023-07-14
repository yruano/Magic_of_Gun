using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
//총들이 공통적으로 갖는 데이터와 함수이름 정의
public class Gun : MonoBehaviour
{
    [SerializeField]
    //최대 탄창크기
    public int MaxMagazine = -1;
    //턴당 발사 가능 수
    public int BulletPerTrun = -1;
    //현재 총에 장착된 탄창
    public List<Bullet> Magazines = new List<Bullet>();
    //집중 사용 여부
    public bool Focus = false;
    //기능고장 확률, 0~1
    public float JamChance = 0;

    //총알 구성품 부족 오류
    public class LackOfAssemblyException : System.Exception
    {
        public LackOfAssemblyException(Warhead wh, CartridgeCase cc)
        {
        }
    }

    //탄창에서 가장 위에있는 총알 반환. 계산은 총알 정보 받아서 알아서함(버프 정보때문에 이래야함)
    public Bullet Fire()
    {
        //탄창이 비었다면
        if(Magazines.Count <= 0) 
        {
            //비었다는 의미로 null 반환
            return null;
        }

        //쏠 총알의 구성품이 모자라다면 
        if (Magazines[0].head == null || Magazines[0].cartridge == null)
        {
            //예외 던짐, 하나라도 비어있으면 총알생성/전달에 매우 큰 문제가 생긴거임, 진행하면 안됨.
            throw new LackOfAssemblyException(Magazines[0].head, Magazines[0].cartridge);
        }

        //다른 오류가 없다면
        //탄창의 제일 위에있는 총알을 골라
        Bullet ret = Magazines[0];
        //탄창에서 제거하고
        Magazines.RemoveAt(0);
        //객체 반환
        return ret;
    }
    
    //탄창 교체
    public List<Bullet> Reload(List<Bullet> NewMagazine)
    {
        //기존 탄창 기억
        List<Bullet> ret = Magazines;
        //새로운 탄창으로 참조 바꾸고,
        Magazines = NewMagazine;
        //기존 탄창 반환.
        return ret;
    }

    //탄창 제거
    public List<Bullet> RejectMagazine()
    {
        //기존 탄창 기억
        List<Bullet> ret = Magazines;
        //기존 탄창은 참조 제거하고
        Magazines = null;
        //기존 탄창 반환.
        return ret;
    }

    //탄창 삽입
    public List<Bullet> InsertMagazine(List<Bullet> NewMagazine)
    {
        //만약 이미 탄창이 들어와있다면
        if(Magazines != null)
        {
            //입력받은거 그대로 다시 반환
            return NewMagazine;
        }
        //탄창이 비어있다면
        else
        {
            //입력받은거 탄창에 연결
            Magazines = NewMagazine;
            //null반환으로 연결되었다고 표시
            return null;
        }
    }
}
