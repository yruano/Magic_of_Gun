using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//가조립된 총알을 클릭시, 매니저에게 정보를 보내서 리스트에 추가하도록 하는 역할임
public class BulletOnAssemble : MonoBehaviour
{
    public AssembleManeger AM = null;

    public void Awake()
    {
        if(AM == null) 
            AM = FindObjectOfType<AssembleManeger>();
    }

    private void OnMouseDown()
    {
        //클릭시 복제본을 전달함
        AM.AddBullet(Instantiate(gameObject));
    }
}
