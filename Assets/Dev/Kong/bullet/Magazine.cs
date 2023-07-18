using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1번째 칸 y +1.4 x 0
//2번째 칸 y +0.9 x 0
//3번째 칸 y +0.4 x -0.17
//4번째 칸 y -0.1 x -0.17
//5번째 칸 y -0.6 x -0.34
//6번째 칸 y -0.1 x -0.51
//7번째 칸 y -0.1 x -0.51
public class Magazine : MonoBehaviour
{
    //탄창역할을 위해 bullet리스트를 담는 무언가
    [SerializeField]
    List<Bullet> bullets = new List<Bullet>();
}
