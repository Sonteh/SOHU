using UnityEngine;

public class PlayerNameNoRotate : MonoBehaviour
{
    private Quaternion rotation;

    void Start()
    {
        rotation = Quaternion.Euler(0, 90, 0);
    }

    void Update()
    {
        transform.rotation = rotation;
    }
}
