using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    //탄두 데이터
    public Warhead head = null;
    //탄피 데이터
    public CartridgeCase cartridge = null;
    public int Damage = 0;

    public void Start()
    { 
        head = null;
        cartridge = null;
    }
    public void InsertData (Warhead head,  CartridgeCase cartridge)
    {
        this.head = head;
        this.cartridge = cartridge;
        Damage = head.Damage + cartridge.Damage;
    }

}
