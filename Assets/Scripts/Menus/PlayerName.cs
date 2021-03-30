using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private InputField nameInput = null;

    public static string displayName { get; private set; }
    private const string playerNameKey = "PlayerName";
    private const string defaultName = "Player Unknown";

    void Start()
    {
        //Clear player prefs
        //PlayerPrefs.DeleteAll();

        SetUpDefaultName();

        nameInput.onValueChanged.AddListener(delegate { SetPlayerName(); });
    }

    private void SetUpDefaultName()
    {
        if (!PlayerPrefs.HasKey(playerNameKey))
        {
            nameInput.text = defaultName;

            displayName = nameInput.text;

            SetDefaultName();
        }
        else
        {
            nameInput.text = PlayerPrefs.GetString(playerNameKey);

            displayName = nameInput.text;
        }

    }

    private void SetDefaultName()
    {
        PlayerPrefs.SetString(playerNameKey, nameInput.text);
    }

    private void SetPlayerName()
    {

        if (nameInput.text != null)
        {
            displayName = nameInput.text;

            PlayerPrefs.SetString(playerNameKey, displayName);
        }
    }
}
