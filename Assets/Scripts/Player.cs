using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        Vector3 playerPosition = transform.position;
        Camera.main.transform.localPosition = new Vector3(playerPosition.x - 10, Camera.main.transform.localPosition.y, playerPosition.z); //Wycentrowanie kamery na gracza
    }

    private void OnEnable() 
    {
        //transform.position = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z + 10);
    }
}