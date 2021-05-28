using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ServersList : MonoBehaviour
{
    [SerializeField] GameObject serverListContent;
    [SerializeField] GameObject serverListElement;

    public void Refresh()
    {

        foreach (Transform child in serverListContent.transform)
        {
            Destroy(child.gameObject);
        }

        var pd = APIHelper.GetPlayers();

        foreach (var el in pd.data)
        {
            var element = Instantiate(serverListElement);
            element.transform.GetChild(0).GetComponent<TMP_Text>().text = el.PlayerName;
            element.transform.GetChild(1).GetComponent<TMP_Text>().text = el.map;
            Debug.Log(el.IP);
            //element.gameObject.AddComponent<JoinFromList>();
            element.transform.SetParent(serverListContent.transform, false);
        }
    }
}
