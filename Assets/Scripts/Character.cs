using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterClass))]
[RequireComponent(typeof(Transform))]

public class Character : MonoBehaviour
{
    [SerializeField]
    private CharacterCanvas characterCanvas;
    //player health 0-1 scale
    private float healthAmount;
    

    public int team;
    public Rigidbody2D rb;

    public float currentHealth;
    public CharacterClass characterClass;
    [SerializeField]
    private GameObject gameObject;


   

    void Start()
    {
        currentHealth = characterClass.maxHealth;
    }
    public float GetHealthAmount()
    {
        healthAmount = currentHealth/characterClass.maxHealth;
        healthAmount = Mathf.Clamp(healthAmount, 0f, 1f);
        return healthAmount;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
