using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMaganer : MonoBehaviour
{
    public Button buttonNewGame, buttonOptions, buttonExit, buttonReturn, buttonResume, buttonMenu;
    public Text pauseText;
    public Slider volumeSlider;
    public AudioSource audioData;
    public bool gameIsPaused = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            buttonNewGame.gameObject.SetActive(true);
            buttonOptions.gameObject.SetActive(true);
            buttonExit.gameObject.SetActive(true);
            buttonReturn.gameObject.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            pauseText.gameObject.SetActive(false);
            buttonResume.gameObject.SetActive(false);
            buttonOptions.gameObject.SetActive(false);
            buttonMenu.gameObject.SetActive(false);
            buttonReturn.gameObject.SetActive(false);
        }
        audioData = GetComponent<AudioSource>();
        if (audioData != null) audioData.Play(0);
    }

    void Update()
    {
        if (volumeSlider != null)
        {
            AudioListener.volume = volumeSlider.value;
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
            buttonReturn.gameObject.SetActive(false);
            SceneManager.LoadScene("Scene01", LoadSceneMode.Single);
            if (audioData != null) audioData.Stop();
        }
        else if (ButtonFunc == "Options")
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                buttonNewGame.gameObject.SetActive(false);
                buttonOptions.gameObject.SetActive(false);
                buttonExit.gameObject.SetActive(false);
                buttonReturn.gameObject.SetActive(true);
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                buttonResume.gameObject.SetActive(false);
                buttonOptions.gameObject.SetActive(false);
                buttonMenu.gameObject.SetActive(false);
                buttonReturn.gameObject.SetActive(true);
            }
        }
        else if (ButtonFunc == "Return")
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                buttonNewGame.gameObject.SetActive(true);
                buttonOptions.gameObject.SetActive(true);
                buttonExit.gameObject.SetActive(true);
                buttonReturn.gameObject.SetActive(false);
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                buttonResume.gameObject.SetActive(true);
                buttonOptions.gameObject.SetActive(true);
                buttonMenu.gameObject.SetActive(true);
                buttonReturn.gameObject.SetActive(false);
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
            pauseText.gameObject.SetActive(false);
            buttonResume.gameObject.SetActive(false);
            buttonOptions.gameObject.SetActive(false);
            buttonMenu.gameObject.SetActive(false);
            buttonReturn.gameObject.SetActive(false);
        }
        else if (ButtonFunc == "Menu")
        {
            SceneManager.LoadScene("menu", LoadSceneMode.Single);
        }
        else
        {

        }
    }
    public void Pause()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                {
                    gameIsPaused = false;
                    Time.timeScale = 1.0f;
                    pauseText.gameObject.SetActive(false);
                    buttonResume.gameObject.SetActive(false);
                    buttonOptions.gameObject.SetActive(false);
                    buttonMenu.gameObject.SetActive(false);
                    buttonReturn.gameObject.SetActive(false);
                }
                else
                {
                    gameIsPaused = true;
                    Time.timeScale = 0.0f;
                    pauseText.gameObject.SetActive(true);
                    buttonResume.gameObject.SetActive(true);
                    buttonOptions.gameObject.SetActive(true);
                    buttonMenu.gameObject.SetActive(true);
                    buttonReturn.gameObject.SetActive(false);
                }
            }
        }
    }
}
