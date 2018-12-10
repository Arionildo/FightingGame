using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMaganer : MonoBehaviour
{
    public Button buttonNewGame, buttonOptions, buttonExit, buttonReturn, buttonResume, buttonMenu, 
        buttonHero1, buttonHero2, buttonHero3, buttonHero4;
    public Text p1Text, p2Text, p3Text, p4Text;
    public GameObject CharSelect, AfterCharSelect, PauseTextScene1, MechLogo;
    public Slider volumeSlider;
    public AudioSource audioData;
    public static string refPlayer1, refPlayer2, refPlayer3, refPlayer4;
    public static string[] playerSelections = new string[4];
    public bool gameIsPaused = false, audioEnabled = true;

    void Start()
    {
        setPlayerSelections();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            buttonNewGame.gameObject.SetActive(true);
            buttonOptions.gameObject.SetActive(true);
            buttonExit.gameObject.SetActive(true);
            CharSelect.gameObject.SetActive(false);

        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            buttonResume.gameObject.SetActive(false);
            buttonOptions.gameObject.SetActive(false);
            buttonMenu.gameObject.SetActive(false);
            PauseTextScene1.gameObject.SetActive(false);
        }
        buttonReturn.gameObject.SetActive(false);
        volumeSlider.gameObject.SetActive(false);
        AfterCharSelect.gameObject.SetActive(false);
        audioData = GetComponent<AudioSource>();
        if (audioData != null) audioData.Play(0);
    }

    void Update()
    {
        if (volumeSlider != null)
        {
            if (audioEnabled)
            {
                AudioListener.volume = volumeSlider.value;
            }
            else
            {
                AudioListener.volume = 0;
            }
        }
        Pause();
    }

    void setPlayerSelections()
    {
        for (int i = 0; i < playerSelections.Length; i++)
        {
            playerSelections[i] = "-";
        }
    }

    public void ButtonClick(string ButtonFunc)
    {
        if (ButtonFunc == "Start")
        {
            buttonNewGame.gameObject.SetActive(false);
            buttonOptions.gameObject.SetActive(false);
            buttonExit.gameObject.SetActive(false);
            buttonReturn.gameObject.SetActive(true);
            CharSelect.gameObject.SetActive(true);
            volumeSlider.gameObject.SetActive(false);
            preFillPlayerTexts();
        }
        else if (ButtonFunc == "Options")
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                buttonNewGame.gameObject.SetActive(false);
                buttonOptions.gameObject.SetActive(false);
                buttonExit.gameObject.SetActive(false);
                buttonReturn.gameObject.SetActive(true);
                volumeSlider.gameObject.SetActive(true);
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                buttonResume.gameObject.SetActive(false);
                buttonOptions.gameObject.SetActive(false);
                buttonMenu.gameObject.SetActive(false);
                buttonReturn.gameObject.SetActive(true);
                volumeSlider.gameObject.SetActive(true);
            }
        }
        else if (ButtonFunc == "Return")
        {
            if (CharSelect != null)
            {
                cleanPlayerTexts();
                CharSelect.gameObject.SetActive(false);
            }
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                buttonNewGame.gameObject.SetActive(true);
                buttonOptions.gameObject.SetActive(true);
                buttonExit.gameObject.SetActive(true);
                buttonReturn.gameObject.SetActive(false);
                volumeSlider.gameObject.SetActive(false);
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                buttonResume.gameObject.SetActive(true);
                buttonOptions.gameObject.SetActive(true);
                buttonMenu.gameObject.SetActive(true);
                buttonReturn.gameObject.SetActive(false);
                volumeSlider.gameObject.SetActive(false);
            }
        }
        else if (ButtonFunc == "Exit")
        {
            Application.Quit();
        }
        else if (ButtonFunc == "Resume")
        {
            gameIsPaused = false;
            Time.timeScale = 1.0f;
            PauseTextScene1.gameObject.SetActive(false);
            buttonResume.gameObject.SetActive(false);
            buttonOptions.gameObject.SetActive(false);
            buttonMenu.gameObject.SetActive(false);
            buttonReturn.gameObject.SetActive(false);
        }
        else if (ButtonFunc == "Menu")
        {
            buttonResume.gameObject.SetActive(false);
            buttonOptions.gameObject.SetActive(false);
            buttonMenu.gameObject.SetActive(false);
            AfterCharSelect.gameObject.SetActive(true);
        }
        else if (ButtonFunc == "Hero1" || ButtonFunc == "Hero2" || ButtonFunc == "Hero3" || ButtonFunc == "Hero4")
        {
            buttonReturn.gameObject.SetActive(false);
            for (int i = 0; i < 2; i++)
            {
                if (playerSelections[i] == "-")
                {
                    if (ButtonFunc == "Hero1")
                    {
                        buttonHero1.gameObject.SetActive(false);
                        playerSelections[i] = "Hero1";
                        break;
                    }
                    else if (ButtonFunc == "Hero2")
                    {
                        buttonHero2.gameObject.SetActive(false);
                        playerSelections[i] = "Hero2";
                        break;
                    }
                    else if (ButtonFunc == "Hero3")
                    {
                        buttonHero3.gameObject.SetActive(false);
                        playerSelections[i] = "Hero3";
                        break;
                    }
                    else if (ButtonFunc == "Hero4")
                    {
                        buttonHero4.gameObject.SetActive(false);
                        playerSelections[i] = "Hero4";
                        break;
                    }
                    else
                    {

                    }
                }
            }
            playerSelections[2] = "NoPlayer";
            playerSelections[3] = "NoPlayer";
            refPlayer1 = playerSelections[0];
            p1Text.text = "P1 "+ refPlayer1;
            refPlayer2 = playerSelections[1];
            p2Text.text = "P2 " + refPlayer2;
            refPlayer3 = playerSelections[2];
            //p3Text.text = "P3 " + refPlayer3;
            //refPlayer4 = playerSelections[3];
            //p4Text.text = "P4 " + refPlayer4;
            if (playerSelections[0] != "-" && playerSelections[1] != "-" && playerSelections[2] != "-" && playerSelections[3] != "-")
            {
                CharSelect.gameObject.SetActive(false);
                AfterCharSelect.gameObject.SetActive(true);
            }
        }
        else if (ButtonFunc == "Confirm")
        {
            if (Time.timeScale != 1.0f)
            {
                Time.timeScale = 1.0f;
            }
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene("Scene01", LoadSceneMode.Single);
                if (audioData != null) audioData.Stop();
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadScene("menu", LoadSceneMode.Single);
                if (audioData != null) audioData.Play(0);
            }
        }
        else if (ButtonFunc == "X")
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                CharSelect.gameObject.SetActive(true);
                buttonReturn.gameObject.SetActive(true);
                setPlayerSelections();
                cleanPlayerTexts();
            }

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                buttonResume.gameObject.SetActive(true);
                buttonOptions.gameObject.SetActive(true);
                buttonMenu.gameObject.SetActive(true);
            }
            AfterCharSelect.gameObject.SetActive(false);
        }
        else if (ButtonFunc == "Volume")
        {
            if (audioEnabled)
            {
                audioEnabled = false;
            }
            else
            {
                audioEnabled = true;
            }
        }
        else
        {

        }
    }

    void cleanPlayerTexts()
    {
        p1Text.text = "";
        p2Text.text = "";
        p3Text.text = "";
        p4Text.text = "";
    }

    void preFillPlayerTexts()
    {
        p1Text.text = "P1 - ";
        p2Text.text = "P2 - ";
        p3Text.text = "P3 - ";
        p4Text.text = "P4 - ";
    }

    public void Pause()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetButtonDown("Pause"))
            {
                if (gameIsPaused)
                {
                    Time.timeScale = 1.0f;
                    PauseTextScene1.gameObject.SetActive(false);
                    buttonResume.gameObject.SetActive(false);
                    buttonOptions.gameObject.SetActive(false);
                    buttonMenu.gameObject.SetActive(false);
                    buttonReturn.gameObject.SetActive(false);
                    volumeSlider.gameObject.SetActive(false);
                    gameIsPaused = false;
                }
                else
                {
                    PauseTextScene1.gameObject.SetActive(true);
                    Time.timeScale = 0.0f;
                    buttonResume.gameObject.SetActive(true);
                    buttonOptions.gameObject.SetActive(true);
                    buttonMenu.gameObject.SetActive(true);
                    buttonReturn.gameObject.SetActive(false);
                    volumeSlider.gameObject.SetActive(false);
                    gameIsPaused = true;
                }
            }
        }
    }
}
