using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Class", menuName = "Class")]
public class CharacterClass : ScriptableObject
{
    public float maxHealth;
    public float autoAttackCooldown;
    public float damage;
    public float attackRange;
    public float distanceToKeepWithTarget;
    public List<AnimationClip> attakcAnimations;
    [SerializeField]
     Object animator;
    public string GetAnimatorFilePath()
    {
        string animatorFilePath =  AssetDatabase.GetAssetPath(animator);
        if (animatorFilePath.StartsWith("Assets/Resources/"))
        {
            animatorFilePath = animatorFilePath.Remove(0, 17);
        }
        if (animatorFilePath.EndsWith(".controller"))
        {
            animatorFilePath = animatorFilePath.Substring(0, animatorFilePath.LastIndexOf(".controller"));
        }
        return animatorFilePath;
    }
}
