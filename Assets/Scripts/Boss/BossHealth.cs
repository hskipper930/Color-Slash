using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public int maxHealth = 20;
    public int currentHealth;

    public BossHealthBar bossHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        bossHealthBar.SetMaxHealth(maxHealth);
    }

    public void BossBarDamage(int damage)
    {
        currentHealth -= damage;

        bossHealthBar.SetHealth(currentHealth);
    }
}
