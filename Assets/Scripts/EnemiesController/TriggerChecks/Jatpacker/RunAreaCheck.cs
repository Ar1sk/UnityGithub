using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAreaCheck : MonoBehaviour
{
    public GameObject Playertarget { get; set; }
    private EnemyBase _enemy;

    private void Awake()
    {
        Playertarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<EnemyBase>();
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
