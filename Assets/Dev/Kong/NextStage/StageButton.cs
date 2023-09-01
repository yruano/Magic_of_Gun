using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스테이지 버튼이 가져야할 요소를 가짐
public class StageButton : MonoBehaviour
{
    //보스층까지 필요한 턴 수 표시하는 글자
    [SerializeField]
    StageText st = null;
    //생성시 필요한 정보를 가지는곳
    [SerializeField]
    public NextStage Data;


    //글자 위치 다시잡기용 !!임 시!!
    public void OnMouseDown()
    {
        st = GetComponentInChildren<StageText>();
        st.MoveToParent();
    }
}
