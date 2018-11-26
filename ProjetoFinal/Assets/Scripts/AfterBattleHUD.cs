using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterBattleHUD : MonoBehaviour
{
    public GameObject hero1, hero2, hero3, hero4;
    bool placedHero1 = false, placedHero2 = false, placedHero3 = false, placedHero4 = false;
    float hero1HP, hero2HP, hero3HP, hero4HP;
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
        if (hero1.gameObject.GetComponent<Character>().currentLife <= 0 || hero2.gameObject.GetComponent<Character>().currentLife <= 0)
        {
            transform.Find("ScoreScreenChild").gameObject.SetActive(true);
            matrixActivator(ActivePlayers);
            updatePlayerPosition();
        }
    }

    void startFunctions()
    {
        onStartPlayerPosition();
        transform.Find("ScoreScreenChild").gameObject.SetActive(false);
        matrixFiller();
    }

    void onStartPlayerPosition()
    {
        for(int i = 0; i <PlayerPosition.Length; i++)
        {
            PlayerPosition[i] = "-";
        }
        //nota para modificar para 4 jogadores: remover as 2 linhas a baixo.
        PlayerPosition[2] = "kk";
        PlayerPosition[3] = "kk";
    }

    void updatePlayerPosition()
    {
        if (hero1HP <=0 && !placedHero1)
        {
            for (int i = PlayerPosition.Length-1; i < -1; i--)
            {
                if(PlayerPosition[i] == "-")
                {
                    if (refSceneManager.GetComponent<SceneMaganer>().refPlayer1 == "hero1")
                    {
                        PlayerPosition[i] = "Player1";
                        break;
                    }
                    else if (refSceneManager.GetComponent<SceneMaganer>().refPlayer2 == "hero1")
                    {
                        PlayerPosition[i] = "Player2";
                        break;
                    }
                    //else if(refSceneManager.GetComponent<SceneMaganer>().refPlayer3 == "hero1")
                    //{
                    //    PlayerPosition[i] = "Player3";
                    //    break;
                    //}
                    //else if (refSceneManager.GetComponent<SceneMaganer>().refPlayer4 == "hero1")
                    //{
                    //    PlayerPosition[i] = "Player4";
                    //    break;
                    //}
                    else
                    {

                    }
                }
                else
                {

                }
            }
            placedHero1 = true;
        }
        else if (hero2HP <= 0 && !placedHero2)
        {
            for (int i = PlayerPosition.Length - 1; i < -1; i--)
            {
                if (PlayerPosition[i] == "-")
                {
                    if (refSceneManager.GetComponent<SceneMaganer>().refPlayer1 == "hero2")
                    {
                        PlayerPosition[i] = "Player1";
                        break;
                    }
                    else if (refSceneManager.GetComponent<SceneMaganer>().refPlayer2 == "hero2")
                    {
                        PlayerPosition[i] = "Player2";
                        break;
                    }
                    //else if(refSceneManager.GetComponent<SceneMaganer>().refPlayer3 == "hero2")
                    //{
                    //    PlayerPosition[i] = "Player3";
                    //    break;
                    //}
                    //else if (refSceneManager.GetComponent<SceneMaganer>().refPlayer4 == "hero2")
                    //{
                    //    PlayerPosition[i] = "Player4";
                    //    break;
                    //}
                    else
                    {

                    }
                }
                else
                {

                }
            }
            placedHero2 = true;
        }
        else if (hero3HP <= 0 && !placedHero3)
        {
            for (int i = PlayerPosition.Length - 1; i < -1; i--)
            {
                if (PlayerPosition[i] == "-")
                {
                    if (refSceneManager.GetComponent<SceneMaganer>().refPlayer1 == "hero3")
                    {
                        PlayerPosition[i] = "Player1";
                        break;
                    }
                    else if (refSceneManager.GetComponent<SceneMaganer>().refPlayer2 == "hero3")
                    {
                        PlayerPosition[i] = "Player2";
                        break;
                    }
                    //else if(refSceneManager.GetComponent<SceneMaganer>().refPlayer3 == "hero3")
                    //{
                    //    PlayerPosition[i] = "Player3";
                    //    break;
                    //}
                    //else if (refSceneManager.GetComponent<SceneMaganer>().refPlayer4 == "hero3")
                    //{
                    //    PlayerPosition[i] = "Player4";
                    //    break;
                    //}
                    else
                    {

                    }
                }
                else
                {

                }
            }
            placedHero3 = true;
        }
        else if (hero4HP <= 0 && !placedHero4)
        {
            for (int i = PlayerPosition.Length - 1; i < -1; i--)
            {
                if (PlayerPosition[i] == "-")
                {
                    if (refSceneManager.GetComponent<SceneMaganer>().refPlayer1 == "hero4")
                    {
                        PlayerPosition[i] = "Player1";
                        break;
                    }
                    else if (refSceneManager.GetComponent<SceneMaganer>().refPlayer2 == "hero4")
                    {
                        PlayerPosition[i] = "Player2";
                        break;
                    }
                    //else if(refSceneManager.GetComponent<SceneMaganer>().refPlayer3 == "hero4")
                    //{
                    //    PlayerPosition[i] = "Player3";
                    //    break;
                    //}
                    //else if (refSceneManager.GetComponent<SceneMaganer>().refPlayer4 == "hero4")
                    //{
                    //    PlayerPosition[i] = "Player4";
                    //    break;
                    //}
                    else
                    {

                    }
                }
                else
                {

                }
            }
            placedHero4 = true;
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
                        matrixHUD[i, j].gameObject.SetActive(false);
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
