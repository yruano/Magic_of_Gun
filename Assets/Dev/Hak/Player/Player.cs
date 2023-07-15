using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    public int MaxHP = -1;
    public int HP = -1;
    public int Shield = 0;
    public Gun gun = null;
    public List<List<Bullet>> MagazineBelt = new List<List<Bullet>>();
    //데미지 피격구현
    public void Damage(int damage) 
    {
        //먼저 쉴드가 제거됨
        Shield -= damage;
        //만약 쉴드 잔량보다 데미지가 많다면
        if (Shield < 0)
        {
            //남은 데미지를 체력에 적용
            HP += Shield;
            //쉴드 잔량 0으로
            Shield = 0;
            
            //만약 체력이 없다면
            if(HP <= 0)
            {
                /*
                 사망 처리, 현재 구현 단계 아님
                 */
            }
        }
    }
    

    
}
