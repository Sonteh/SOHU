using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    //[SerializeField] private Network networkManager = null;

    [Header("Menu")]
    [SerializeField] private GameObject main = null;
    [SerializeField] private GameObject startMenu = null;

    void Start()
    {
        Button buttonBack = transform.Find("ButtonBack").GetComponent<Button>();
        Button buttonRefresh = transform.Find("ButtonRefresh").GetComponent<Button>();

        buttonBack.onClick.AddListener(GoBack);
        buttonRefresh.onClick.AddListener(Refresh);

    }

    void GoBack()
    {
        main.SetActive(true);
        startMenu.SetActive(false);
    }

    public void HostLobby()
    {
        //networkManager.StartHost();

        startMenu.SetActive(false);

        NetworkManager.singleton.StartHost();

    }

    public void Refresh()
    {
        Debug.Log("here");

        var pd = APIHelper.GetPlayers();

        Debug.Log(pd.data[0].IP);
    }

}
