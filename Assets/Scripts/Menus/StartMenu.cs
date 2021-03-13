using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Network networkManager = null;

    [Header("Menu")]
    [SerializeField] private GameObject main = null;
    [SerializeField] private GameObject startMenu = null;

    void Start()
    {
        Button buttonBack = transform.Find("ButtonBack").GetComponent<Button>();

        buttonBack.onClick.AddListener(GoBack);

    }

    void GoBack()
    {
        main.SetActive(true);
        startMenu.SetActive(false);
    }

    public void HostLobby()
    {
        networkManager.StartHost();

        startMenu.SetActive(false);

    }

}
