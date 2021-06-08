using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Chat : NetworkBehaviour
{
    [SerializeField] private Text chatText = null;
    [SerializeField] private InputField inputField = null;
    [SerializeField] private GameObject canvas = null;
    public static bool isChatActive = false;

    [SerializeField] private GameObject scrollView = null;
    [SerializeField] private GameObject inputFieldObject = null;
    [SerializeField] private Player player;
    public float liveTime = 0.0f;
    private string playerName;

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
            scrollView.SetActive(true);
            inputFieldObject.SetActive(true);
            inputField.ActivateInputField();
            isChatActive = true;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            scrollView.SetActive(false);
            inputFieldObject.SetActive(false);
            isChatActive = false;
        }
        if (isChatActive == true)
        {
            liveTime = Time.time + 1.0f;
        }

        if (Time.time - liveTime > 0.0f)
        {
            scrollView.SetActive(false);
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

        scrollView.SetActive(true);
        liveTime = Time.time + 5.0f; 
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
        //RpcHandleMessage($"[{PlayerPrefs.GetString("PlayerName")}]: {message}");
        RpcHandleMessage($"[{player.playerName}]: {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }
}