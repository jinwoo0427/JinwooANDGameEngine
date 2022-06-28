using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Text healthText;
    public Image healthBar;

    float health, maxHealth;
    float lerpSpeed;

    private void Start()
    {
        health = maxHealth;
    }
    public void InitHp(float maxhp)
    {
        maxHealth = maxhp;
        health = maxhp;
    }
    private void Update()
    {
        //healthText.text = health.ToString();
        healthText.text = Mathf.Clamp(health, 0, maxHealth).ToString();
        if (health > maxHealth) health = maxHealth;

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (health / maxHealth), lerpSpeed);
    }
    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.black, Color.red, (health / maxHealth));
        healthBar.color = healthColor;
    }

    public void Damage(float damagePoints)
    {
        if (health > 0)
            health -= damagePoints;
    }
    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
        health += healingPoints;

        //if (health + healingPoints< maxHealth)
        //{
        //    health += healingPoints;
        //}
    }
    public void MaxHpUp(float max)
    {
        maxHealth += max;
    }


}
