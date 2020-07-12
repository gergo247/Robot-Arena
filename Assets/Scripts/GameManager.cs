using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Character> AllCharactersInScene = new List<Character>();
    [SerializeField]
    private GameObject sceneCamera;
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one gamemanadger in scene.");
        else
            instance = this;
    }
    void Start()
    {
        LookForEveryCharacterInScene();

     //   PlayerTeam.instance.PutIntoTeam();

        PlayerTeam.instance.SpawnTeam();
    }
    void LookForEveryCharacterInScene()
    {
        GameObject[] targetGO = GameObject.FindGameObjectsWithTag("Character");
        if (targetGO != null)
        {
            foreach (GameObject go in targetGO)
            {
                Character charToSave = go.GetComponent(typeof(Character)) as Character;
                if (charToSave != null)
                {
                    AllCharactersInScene.Add(charToSave);
                }
            }

        }
    }
}
