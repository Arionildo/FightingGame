using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{

    public static GameController instance = null;
    public int characterCount = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    void InitGame()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            Character character = go.GetComponent<Character>();
            instance.characterCount++;
            character.id = instance.characterCount;
        }

        foreach (string joy in Input.GetJoystickNames())
            print(joy);

    }

    void Update()
    {
        ClearSpecialSkills();
    }

    private void ClearSpecialSkills()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Special"))
        {
            Destroy(go, 1f);
        }
    }
}