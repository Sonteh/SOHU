using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    [Header("Menu")]
    [SerializeField] private GameObject main = null;
    [SerializeField] private GameObject startMenu = null;
    [SerializeField] private GameObject optionsMenu = null;


    void Start()
    {
        main.SetActive(true);
        startMenu.SetActive(false);
        optionsMenu.SetActive(false);

        Button buttonStart = GameObject.Find("ButtonStart").GetComponent<Button>();
        Button buttonOptions = GameObject.Find("ButtonOptions").GetComponent<Button>();
        Button buttonExit = GameObject.Find("ButtonExit").GetComponent<Button>();

        buttonStart.onClick.AddListener(StartGame);
        buttonOptions.onClick.AddListener(DisplayOptions);
        buttonExit.onClick.AddListener(ExitGame);

    }


    void StartGame()
    {
        main.SetActive(false);
        startMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    void DisplayOptions()
    {
        main.SetActive(false);
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    void ExitGame()
    {
        Application.Quit();
    }
}