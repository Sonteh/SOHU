using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ServersList : MonoBehaviour
{
    [SerializeField] private GameObject serverListContent;
    [SerializeField] private GameObject serverListElement;
    [SerializeField] private Button sortPlayersButton;
    [SerializeField] private Button sortArenasButton;
    [SerializeField] private TMP_InputField filterPlayersInput;
    [SerializeField] private TMP_InputField filterArenasInput;
    [SerializeField] private Button statusButton;
    [SerializeField] private TMP_Text statusText;

    private bool toggle;
    private string dir;

    public void Refresh()
    {
        cleanList();

        resetArrows();

        var response = APIHelper.GetServers();

        unpackResponse(response);
    }

    public void SortPlayers()
    {
        cleanList();

        toggleArrow(sortPlayersButton);

        var response = APIHelper.GetSortedByPlayers(dir);

        unpackResponse(response);
    }

    public void SortArena()
    {
        cleanList();

        toggleArrow(sortArenasButton);

        var response = APIHelper.GetSortedByArena(dir);

        unpackResponse(response);

    }

    public void Filter()
    {
        cleanList();

        resetArrows();

        string player = filterPlayersInput.text;
        string arena = filterArenasInput.text;

        var response = APIHelper.Filter(player, arena);

        unpackResponse(response);
    }

    public void CheckStatus()
    {

    }

    private void cleanList()
    {
        foreach (Transform child in serverListContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void unpackResponse(Servers response)
    {
        foreach (var el in response.data)
        {
            var element = Instantiate(serverListElement);
            element.transform.GetChild(0).GetComponent<TMP_Text>().text = el.PlayerName;
            element.transform.GetChild(1).GetComponent<TMP_Text>().text = el.map;
            element.transform.SetParent(serverListContent.transform, false);
        }
    }

    private void toggleArrow(Button button)
    {
        toggle = !toggle;

        var text = button.GetComponentInChildren<TMP_Text>();
        text.text = toggle ? "↓" : "↑";

        if (toggle == false)
        {
            dir = "desc";
        }
        else if (toggle == true)
        {
            dir = "asc";
        }
    }

    private void resetArrows()
    {
        toggle = false;
        var text1 = sortPlayersButton.GetComponentInChildren<TMP_Text>();
        var text2 = sortArenasButton.GetComponentInChildren<TMP_Text>();
        text1.text = "↑";
        text2.text = "↑";
    }
}
