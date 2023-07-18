using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//데이터 오가는거 확인용 샘플데이터
public class SampleBullet : Bullet
{
    //기존 생성자를 호출함.
    public SampleBullet()
    {
        base.Start();
        InsertData(new SampleWarhead(), new SampleCartridgeCase());
        Debug.Log($"bulletReady, damage: {Damage}");
    }
}
