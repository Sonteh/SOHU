using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMainMenu : MonoBehaviour
{

    

    void Start()
    {
        Button buttonStart = GameObject.Find("ButtonStart").GetComponent<Button>();
        Button buttonOptions = GameObject.Find("ButtonOptions").GetComponent<Button>();
        Button buttonExit = GameObject.Find("ButtonExit").GetComponent<Button>();

        buttonStart.onClick.AddListener(StartGame);
        buttonStart.onClick.AddListener(DisplayOptions);
        buttonStart.onClick.AddListener(ExitGame);


    }


    void StartGame() { }

    void DisplayOptions() { }

    void ExitGame() { }
}
