using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardStats : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Logique de mort du garde
        Destroy(gameObject); // Par exemple, tu peux détruire l'objet du garde
    }
}
