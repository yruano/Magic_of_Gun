using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMagic : MonoBehaviour
{
    protected Rigidbody2D _rb2d;
    public int Damage;
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var damageable = other.GetComponent<IDamageable>();
            damageable?.Damage(Damage);

            Destroy(gameObject);
        }
    }
}
