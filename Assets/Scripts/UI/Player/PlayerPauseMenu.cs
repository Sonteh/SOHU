using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerPauseMenu : NetworkBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool IsPauseActive = false;

    private void Update() 
    {
        if (!hasAuthority) { return; }

        if (Input.GetButtonDown("Pause"))
        {
            IsPauseActive = !IsPauseActive;
            pauseMenu.SetActive(IsPauseActive);
        }
    }

    public void ClosePauseMenu()
    {
        IsPauseActive = !IsPauseActive;
        pauseMenu.SetActive(IsPauseActive);
    }

    public void QuitMatch()
    {
        NetworkManager.singleton.ServerChangeScene("Menus");
    }
}
