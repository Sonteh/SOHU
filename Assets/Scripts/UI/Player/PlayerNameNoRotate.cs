using UnityEngine;

public class PlayerNameNoRotate : MonoBehaviour
{
    private Quaternion rotation;

    void Start()
    {
        rotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = rotation;
    }
}
