using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Chat : NetworkBehaviour
{
    [SerializeField] private Text chatText = null;
    [SerializeField] private InputField inputField = null;
    [SerializeField] private GameObject canvas = null;

    private static event Action<string> OnMessage;

    public override void OnStartAuthority()
    {
        canvas.SetActive(true);

        OnMessage += HandleNewMessage;
    }

    private void Update()
    {
        if (!hasAuthority) {return;}

        if(Input.GetKeyDown(KeyCode.Return))
        {
            canvas.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(false);
        }
    }

    [ClientCallback]
    private void OnDestroy()
    {
        if(!hasAuthority) { return; }

        OnMessage -= HandleNewMessage;
    }

    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    [Client]
    public void Send()
    {
        if(!Input.GetKeyDown(KeyCode.Return)) { return; }
        if (string.IsNullOrWhiteSpace(inputField.text)) { return; }
        CmdSendMessage(inputField.text);
        inputField.text = string.Empty;
    }

    [Command]
    private void CmdSendMessage(string message)
    {
        // Validate message
        RpcHandleMessage($"[{PlayerPrefs.GetString("PlayerName")}]: {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }
}