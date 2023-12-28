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

    //클릭 시 다음 씬으로 넘어가기
    public void OnMouseDown()
    {
        //부모 있는지 확인해서 있다면 이름 연결, 없다면 ---- 적음
        string parentName = 
            transform.parent == null ? "----" : transform.parent.name;
        //제대로 입력되어 있을때만 넘어가기
        if (nextScene == "" || nextScene == "-")
            Debug.Log($"{parentName} | {gameObject.name}: No Connected Scene");
        else
        {
            //씬 이동
            SceneManager.LoadScene(nextScene);
            //map객체 끄기(부모의 부모)
            transform.parent.parent.GetComponent<Map>().OffFloors();
        }
    }

    //클릭 불가모드
    public void OffClick()
    {
        //클릭 받을 콜라이더를 끔
        gameObject.GetComponent<CircleCollider2D>().enabled = false;

        //반투명하게 만듬
        //현재 색상 저장
        Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;

        //알파값 변경
        currentColor.a = 0.4f;

        //변경된 값 적용
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
    }
    //클릭 가능 모드
    public void OnClick()
    {
        //클릭 받을 콜라이더를 켬
        gameObject.GetComponent<CircleCollider2D>().enabled = true;

        //불투명하게 만듬
        //현재 색상 저장
        Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;

        //알파값 변경
        currentColor.a = 1f;

        //변경된 값 적용
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
    }
}