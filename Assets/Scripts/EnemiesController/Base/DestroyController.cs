using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyController : MonoBehaviour
{
    public void DestroyEnemy(float delay)
    {
        Destroy(gameObject, delay);
    }
}
