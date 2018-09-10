using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMaganer : MonoBehaviour
{
    public Button buttonNewGame, buttonOptions, buttonExit, buttonVoltaMenu;

    void Start()
    {
        buttonNewGame.gameObject.SetActive(true);
        buttonOptions.gameObject.SetActive(true);
        buttonExit.gameObject.SetActive(true);
        buttonVoltaMenu.gameObject.SetActive(false);
    }

    void Update()
    {

    }

    public void ButtonClick(string ButtonFunc)
    {
        if (ButtonFunc == "Start")
        {
            Debug.Log("start button");
            //SceneManager.LoadScene(indice da cena)
        }
        else if (ButtonFunc == "Options")
        {
            Debug.Log("options button");
            buttonNewGame.gameObject.SetActive(false);
            buttonOptions.gameObject.SetActive(false);
            buttonExit.gameObject.SetActive(false);
            buttonVoltaMenu.gameObject.SetActive(true);
        }
        else if (ButtonFunc == "FazAlgo")
        {
            Debug.Log("faz algo button");
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
