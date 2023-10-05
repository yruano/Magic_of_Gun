using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMagic : MonoBehaviour
{
    protected Rigidbody2D _rb2d;
    public int Damage;
    public int Heel;

    protected virtual void Damage_Cal(Collider2D other)
    {
        var damageable = other.GetComponent<IDamageable>();
        damageable?.Damage(Damage);

        Destroy(gameObject);
    }

    protected virtual void Heel_Cal(Collider2D other)
    {
        var damageable = other.GetComponent<IDamageable>();
        damageable?.Heel(Heel);

        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Damage_Cal(other);
        }
    }
}
