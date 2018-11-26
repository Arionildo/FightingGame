using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterBattleHUD : MonoBehaviour
{
    public GameObject hero1, hero2;
    GameObject[,] matrixHUD = new GameObject[4, 3];
    public int ActivePlayers = 2;
    public GameObject refSceneManager;
    public string[] PlayerPosition = new string[4];

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
        if (!hero1.gameObject.GetComponent<Character>().IsAlive() || !hero2.gameObject.GetComponent<Character>().IsAlive())
        {
            gameObject.SetActive(true);
            matrixActivator(ActivePlayers);
            updatePlayerPosition();
        }
    }

    void startFunctions()
    {
        onStartPlayerPosition();
        gameObject.SetActive(false);
        matrixFiller();
    }

    void onStartPlayerPosition()
    {
        for(int i = 0; i <PlayerPosition.Length; i++)
        {
            PlayerPosition[i] = "-";
        }
        PlayerPosition[2] = "kk";
        PlayerPosition[3] = "kk";
    }

    void updatePlayerPosition()
    {
        if (!hero1.GetComponent<Character>().IsAlive() || Input.GetKeyDown(KeyCode.O))
        {
            for (int i = PlayerPosition.Length-1; i < -1; i--)
            {
                if(PlayerPosition[i] == "-")
                {
                    if (refSceneManager.GetComponent<SceneMaganer>().refPlayer1 == "hero1")
                    {
                        PlayerPosition[i] = "Player1";
                    }
                    else if (refSceneManager.GetComponent<SceneMaganer>().refPlayer2 == "hero1")
                    {
                        PlayerPosition[i] = "Player2";
                    }
                }
                else
                {

                }
            }
        }
        if (!hero2.GetComponent<Character>().IsAlive() || Input.GetKeyDown(KeyCode.P))
        {
            for (int i = PlayerPosition.Length - 1; i < -1; i--)
            {
                if (PlayerPosition[i] == "-")
                {
                    if (refSceneManager.GetComponent<SceneMaganer>().refPlayer1 == "hero2")
                    {
                        PlayerPosition[i] = "Player1";
                    }
                    else if (refSceneManager.GetComponent<SceneMaganer>().refPlayer2 == "hero2")
                    {
                        PlayerPosition[i] = "Player2";
                    }
                }
                else
                {

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
                if (playersCount >= i + 1)
                {
                    if (j == 0 || j == 1)
                    {
                        matrixHUD[i, j].gameObject.SetActive(true);
                    }
                    if (j == 1)
                    {
                        if(PlayerPosition[i] == "Player1")
                        {
                            if (refSceneManager.GetComponent<SceneMaganer>().refPlayer1 == "Hero1")
                            {
                                //carrega imagem do robo 1
                            }
                            else
                            {
                                //carrega imagem do robo 2
                            }
                        }
                        else if (PlayerPosition[i] == "Player2")
                        {
                            if (refSceneManager.GetComponent<SceneMaganer>().refPlayer2 == "Hero1")
                            {
                                //carrega imagem do robo 1
                            }
                            else
                            {
                                //carrega imagem do robo 2
                            }
                        }
                        else
                        {

                        }
                        matrixHUD[i, j].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Teste");
                    }
                    if (j == 2)
                    {
                        if (PlayerPosition[i] == "Player1" || PlayerPosition[i] == "Player2")
                        {
                            matrixHUD[i, j].gameObject.GetComponent<Text>().text = PlayerPosition[i];
                        }
                        matrixHUD[i, j].gameObject.GetComponent<Text>().text = "";
                    }
                }
            else if (playersCount < i + 1)
            {
                if (j == 0 || j == 1)
                {
                    matrixHUD[i, j].gameObject.SetActive(false);
                }
                if (j == 2)
                {
                    matrixHUD[i, j].gameObject.GetComponent<Text>().text = "";
                }
            }
            else
            {

            }
            }
        }
    }

}
