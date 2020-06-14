using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    [SerializeField]
    private Character character;
    CharacterAi characterAiScript;

    public void DamageTargetFromAnimation()
    {
        characterAiScript = character.GetComponent<CharacterAi>();
            if (characterAiScript != null)
        characterAiScript.DamageTargetFromAnimation();
    }
}
