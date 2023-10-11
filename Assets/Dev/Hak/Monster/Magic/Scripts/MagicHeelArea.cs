using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHeelArea : MonsterMagic
{
    private float _timer = 0f;
    private int _interval;
    private List<float> _intervals = new();

    private void Awake()
    {
        _intervals = new List<float> { 0.5f, 0.25f, 0.2f, 0.15f, 0.1f };
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.velocity = new Vector2(0, -Speed);
        RandomInterval();
    }

    private void RandomInterval()
    {
        _interval = Random.Range(0, _intervals.Count);
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        _timer += Time.deltaTime;

        if (other.CompareTag("Knight") && _timer >= _intervals[_interval])
        {
            Heel_Cal(other);
            _timer = 0f;
            RandomInterval();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Knight"))
        {
            Destroy(gameObject);
        }
    }
}