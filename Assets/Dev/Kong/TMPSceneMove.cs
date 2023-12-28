using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TMPSceneMove : MonoBehaviour
{
    public string targetScene;

    public void OnMouseDown()
    {
        //씬 이동
        SceneManager.LoadScene(targetScene);
    }
}
