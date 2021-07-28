using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public float currentHealth;

    float maxHealth = 100f;
    Animator anim;
    public CanvasScript healthBar;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        healthBar.EnemySetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        transform.GetComponent<EnemyCombat2>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerCombat>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
    }
}
