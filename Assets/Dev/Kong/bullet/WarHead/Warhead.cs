using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warhead : MonoBehaviour
{
    [SerializeField]
    //탄두가 가져야할 기본적인 요소
    //탄두 데미지
    public int Damage = 0;
    //탄두가 가지는 버프 제거/디버프 등등의 id를 담을 곳
    List<int> Buff = null;
    //타입
    int type = -1;
    int ID = -1;
}