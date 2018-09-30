using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMaganer : MonoBehaviour
{
    public Button buttonNewGame, buttonOptions, buttonExit, buttonVoltaMenu;
    public AudioSource audioData;

    void Start()
    {
        buttonNewGame.gameObject.SetActive(true);
        buttonOptions.gameObject.SetActive(true);
        buttonExit.gameObject.SetActive(true);
        buttonVoltaMenu.gameObject.SetActive(false);
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }

    void Update()
    {

    }

    public void ButtonClick(string ButtonFunc)
    {
        if (ButtonFunc == "Start")
        {
            buttonNewGame.gameObject.SetActive(false);
            buttonOptions.gameObject.SetActive(false);
            buttonExit.gameObject.SetActive(false);
            buttonVoltaMenu.gameObject.SetActive(false);
            SceneManager.LoadScene("Scene01", LoadSceneMode.Single);
            audioData.Stop();
        }
        else if (ButtonFunc == "Options")
        {
            buttonNewGame.gameObject.SetActive(false);
            buttonOptions.gameObject.SetActive(false);
            buttonExit.gameObject.SetActive(false);
            buttonVoltaMenu.gameObject.SetActive(true);
        }
        else if (ButtonFunc == "FazAlgo")
        {
            buttonNewGame.gameObject.SetActive(true);
            buttonOptions.gameObject.SetActive(true);
            buttonExit.gameObject.SetActive(true);
            buttonVoltaMenu.gameObject.SetActive(false);
        }
        else if (ButtonFunc == "Exit")
        {
            Application.Quit();
        }
        else
        {

        }
    }
}
