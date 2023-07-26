using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.PackageManager.UI;
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
    //발사한 횟수
    public int FiredCount = 0;

    [SerializeField]
    //총알 스크립트를 만들기 위해 참조할 프리팹
    GameObject prefab;
    //현재 총에 들어온 총알 정보
    public List<Bullet> Magazine = new List<Bullet>();
    //총에 어떤 총알이 있는지 플레이어에게 보여주기 위한 오브젝트 리스트
    public List<GameObject> bulletSprite = new List<GameObject>();
    //탄창을 클릭했을 때 정보를 받아오기 위한 변수
    public GameObject nextMagazine = null;

    //집중 사용 여부
    public bool Focus = false;
    //기능고장 확률, 0~1
    public float JamChance = 0;
    //조준하고 있는 대상을 저장함
    public GameObject Target = null;

    //이벤트 구독 연결
    public EventBus eventBus = null;

    //초기화
    private void Awake()
    {
        //이벤트 버스 구독준비
        if(eventBus == null)
        {
            //이벤트 연결
            eventBus = FindObjectOfType<EventBus>();
        }

        //목표 지정 이벤트 구독
        eventBus.ClickMonster += GetTarget;

        //방아쇠 이벤트 구독
        eventBus.ClickTriger += HitTarget;

        //턴 시작 이벤트 구독
        eventBus.PlayerTurn += PlayerTurn;

        //탄창 선택 이벤트 구독
        eventBus.ClickMagazine += NewMagazine;

        //탄창 교체 확정 이벤트 구독
        eventBus.ClickLoadConfirmed += Reload;
    }

    //탄창에서 가장 위에있는 총알 반환. 계산은 총알 정보 받아서 알아서함(버프 정보때문에 이래야함)
    public Bullet Fire()
    {
        //탄창이 비었다면
        if(Magazine.Count <= 0) 
        {
            //비었다는 의미로 null 반환
            return null;
        }

        //다른 오류가 없다면
        //로그에 남기고
        Debug.Log($"fired: {gameObject.name}");
        //탄창의 제일 위에있는 총알을 골라
        Bullet ret = Magazine[0];
        //탄창에서 제거하고
        Magazine.RemoveAt(0);

        //발사횟수 증가하고
        FiredCount++;

        //총알 스프라이트 위치 조정하고
        MoveBulletSptite();

        //객체 반환
        return ret;
    }
    
    //목표 선택 이벤트 리스너
    public void GetTarget(GameObject target)
    {
        this.Target = target;
    }

    //방아쇠 이벤트 리스너
    public void HitTarget(int info)
    {
        //fire함수를 통해 탄창관련 처리 후 하나 받아옴
        Bullet damage = Fire();
        //탄창이 비어 받아온게 없다면
        if(damage == null)
        {
            Debug.Log($"out of ammo: {gameObject.name}");
            //탄창 비었다는 이벤트 발생
        }
        //비지 않았다면
        else
        {
            Debug.Log($"{gameObject.name}: gun shoted, target: {Target.name}");
            //목표에게 데미지 전달
            Target.GetComponent<IDamageable>().Damage(damage.Damage);
        }
    }

    //플레이어 턴 시작 이벤트 리스너
    public void PlayerTurn()
    {
        //발사횟수 초기화
        FiredCount = 0;
    }

    //탄창 선택 이벤트 리스너
    public void NewMagazine(GameObject newMagazine)
    {
        //새로운거로 교체
        nextMagazine = newMagazine;
    }

    //탄창 교체 확정 이벤트 리스너
    public void Reload(int info)
    {
        //기존 탄창 데이터 버림
        Magazine.Clear();
        //새로운 데이터를 넣음
        Magazine.AddRange(nextMagazine.GetComponent<Magazine>().bullets);
    }

    //잔탄을 보여주기 위해 총에 들어가있는 총알 데이터를 기반으로 총알 스프라이트 생성
    public void MakeBulletSprite()
    {
        //기존 리스트 비움
        bulletSprite.Clear();
        //탄창 정보따라 스프라이트-오브젝트를 만듬
        foreach (var bullet in Magazine)
        {
            //먼저 프리팹을 참조해 오브젝트를 만듬
            bulletSprite.Add(Instantiate(prefab));
            //그 후 자식 오브젝트로 만듬
            bulletSprite[bulletSprite.Count - 1].transform.SetParent(transform);
        }
        //위치조정 //!!!여기까지 했음!!!
        MoveBulletSptite();
    }

    //총알 스프라이트의 위치를 조정함.
    public void MoveBulletSptite()
    {
        for (int i = 0; i < Magazine.Count; i++)
        {
            //좌표 설정
            Magazine[i].gameObject.transform.localPosition = new Vector3(0f, 0.3884358f - (i * 0.1387271f), 0f);
        }
    }

    /* 탄창 교체 감이 안잡혔을 때 썼던 함수
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
    */
}
