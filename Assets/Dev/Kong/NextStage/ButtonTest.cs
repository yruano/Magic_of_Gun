using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//글자 위치 다시잡기용 !!임 시!!
public class ButtonTest : MonoBehaviour
{
    [SerializeField]
    StageText st = null;
    public void OnMouseDown()
    {
        st = GetComponentInChildren<StageText>();
        st.MoveToParent();
    }
}
