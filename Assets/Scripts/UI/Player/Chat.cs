// using Mirror;
// using System;
// //using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class Chat : NetworkBehaviour
// {
//     // [SerializeField] private GameObject chatUI = null;
//     // [SerializeField] privTMP_Text chatText = null;
//     // [SerializeField] private TMP_InputField inputField = null;

//     [SerializeField] private GameObject chatUI = null;
//     [SerializeField] private Text chatText = null;
//     [SerializeField] private InputField inputField = null;

//     private static event Action<string> OnMessage;

//     public override void OnStartAuthority()
//     {
//         chatUI.SetActive(true);

//         OnMessage += HandleNewMessage;
//     }

//     [ClientCallback]
//     private void OnDestroy()
//     {
//         if (!hasAuthority) { return; }

//         OnMessage -= HandleNewMessage;
//     }

//     private void HandleNewMessage(string message)
//     {
//         chatText.text += message;
//         //chatText.SetText(message);
//         Debug.Log("ADDING TO CHAT " + message);
//     }

//     [Client]
//     public void Send(string message)
//     {
//         if (!Input.GetKeyDown(KeyCode.Return)) { return; }

//         if (string.IsNullOrWhiteSpace(message)) { return; }

//         CmdSendMessage(message);

//         inputField.text = string.Empty;
//         Debug.Log("SEND FUNCTION" + message);
//     }

//     [Command]
//     private void CmdSendMessage(string message)
//     {
//         RpcHandleMessage($"[{connectionToClient.connectionId}]: {message}");
//         Debug.Log("COMMAND FUNCTION" + message);
//     }

//     [ClientRpc]
//     private void RpcHandleMessage(string message)
//     {
//         OnMessage?.Invoke($"\n{message}");
//         Debug.Log("CLIENT FUNCTION" + message);
//     }
// }

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

    // Called when the a client is connected to the server
    public override void OnStartAuthority()
    {
        canvas.SetActive(true);

        OnMessage += HandleNewMessage;
    }

    // Called when a client has exited the server
    [ClientCallback]
    private void OnDestroy()
    {
        if(!hasAuthority) { return; }

        OnMessage -= HandleNewMessage;
    }

    // When a new message is added, update the Scroll View's Text to include the new message
    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    // When a client hits the enter button, send the message in the InputField
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
        RpcHandleMessage($"[{connectionToClient.connectionId}]: {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }

}