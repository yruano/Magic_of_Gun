using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//버튼의 정보 생성 담당

//버튼이 가질 수 있는 타입 종류
public enum Type
{
    Rest, Shop, Battle, Inconter
}

//버튼을 만들 떄 필요한 데이터를 저장하는 구조체
public struct NextStage
{
    //종류
    public Type type;
}

//랜덤으로 다음칸 후보를 만듦
public class GenerateStageList
{
    //생성후 만들어진 정보 반환, 매개변수가 비어있다면 무슨칸인지만 랜덤으로 생성
    public NextStage newStage()
    {
        //정보 생성
        NextStage ret = new NextStage();

        //무슨 칸인지 정보 랜덤선택, 0이상 4미만
        ret.type = (Type)Random.Range(0, 4);

        return ret;
    }

}