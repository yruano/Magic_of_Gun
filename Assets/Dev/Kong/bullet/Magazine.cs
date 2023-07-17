using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    //탄창역할을 위해 bullet리스트를 담는 무언가
    [SerializeField]
    List<Bullet> bullets = new List<Bullet>();
}
