﻿using System.Collections;
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
    [HideInInspector]
    public float currentHealth;
    public CharacterClass characterClass;
    [SerializeField]
    private GameObject gameObject;

    public bool dead;
    void Start()
    {
        currentHealth = characterClass.maxHealth;
        //Set the animationset of the character's class
        string animatorFilePath = characterClass.GetAnimatorFilePath();
          //  this.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animations/KnightMale/KnightMale.controller");
          //set the animator on the character
        this.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(animatorFilePath);
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
        if (currentHealth <= 0)
        {
            dead = true;
            Die();
        }
    }

    void Die()
    {
        this.GetComponent<Animator>().SetTrigger("Die");
        Destroy(gameObject,1.5f);
    }

}
