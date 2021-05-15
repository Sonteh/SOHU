using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class Player : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        Vector3 playerPosition = transform.position;
        Camera.main.transform.localPosition = new Vector3(playerPosition.x - 10, Camera.main.transform.localPosition.y, playerPosition.z); //Wycentrowanie kamery na gracza
    }
}