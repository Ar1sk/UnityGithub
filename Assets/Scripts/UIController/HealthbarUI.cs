using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image healthBarForegroundImage;

    public void UpdateHealthBar(Health healthController)
    {
        healthBarForegroundImage.fillAmount = healthController.RemainingHealthPercentage;
    }
}
