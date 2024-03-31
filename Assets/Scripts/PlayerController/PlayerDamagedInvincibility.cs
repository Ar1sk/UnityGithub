using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedInvincibility : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private float InvincibilityDuration;

    private PlayerLife InvincibilityController;

    private void Awake()
    {
        InvincibilityController = GetComponent<PlayerLife>();
        anim = GetComponent<Animator>();
    }

    public void StartInvincibility()
    {
        InvincibilityController.StartInvincibility(InvincibilityDuration);
        anim.SetTrigger("getHit");
    }
}
