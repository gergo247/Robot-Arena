using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Class", menuName = "Class")]
public class CharacterClass : ScriptableObject
{
    public float maxHealth;
    public float autoAttackCooldown;
    public float damage;
    public float attackRange;
    public float distanceToKeepWithTarget;
}
