using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    //해당 층이 가지는 버튼들
    public List<GameObject> buttons;

    //해당 층의 모든 버튼을 클릭 불가 모드로 전환
    public void OffClick()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<StageButton>().OffClick();
        }
    }
    //해당 층의 모든 버튼을 클릭 가능 모드로 전환
    public void OnClick()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<StageButton>().OnClick();
        }
    }
}
