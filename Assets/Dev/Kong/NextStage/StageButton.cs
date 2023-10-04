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
    TMP_Text st = null;
    //생성시 필요한 정보를 가지는곳
    [SerializeField]
    public NextStage Data;
    //넘어갈 다음 씬이름
    [SerializeField]
    public string nextScene = "";

    //초기화
    public void Awake()
    {
        //텍스트 컴포넌트 연결
        st = GetComponentInChildren<TMP_Text>();
    }

    //글자 정보 갱신
    public void RefreshNumberInfor()
    { 
        //글자 정보 실제로 갱신하는부분
        st.text = "" + Data.left;
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
