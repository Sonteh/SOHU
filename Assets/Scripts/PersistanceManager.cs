using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistanceManager : MonoBehaviour
{

    public static PersistanceManager persistanceManager { get; private set; }

    private void Awake()
    {
        if (persistanceManager == null)
        {
            persistanceManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
