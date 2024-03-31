using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedInvincibility : MonoBehaviour
{
    [SerializeField] private float InvincibilityDuration;

    private PlayerLife InvincibilityController;

    private void Awake()
    {
        InvincibilityController = GetComponent<PlayerLife>();
    }

    public void StartInvincibility()
    {
        InvincibilityController.StartInvincibility(InvincibilityDuration);
    }
}
