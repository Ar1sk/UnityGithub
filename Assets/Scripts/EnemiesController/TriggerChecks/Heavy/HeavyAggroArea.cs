using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAggroArea : MonoBehaviour
{
    public GameObject Playertarget { get; set; }
    private HeavyBase _enemy;

    private void Awake()
    {
        Playertarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<HeavyBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Playertarget)
        {
            _enemy.SetAggroStatus(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Playertarget)
        {
            _enemy.SetAggroStatus(false);
        }
    }
}
