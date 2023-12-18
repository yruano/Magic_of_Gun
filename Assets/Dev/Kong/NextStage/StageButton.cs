using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//스테이지 버튼이 가져야할 요소를 가짐
public class StageButton : MonoBehaviour
{
    //생성시 필요한 정보를 가지는곳
    public NextStage Data;
    //넘어갈 다음 씬이름
    public string nextScene = "";
    //해당 층에서의 구별ID
    public int onFloorID = -1;
    //연결된 다음 층의 버튼ID
    public List<int> connectedFloorID = new List<int>();

    /*
    //초기화//현재 기능없음
    public void Awake()
    {
        
    }
    */

    //클릭 시 다음 씬으로 넘어가기
    public void OnMouseDown()
    {
        //부모 있는지 확인해서 있다면 이름 연결, 없다면 ---- 적음
        string parentName = 
            transform.parent == null    ?    "----" : transform.parent.name;
        //제대로 입력되어 있을때만 넘어가기
        if (nextScene == "")
            Debug.Log($"{parentName} | {gameObject.name}: No Connected Scene");
        else
            Debug.Log($"{parentName} | {gameObject.name}: Function Not Ready, Work In Progress");
    }
}