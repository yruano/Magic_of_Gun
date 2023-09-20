using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//랜덤으로 다음칸 후보를 만드는 애
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
    //매개변수가 1개거나 최대가 최소보다 작다면 남은칸을 최소랑 같게 설정해서 반환
    public NextStage nextStage(int min)
    {
        return nextStage(min, min - 1);
    }
    //매개변수가 2개라면 
    public NextStage nextStage(int min, int max)
    {
        //새로 만들고,
        NextStage ret = newStage();
        //최대가 최소보다 작다면
        if (min > max)
        {
            //남은 칸을 최소로 설정하고
            ret.left = min;
        }
        //아니라면
        else
        {
            //범위 내로 남은 칸 설정하고
            ret.left = Random.Range(min, max+1);
        }
        //반환
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