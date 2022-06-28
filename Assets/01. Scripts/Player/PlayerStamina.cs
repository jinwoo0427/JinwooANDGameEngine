using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public Image ringHealthBar;

    public float currentStamina, maxStamina;
    float lerpSpeed;

    private void Start()
    {
        currentStamina = maxStamina;
    }
    public void InitStamina(float Stamina)
    {
        maxStamina= Stamina;
        currentStamina = Stamina;
    }
    private void Update()
    {
        if (currentStamina > maxStamina) currentStamina = maxStamina;

        lerpSpeed = 3f * Time.deltaTime;

        StaminaBarFiller();
        ColorChanger();
    }

    void StaminaBarFiller()
    {
        ringHealthBar.fillAmount = Mathf.Lerp(ringHealthBar.fillAmount, (currentStamina / maxStamina), lerpSpeed);
    }
    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (currentStamina / maxStamina));
        ringHealthBar.color = healthColor;
    }

    public void MinusStamina(float damagePoints)
    {
        if (currentStamina > 0)
            currentStamina -= damagePoints;
    }
    public void HealStamina(float healingPoints)
    {
        if (currentStamina < maxStamina)
            currentStamina += healingPoints;
    }
}
