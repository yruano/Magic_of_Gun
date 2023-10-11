using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHeelBall : MonsterMagic
{
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.velocity = Vector3.left * Speed;
        Destroy(gameObject, 1f);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Knight"))
        {
            Heel_Cal(other);
        }
    }
}
