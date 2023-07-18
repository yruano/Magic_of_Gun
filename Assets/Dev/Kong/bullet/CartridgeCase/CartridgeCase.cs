using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeCase : MonoBehaviour
{
    public int Damage= 0;
    public int ID = -1;
    //탄피가 보여져야할 색깔, #RGB, 기본은 황금색 = #ffae00
    Color color = new Color(0xFF / 255f,
                            0xAE / 255f,
                            0x00 / 255f);
}
