using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierRunArea : MonoBehaviour
{
    public GameObject Playertarget { get; set; }
    private SoldierBase _enemy;

    private void Awake()
    {
        Playertarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<SoldierBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Playertarget)
        {
            _enemy.SetRunAway(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Playertarget)
        {
            _enemy.SetRunAway(false);
        }
    }
}
