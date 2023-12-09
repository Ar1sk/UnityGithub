using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackArea : MonoBehaviour
{
    private int damage = 25;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<EnemiesHealth>() != null)
        {
            EnemiesHealth health = collider.GetComponent<EnemiesHealth>();
            health.Damage(damage);
        }
    }
}
