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

    private bool toggle;
    private string dir;

    public void Refresh()
    {
        cleanList();

        var pd = APIHelper.GetServers();

        unpackResponse(pd);
    }

    public void SortPlayers()
    {
        cleanList();

        toggleArrow(sortPlayersButton);

        var pd = APIHelper.GetSortedByPlayers(dir);

        unpackResponse(pd);
    }

    public void SortArena()
    {
        cleanList();

        toggleArrow(sortArenasButton);

        var pd = APIHelper.GetSortedByArena(dir);

        unpackResponse(pd);

    }

    public void Filter()
    {
        //Debug.Log(serverListContent.transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text);
    }

    private void cleanList()
    {
        foreach (Transform child in serverListContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void unpackResponse(Servers pd)
    {
        foreach (var el in pd.data)
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

    private void resetArrow()
    {

    }
}
