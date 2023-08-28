using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//랜덤으로 다음칸 후보를 만드는 애
public class GenerateStageList : MonoBehaviour
{

    //생성후 반환, 뭘?
    public NextStage newStage()
    {
        //정보 생성
        NextStage ret = new NextStage();

        //무슨 칸인지
        ret.type = (Type)Random.Range(0, 3);

        return ret;
    }

}

public enum Type
{
    Rest, Shop, Battle, Inconter
}
public struct NextStage
{
    //종류
    public Type type;
    //보스까지 몇칸 남았는지
    public int left;
}