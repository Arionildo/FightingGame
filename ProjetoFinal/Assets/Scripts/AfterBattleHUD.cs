using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterBattleHUD : MonoBehaviour
{
    private float hero1HP, hero2HP, hero3HP, hero4HP;
    private GameObject[,] matrixHUD = new GameObject[4, 3];
    public GameObject hero1, hero2, hero3, hero4;
    public int ActivePlayers = 2;
    public string[] PlayerPosition = new string[4];
    public GameObject scorePanel;

    // Use this for initialization
    void Start()
    {
        startFunctions();
    }

    // Update is called once per frame
    void Update()
    {
        checkCharacterStatus();
    }

    void checkCharacterStatus()
    {
        instantiateHeroHPs();
        if (hero1HP <= 0 || hero2HP <= 0)
        {
            scorePanel.gameObject.SetActive(true);
            matrixActivator(ActivePlayers);
            updatePlayerPosition();
        }
    }

    void startFunctions()
    {
        onStartPlayerPosition();
        scorePanel.gameObject.SetActive(false);
        matrixFiller();
        instantiateHeroHPs();
    }

    void onStartPlayerPosition()
    {
        for(int i = 0; i <PlayerPosition.Length; i++)
        {
            PlayerPosition[i] = "-";
        }
    }

    void updatePlayerPosition()
    {
        if (hero1HP <= 0)
        {
            for (int i = 0; i < PlayerPosition.Length; i++)
            {
                string player = "Player1";
                if (PlayerPosition[i] == player)
                {
                    break;
                }
                else if(PlayerPosition[i] == "-")
                {
                    PlayerPosition[i] = player;
                    break;
                }
            }
        }

        if (hero2HP <= 0)
        {
            for (int i = 0; i < PlayerPosition.Length; i++)
            {
                string player = "Player2";
                if (PlayerPosition[i] == player)
                {
                    break;
                }
                else if (PlayerPosition[i] == "-")
                {
                    PlayerPosition[i] = player;
                    break;
                }
            }
        }

        if (hero3HP <= 0)
        {
            for (int i = 0; i < PlayerPosition.Length; i++)
            {
                string player = "Player3";
                if (PlayerPosition[i] == player)
                {
                    break;
                }
                else if (PlayerPosition[i] == "-")
                {
                    PlayerPosition[i] = player;
                    break;
                }
            }
        }

        if (hero4HP <= 0)
        {
            for (int i = 0; i < PlayerPosition.Length; i++)
            {
                string player = "Player4";
                if (PlayerPosition[i] == player)
                {
                    break;
                }
                else if (PlayerPosition[i] == "-")
                {
                    PlayerPosition[i] = player;
                    break;
                }
            }
        }
        for (int i = 0; i < PlayerPosition.Length; i++)
        {
            Debug.Log(PlayerPosition[i] + " = " + i);
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
                    matrixHUD[i, j] = scorePanel.transform.Find((i + 1) + "IconMedal").gameObject;
                }
                else if (j == 1)
                {
                    matrixHUD[i, j] = scorePanel.transform.Find((i + 1) + "PlacePic").gameObject;
                }
                else
                {
                    matrixHUD[i, j] = scorePanel.transform.Find((i + 1) + "PlaceTexts").gameObject;
                }
            }
        }
    }

    void instantiateHeroHPs()
    {
        hero1HP = hero1.gameObject.GetComponent<Character>().currentLife;
        hero2HP = hero2.gameObject.GetComponent<Character>().currentLife;
        //hero3HP = hero3.gameObject.GetComponent<Character>().currentLife;
        //hero4HP = hero4.gameObject.GetComponent<Character>().currentLife;
    }

    void lastStandingCheck(int playersCount)
    {

    }

    void matrixActivator(int playersCount)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (playersCount >= i + 1)
                {
                    if (j == 0 || j == 1)
                    {
                        matrixHUD[i, j].gameObject.SetActive(true);
                    }
                    if (j == 1)
                    {
                        string playerIndex = PlayerPosition[i].Substring(PlayerPosition[i].Length - 1);
                        matrixHUD[i, j].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("hero0"+playerIndex+"_icon");
                    }
                    if (j == 2)
                    {
                        if (PlayerPosition[i] == "Player1" || PlayerPosition[i] == "Player2")
                        {
                            matrixHUD[i, j].gameObject.GetComponent<Text>().text = PlayerPosition[i];
                        }
                    }
                }
            else if (playersCount < i + 1)
            {
                matrixHUD[i, j].gameObject.SetActive(false);
            }
            }
        }
    }

}
