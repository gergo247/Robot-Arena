using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField]
    private Text healthStatusText;
    [SerializeField]
    private RectTransform healthStatusTransform;
    [SerializeField]
    private Character character;


    void Update()
    {
        SetHealth(character.GetHealthAmount());
        healthStatusText.text = string.Format("{0}/{1}",character.currentHealth,character.characterClass.maxHealth);
    }

    public void SetHealth(float fillAmount)
    {
        healthStatusTransform.localScale = new Vector2(fillAmount, 1);
    }
}
