using UnityEngine;

public class HealthNoRotate : MonoBehaviour
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
