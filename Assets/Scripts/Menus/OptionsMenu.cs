using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject main = null;
    [SerializeField] private GameObject optionsMenu = null;

    void Start()
    {
        Button buttonBack = transform.Find("ButtonBack").GetComponent<Button>();

        buttonBack.onClick.AddListener(GoBack);

    }

    void GoBack()
    {
        main.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
