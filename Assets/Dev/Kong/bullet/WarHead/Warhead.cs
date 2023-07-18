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
    public List<int> Buff = null;
    //타입
    public int type = -1;
    public int ID = -1;
    //탄두가 보여져야할 색깔, #RGB, 기본은 황금색 = #ffd700
    Color color = new Color(0xFF / 255f, 
                            0xD7 / 255f, 
                            0x00 / 255f);
}
