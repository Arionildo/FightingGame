using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMaganer : MonoBehaviour
{
    public Button buttonNewGame, buttonOptions, buttonExit, buttonReturn, buttonResume, buttonMenu;
    public GameObject CharSelect, AfterCharSelect, PauseTextScene1, MechLogo;
    public Slider volumeSlider;
    public AudioSource audioData;
    public static string Player1, Player2;
    public bool gameIsPaused = false, audioEnabled = true;

    void Start()
    {
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
        else if (ButtonFunc == "Hero1" || ButtonFunc == "Hero2")
        {
            CharSelect.gameObject.SetActive(false);
            AfterCharSelect.gameObject.SetActive(true);
            buttonReturn.gameObject.SetActive(false);
            if (ButtonFunc == "Hero1")
            {
                Player1 = "Hero1";
                Player2 = "Hero2";
            }
            if (ButtonFunc == "Hero2")
            {
                Player2 = "Hero1";
                Player1 = "Hero2";
            }
        }
        else if (ButtonFunc == "Confirm")
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene("Scene01", LoadSceneMode.Single);
                if (audioData != null) audioData.Stop();
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (Time.timeScale != 1.0f)
                {
                Time.timeScale = 1.0f;
                }
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
