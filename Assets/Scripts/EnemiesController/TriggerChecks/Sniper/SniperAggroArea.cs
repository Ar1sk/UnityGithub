using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAggroArea : MonoBehaviour
{
    public GameObject Playertarget { get; set; }
    private SniperBase _enemy;

    private void Awake()
    {
        Playertarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<SniperBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Playertarget)
        {
            _enemy.SetAggroStatus(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Playertarget)
        {
            _enemy.SetAggroStatus(true);
        }
    }
}
