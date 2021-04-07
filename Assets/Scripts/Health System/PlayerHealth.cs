using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerMaxHealth = 10000;
    public int playerCurrentHealth;

    public PlayerHealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        playerCurrentHealth -= damageAmount;

        if (playerCurrentHealth <= 0)
        {
            playerCurrentHealth = 0;
            //Destroy(gameObject);
        }

        healthBar.SetHealth(playerCurrentHealth);

        //LogDamage();
    }

    void LogDamage()
    {
        Debug.Log("Vida player: " + playerCurrentHealth);
    }
}
