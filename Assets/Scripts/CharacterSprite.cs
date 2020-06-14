using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    [SerializeField]
    private Character character;

    CharacterAi characterAiScript;
    void start()
    {
       //go = GameObject.Find("GameManager");
       //goScript = (GameManagerScript)go.GetComponent(typeof(GameManagerScript));

      //  characterAiScript = (CharacterAi)character.GetComponent(typeof(CharacterAi));
    }

    public void DamageTargetFromAnimation()
    {
        //  if (characterAiScript != null)
        //  characterAiScript.DamageTargetFromAnimation();

        characterAiScript = character.GetComponent<CharacterAi>();
            if (characterAiScript != null)
        characterAiScript.DamageTargetFromAnimation();
    }
}
