using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _fireballPrefab;
    [SerializeField]
    private float _fireballCooldown = 0.5f;
    private float _canUseFireball = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time > _canUseFireball)
        {
            UseFireball();
        }
    }

    private void UseFireball()
    {
        _canUseFireball = _fireballCooldown + Time.time;
        Instantiate(_fireballPrefab, transform.position + Vector3.forward, Quaternion.identity);

        
    }
}
