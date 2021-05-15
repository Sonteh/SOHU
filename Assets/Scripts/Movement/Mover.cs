using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class Mover : NetworkBehaviour
{
    private void Update()
    {
        if (!isLocalPlayer) {return;}

        if (Input.GetButton("Fire2"))
        {
            MoveToCursor();
        }
    }

    private void MoveToCursor()
    {
        RaycastHit hit = GetPlayerMousePosition();
        
        GetComponent<NavMeshAgent>().destination = hit.point;
    }
    private RaycastHit GetPlayerMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        return hit;
    }
}
