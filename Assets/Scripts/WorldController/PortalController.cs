using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private Transform destination;
    GameObject player;
    Rigidbody2D rb;
    Animation anim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animation>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            { 
                StartCoroutine(PortalIn());
            }
        }
    }

    IEnumerator PortalIn()
    {
        rb.simulated = false;
        yield return new WaitForSeconds(0.5f);
        player.transform.position = destination.transform.position;
        yield return new WaitForSeconds(0.5f);
        rb.simulated = true;
    }
}
