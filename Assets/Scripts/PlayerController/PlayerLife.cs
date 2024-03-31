using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Health healthController;


    private void Awake()
    {
        healthController = GetComponent<Health>();
    }

    public void StartInvincibility(float InvincibilityDuration)
    {
        StartCoroutine(InvincibilityCoroutine(InvincibilityDuration));
    }
     
    private IEnumerator InvincibilityCoroutine(float InvincibilityDuration)
    {
        healthController.IsInvincible = true; 
        yield return new WaitForSeconds(InvincibilityDuration);
        healthController.IsInvincible = false;
    }
}
