using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBattleHUD : MonoBehaviour
{
    public GameObject hero1, hero2;
    GameObject[,] matrixHUD = new GameObject[4, 3];
    public int ActivePlayers = 2;

    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(true);
        matrixFiller();
        matrixActivator(ActivePlayers);
    }

    // Update is called once per frame
    void Update()
    {
        checkCharacterStatus();
    }

    void checkCharacterStatus()
    {
        if (!hero1.gameObject.GetComponent<Character>().IsAlive() || !hero2.gameObject.GetComponent<Character>().IsAlive())
        {
            gameObject.SetActive(true);
        }
    }
    void matrixFiller()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (j == 0)
                {
                    matrixHUD[i, j] = GameObject.Find((i + 1) + "IconMedal");
                }
                else if (j == 1)
                {
                    matrixHUD[i, j] = GameObject.Find((i + 1) + "PlacePic");
                }
                else
                {
                    matrixHUD[i, j] = GameObject.Find((i + 1) + "PlaceTexts");
                }
            }
        }
    }

    void matrixActivator(int playersCount)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (playersCount > i + 1)
                {
                    if (j == 0 || j == 1)
                    {
                        //matrixHUD[i, j].gameObject.GetComponent<Sprite>().texture.
                    }
                    if (j == 2)
                    {
                        matrixHUD[i, j].gameObject.GetComponent<TextEditor>().text = "";

                    }
                }
                else if(playersCount < i+1)
                {
                    if (j == 0 || j == 1)
                    {
                        matrixHUD[i, j].gameObject.GetComponent<SpriteRenderer>().gameObject.SetActive(false);
                    }
                    if (j == 2)
                    {
                        matrixHUD[i, j].gameObject.GetComponent<TextEditor>().text = "";

                    }
                }
                else
                {

                }
            }
        }
    }
}
