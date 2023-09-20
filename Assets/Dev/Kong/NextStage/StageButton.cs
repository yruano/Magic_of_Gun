using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//스테이지 버튼이 가져야할 요소를 가짐
public class StageButton : MonoBehaviour
{
    //보스층까지 필요한 턴 수 표시하는 글자
    [SerializeField]
    StageText st = null;
    //생성시 필요한 정보를 가지는곳
    [SerializeField]
    public NextStage Data;
    //넘어갈 다음 씬이름
    [SerializeField]
    public string nextScene = "";

    //초기화
    public void Awake()
    {
        //컴포넌트 연결
        st = GetComponentInChildren<StageText>();
    }

    //글자 위치 다시잡기용 
    public void MoveNumber()
    {
        st.MoveToParent();
    }

    //글자 정보 갱신
    public void RefreshNumberInfor()
    {
        //start가 먼저 실행이 안되서 비었을때 대비용 if문
        if(st == null)
        {
            st = GetComponentInChildren<StageText>();
        }
        if(st.TextMesh == null)
        {
            st.TextMesh = st.GetComponent<TMP_Text>();
        }
        //글자 정보 실제로 갱신하는부분
        //디버그용, left가 비었으면 로그에 남김
        Debug.Log(Data.left);
        st.TextMesh.text = "" + Data.left;
    }

    //클릭 시 다음 씬으로 넘어가기
    public void OnMouseDown()
    {
        //제대로 입력되어 있을때만 넘어가기
        if (nextScene != "")
            Debug.Log($"{gameObject.name}: Connected Wrong Scene");
        else
            Debug.Log($"{gameObject.name}: No Connected Scene"); 
    }
}
